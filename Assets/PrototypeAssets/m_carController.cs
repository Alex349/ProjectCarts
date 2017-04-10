using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class m_carController : MonoBehaviour {

	public float idealRPM = 500f;
	public float maxRPM = 1000f;
    private new Rigidbody rigidbody;

    public Transform centerOfGravity;

	public WheelCollider wheelFR;
	public WheelCollider wheelFL;
	public WheelCollider wheelBR;
	public WheelCollider wheelBL;

	public float turnRadius = 6f;
	public float torque = 100f;
	public float brakeTorque = 100f;

	public float AntiRoll = 20000.0f;

	public enum DriveMode { Front, Rear, All };
	public DriveMode driveMode = DriveMode.Rear;

	public Text speedText;

	void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
		rigidbody.centerOfMass = centerOfGravity.localPosition;
	}

	public float Speed()
    {
		return wheelBR.radius * Mathf.PI * wheelBR.rpm * 60f / 1000f;
	}

	public float Rpm()
    {
		return wheelBL.rpm;
	}

	void FixedUpdate () {

		if(speedText!=null)
			speedText.text = "Speed: " + Speed().ToString("f0") + " km/h";

		//Debug.Log ("Speed: " + (wheelRR.radius * Mathf.PI * wheelRR.rpm * 60f / 1000f) + "km/h    RPM: " + wheelRL.rpm);

		float scaledTorque = Input.GetAxis("Vertical") * torque;

		if(wheelBL.rpm < idealRPM)
			scaledTorque = Mathf.Lerp(scaledTorque/10f, scaledTorque, wheelBL.rpm / idealRPM );
		else 
			scaledTorque = Mathf.Lerp(scaledTorque, 0,  (wheelBL.rpm-idealRPM) / (maxRPM-idealRPM) );

		BarRolling(wheelFR, wheelFL);
		BarRolling(wheelBR, wheelBL);

		wheelFR.steerAngle = Input.GetAxis("Horizontal") * turnRadius;
		wheelFL.steerAngle = Input.GetAxis("Horizontal") * turnRadius;

		wheelFR.motorTorque = driveMode==DriveMode.Rear  ? 0 : scaledTorque;
		wheelFL.motorTorque = driveMode==DriveMode.Rear  ? 0 : scaledTorque;
        wheelBR.motorTorque = driveMode==DriveMode.Front ? 0 : scaledTorque;
		wheelBL.motorTorque = driveMode==DriveMode.Front ? 0 : scaledTorque;

		if(Input.GetButton("Fire1"))
        {
			wheelFR.brakeTorque = brakeTorque;
			wheelFL.brakeTorque = brakeTorque;
            wheelBR.brakeTorque = brakeTorque;
			wheelBL.brakeTorque = brakeTorque;
		}
		else {
			wheelFR.brakeTorque = 0;
			wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
			wheelBL.brakeTorque = 0;
		}
	}


	void BarRolling(WheelCollider WheelL, WheelCollider WheelR) {
		WheelHit hit;
		float travelL = 1.0f;
		float travelR = 1.0f;
		
		bool groundedL = WheelL.GetGroundHit(out hit);
		if (groundedL)
			travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;
		
		bool groundedR = WheelR.GetGroundHit(out hit);
		if (groundedR)
			travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;
		
		float antiRollForce = (travelL - travelR) * AntiRoll;
		
		if (groundedL)
			rigidbody.AddForceAtPosition(WheelL.transform.up * -antiRollForce,
			                             WheelL.transform.position); 
		if (groundedR)
			rigidbody.AddForceAtPosition(WheelR.transform.up * antiRollForce,
			                             WheelR.transform.position); 
	}

}
