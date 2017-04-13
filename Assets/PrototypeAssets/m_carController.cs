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

    public MeshRenderer wheelFRMesh;
    public MeshRenderer wheelFLMesh;
    public MeshRenderer wheelBRMesh;
    public MeshRenderer wheelBLMesh;

    public int acceleration = 10;
    public float gravity = 9.81f;
	public float turnRadius = 6f;
	public float torque = 100f;
	public float brakeTorque = 100f;

	public float AntiRoll = 20000.0f;

	public enum DriveMode { Front, Rear, All };
	public DriveMode driveMode = DriveMode.Rear;

	public Text speedText;

    private Transform m_car_position;
    private float timeCounter;
    float wheelBLMeshRotation = 0, wheelBRMeshRotation = 0, wheelFLMeshRotation = 0, wheelFRMeshRotation = 0;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
		rigidbody.centerOfMass = centerOfGravity.localPosition;
        m_car_position = transform;
    }

	public float Speed()
    {
        //convert to km/h
		return wheelBR.radius * Mathf.PI * wheelBR.rpm * 60f / 1000f;        
	}

	public float Rpm()
    {
		return wheelBL.rpm;
	}

	void FixedUpdate ()
    {
        m_car_position = rigidbody.transform;

        if (speedText!=null)
			speedText.text = "Speed: " + Speed().ToString("") + " km/h";

		float scaledTorque = Input.GetAxis("Vertical") * torque * acceleration;

		if(wheelBL.rpm < idealRPM)
			scaledTorque = Mathf.Lerp(scaledTorque/5, scaledTorque, wheelBL.rpm / idealRPM );
		else 
			scaledTorque = Mathf.Lerp(scaledTorque, 0,  (wheelBL.rpm-idealRPM) / (maxRPM-idealRPM) );

		//BarRolling(wheelFR, wheelFL);
		BarRolling(wheelBR, wheelBL, wheelFR, wheelFL);

		wheelFR.steerAngle = Input.GetAxis("Horizontal") * turnRadius;
		wheelFL.steerAngle = Input.GetAxis("Horizontal") * turnRadius;
        
        wheelFR.motorTorque = driveMode==DriveMode.Rear  ? 0 : scaledTorque;
		wheelFL.motorTorque = driveMode==DriveMode.Rear  ? 0 : scaledTorque;
        wheelBR.motorTorque = driveMode==DriveMode.Front ? 0 : scaledTorque;
		wheelBL.motorTorque = driveMode==DriveMode.Front ? 0 : scaledTorque;

        if (Input.GetAxis("Vertical") != 0)
        {
            //rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * acceleration/5, ForceMode.Acceleration);
        }
        if (Input.GetButton("Fire1"))
        {
			wheelFR.brakeTorque = brakeTorque;
			wheelFL.brakeTorque = brakeTorque;
            wheelBR.brakeTorque = brakeTorque;
			wheelBL.brakeTorque = brakeTorque;
		}
		else
        {
			wheelFR.brakeTorque = 0;
			wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
			wheelBL.brakeTorque = 0;
		}
	}


	void BarRolling (WheelCollider WheelBL, WheelCollider WheelBR, WheelCollider WheelFL, WheelCollider WheelFR)
    {
		WheelHit hit;
	    //float travelL = 1.0f;
	    //float travelR = 1.0f;
        Vector3 localPosition = transform.localPosition;
		
		bool groundedBL = WheelBL.GetGroundHit(out hit);
        bool groundedBR = WheelBR.GetGroundHit(out hit);
        bool groundedFL = WheelFL.GetGroundHit(out hit);
        bool groundedFR = wheelFR.GetGroundHit(out hit);

        /*if (groundedL)
            travelL = (-WheelBL.transform.InverseTransformPoint(hit.point).y - WheelBL.radius) / WheelBL.suspensionDistance;       
		if (groundedR)
            travelR = (-WheelBR.transform.InverseTransformPoint(hit.point).y - WheelBR.radius) / WheelBR.suspensionDistance;

        float antiRollForce = (travelL - travelR) * AntiRoll;*/
		
		if (groundedBL)
        {
            m_car_position.localPosition = localPosition;

            if (Input.GetAxis("Vertical") != 0)
            {
                rigidbody.AddForceAtPosition(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * acceleration, WheelBL.transform.position);

                if (Input.GetAxis("Horizontal") > 0)
                {
                    //rigidbody.AddRelativeForce(new Vector3(-transform.forward.x/2, 0, Mathf.Abs(transform.forward.z)) * scaledTorque, ForceMode.Impulse);
                    rigidbody.AddForceAtPosition(new Vector3(-transform.forward.x/2, 0, Mathf.Abs(transform.forward.z)) * acceleration, WheelBL.transform.position);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    rigidbody.AddForceAtPosition(new Vector3(transform.forward.x/2, 0, Mathf.Abs(transform.forward.z)) * acceleration, WheelBL.transform.position);
                }
            }
            
        }			
        else if (!groundedBL)
        {
            timeCounter += Time.deltaTime;
            rigidbody.AddForceAtPosition(WheelBL.transform.up * -gravity, WheelBL.transform.position);

            if (timeCounter >= 2)
            {
                //m_car_position.localPosition = localPosition;
                timeCounter = 0;
            }
        }
        if (groundedBR)
        {
            //rigidbody.AddForceAtPosition(WheelR.transform.up * -antiRollForce, WheelR.transform.position);
            m_car_position.localPosition = localPosition;

            if (Input.GetAxis("Vertical") != 0)
            {
                rigidbody.AddForceAtPosition(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * acceleration, WheelBR.transform.position);

                if (Input.GetAxis("Horizontal") > 0)
                {
                    rigidbody.AddForceAtPosition(new Vector3(-transform.forward.x/2, 0, Mathf.Abs(transform.forward.z)) * acceleration, WheelBR.transform.position);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    rigidbody.AddForceAtPosition(new Vector3(transform.forward.x/2, 0, Mathf.Abs(transform.forward.z)) * acceleration, WheelBR.transform.position);
                }
            }
            
        }			     
        else if (!groundedBR)
        {
            timeCounter += Time.deltaTime;
            rigidbody.AddForceAtPosition(WheelBR.transform.up * -gravity, WheelBR.transform.position);

            if (timeCounter >= 2)
            {
                //m_car_position.localPosition = localPosition;
                timeCounter = 0;
            }
        }        
        if (!groundedFL)
        {
            rigidbody.AddForceAtPosition(WheelFL.transform.up * -gravity, WheelFL.transform.position);
        }
        if (!groundedFR)
        {
            rigidbody.AddForceAtPosition(WheelFR.transform.up * -gravity, WheelFR.transform.position);
        }
    }
    public void ResetPosition()
    {

    }

}
