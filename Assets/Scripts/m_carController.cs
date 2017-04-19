using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class m_carController : MonoBehaviour {

	public float g_RPM = 500f;
	public float max_RPM = 1000f;
    private new Rigidbody rigidbody;

    public Transform centerOfGravity;

	public WheelCollider wheelFR;
	public WheelCollider wheelFL;
	public WheelCollider wheelBR;
	public WheelCollider wheelBL;

    private ParticleSystem m_particleSystem;

    public int acceleration = 10;
    public float gravity = 9.81f;
	public float turnRadius = 6f;
	public float torque = 100f;
	public float brakeTorque = 100f;

	public enum DriveMode { Front, Rear, Drift, All };
	public DriveMode driveMode = DriveMode.Rear;

	public Text speedText;

    private Transform m_car_position;
    private float timeCounter;
    private float scaledTorque;
    private float sideFrictionWheel;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
		rigidbody.centerOfMass = centerOfGravity.localPosition;
        m_particleSystem = wheelBL.GetComponent<ParticleSystem>();  
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
        var m_emission = m_particleSystem.emission;
        rigidbody.AddRelativeForce(Vector3.down * gravity, ForceMode.Acceleration);

        if (speedText!=null)
			speedText.text = "Speed: " + Speed().ToString("") + " km/h";

		scaledTorque = Input.GetAxis("Vertical") * torque;

		if(wheelBL.rpm < g_RPM)
			scaledTorque = Mathf.Lerp(scaledTorque/5, scaledTorque, wheelBL.rpm / g_RPM);
		else 
			scaledTorque = Mathf.Lerp(scaledTorque, 0,  (wheelBL.rpm - g_RPM) / (max_RPM - g_RPM) );		

		wheelFR.steerAngle = Input.GetAxis("Horizontal") * turnRadius;
		wheelFL.steerAngle = Input.GetAxis("Horizontal") * turnRadius;

        if (wheelFR.steerAngle >= turnRadius/2)
        {
            driveMode = DriveMode.Drift;
        }
        else if (wheelFR.steerAngle < turnRadius/2 && Input.GetAxis("Vertical") > 0)
        {
            driveMode = DriveMode.Front;
        }
        else if ((wheelFR.steerAngle < turnRadius/2 && Input.GetAxis("Vertical") < 0))
        {
            driveMode = DriveMode.Rear;
        }

        if (driveMode == DriveMode.Rear)
        {
            wheelFR.motorTorque = scaledTorque;
            wheelFL.motorTorque = scaledTorque;
            m_particleSystem.Stop();
            m_emission.enabled = false;
        }
        if (driveMode == DriveMode.Front)
        {
            wheelBR.motorTorque = scaledTorque;
            wheelBL.motorTorque = scaledTorque;
            m_particleSystem.Stop();
            m_emission.enabled = false;
        }
        if (driveMode == DriveMode.Drift)
        {
            wheelFR.motorTorque = scaledTorque * 3;
            wheelFL.motorTorque = scaledTorque * 3;
            wheelBR.motorTorque = scaledTorque/2;
            wheelBL.motorTorque = scaledTorque/2;
            wheelBR.brakeTorque = brakeTorque;
            wheelBL.brakeTorque = brakeTorque;            

            m_particleSystem.Play();
            m_emission.enabled = true;
        }

        WheelBehaviour(wheelBR, wheelBL, wheelFR, wheelFL);

        if (Input.GetButton("Jump"))
        {
            wheelBR.brakeTorque = brakeTorque * 2;
			wheelBL.brakeTorque = brakeTorque * 2;
		}
		else
        {
			wheelFR.brakeTorque = 0;
			wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
			wheelBL.brakeTorque = 0;
		}
	}


	void WheelBehaviour(WheelCollider WheelBL, WheelCollider WheelBR, WheelCollider WheelFL, WheelCollider WheelFR)
    {
		WheelHit hit;

        Vector3 localPosition = transform.localPosition;
		
		bool groundedBL = WheelBL.GetGroundHit(out hit);
        bool groundedBR = WheelBR.GetGroundHit(out hit);
        bool groundedFL = WheelFL.GetGroundHit(out hit);
        bool groundedFR = wheelFR.GetGroundHit(out hit);
		
		if (groundedBL)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                //rigidbody.AddForceAtPosition(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * acceleration, WheelBL.transform.position);
                rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * acceleration, ForceMode.Acceleration);

                if (Input.GetAxis("Horizontal") > 0)
                {
                    //rigidbody.AddRelativeForce(new Vector3(transform.forward.x, 0, 0) * acceleration, ForceMode.Acceleration);
                    rigidbody.AddForceAtPosition(new Vector3(transform.forward.x, 0, Mathf.Abs(transform.forward.z)) * acceleration, WheelBL.transform.position);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    rigidbody.AddForceAtPosition(new Vector3(-transform.forward.x, 0, Mathf.Abs(transform.forward.z)) * acceleration, WheelBL.transform.position);
                }
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                rigidbody.AddForceAtPosition(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * -acceleration, WheelBL.transform.position);
                //rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * -acceleration, ForceMode.Acceleration);
            }            
        }			
        else if (!groundedBL)
        {
            timeCounter += Time.deltaTime;
            rigidbody.AddForceAtPosition(WheelBL.transform.up * -gravity, WheelBL.transform.position);
            
            //add counter to reset the car position
        }
        if (groundedBR)
        {
            if (Input.GetAxis("Vertical") > 0)
            {                
                //rigidbody.AddForceAtPosition(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * acceleration, WheelBR.transform.position);
                rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * acceleration, ForceMode.Force);

                if (Input.GetAxis("Horizontal") > 0)
                {
                    //rigidbody.AddRelativeForce(new Vector3(transform.forward.x, 0, 0) * acceleration, ForceMode.Acceleration);
                    rigidbody.AddForceAtPosition(new Vector3(transform.forward.x, 0, Mathf.Abs(transform.forward.z)) * acceleration, WheelBR.transform.position);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    //rigidbody.AddRelativeForce(new Vector3(-transform.forward.x, 0, 0) * acceleration, ForceMode.Acceleration);
                    rigidbody.AddForceAtPosition(new Vector3(-transform.forward.x, 0, Mathf.Abs(transform.forward.z)) * acceleration, WheelBR.transform.position);
                }
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * -acceleration, ForceMode.Acceleration);
            }
            
        }			     
        else if (!groundedBR)
        {
            timeCounter += Time.deltaTime;
            rigidbody.AddForceAtPosition(WheelBR.transform.up * -gravity, WheelBR.transform.position);
            //add counter to reset the car position
        }
        /*if (!groundedFL)
        {
            rigidbody.AddForceAtPosition(WheelFL.transform.up * -gravity, WheelFL.transform.position);
        }
        if (!groundedFR)
        {
            rigidbody.AddForceAtPosition(WheelFR.transform.up * -gravity, WheelFR.transform.position);
        }*/
    }
    public void ResetPosition()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Turbo")
        {            
            rigidbody.AddRelativeForce(new Vector3(0, 0, transform.forward.z) * acceleration, ForceMode.VelocityChange);
        }
    }

}
