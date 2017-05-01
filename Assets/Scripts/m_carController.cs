using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class m_carController : MonoBehaviour {

	public float g_RPM = 500f;
	public float max_RPM = 1000f;
    private Rigidbody m_rigidbody;

    public Transform centerOfGravity;

	public WheelCollider wheelFR;
	public WheelCollider wheelFL;
	public WheelCollider wheelBR;
	public WheelCollider wheelBL;

    private ParticleSystem m_particleSystem1;
    private ParticleSystem m_particleSystem2;

    public float acceleration = 10;
    public float gravity = 9.81f;
	public float turnRadius = 6f;
	public float torque = 100f;
	public float brakeTorque = 100f;

	public enum DriveMode { Front, Rear, Drift, All };
	public DriveMode driveMode = DriveMode.All;

	public Text speedText;

    private float timeCounter, slowDownCounter;
    private float scaledTorque;

    private GameObject[] nodes;
    private Vector3 distanceToRespawnPoint;

    //public Camera m_camera;
    public Transform startPos;
    public Transform targetPosL, targetPosR;
    public Transform RearPos;
    private float cameraSpeed = 0.01f;
    public float AntiRoll = 1000f;

    public float wheelBLDriftFriction;
    public float wheelBRDriftFriction;

    //private float wheelFLFrwdFriction, wheelFRFrwdFritction;

    public float driftForce = 10f;
    public float speed = 5f;

    void Start()
    {
        //m_camera = GetComponentInChildren<Camera>();        

        //m_camera.transform.position = startPos.position;

        nodes = GameObject.FindGameObjectsWithTag("Node");

        m_rigidbody = GetComponent<Rigidbody>();
		m_rigidbody.centerOfMass = centerOfGravity.localPosition;

        m_particleSystem1 = wheelBL.GetComponent<ParticleSystem>();
        m_particleSystem2 = wheelBR.GetComponent<ParticleSystem>();

        wheelBLDriftFriction = wheelBL.sidewaysFriction.extremumSlip;
        wheelBRDriftFriction = wheelBR.sidewaysFriction.extremumSlip;

        //startTime = Time.time;
        //journeyLength = Vector3.Distance(startPos.position, targetPosL.position);
    }

	public float Speed()
    {
        //convert to km/h
		return 2 * wheelBR.radius * Mathf.PI * wheelBR.rpm * 60f / 1000f;        
	}

	public float Rpm()
    {
		return wheelBL.rpm;
	}

	void FixedUpdate ()
    {
        m_rigidbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
     
        if (speedText !=null)
			speedText.text = "Speed: " + Speed().ToString("") + " km/h";

		scaledTorque = Input.GetAxis("Vertical") * torque * acceleration;

		if(wheelBL.rpm < g_RPM)
        {
            scaledTorque = Mathf.Lerp(scaledTorque / 10, scaledTorque, wheelBL.rpm / g_RPM);
        }			
		else
        {
            scaledTorque = Mathf.Lerp(scaledTorque, 0, (wheelBL.rpm - g_RPM) / (max_RPM - g_RPM));
        }					

		wheelFR.steerAngle = Input.GetAxis("Horizontal") * turnRadius;
		wheelFL.steerAngle = Input.GetAxis("Horizontal") * turnRadius;

        Stabilizer(wheelBL, wheelBR, wheelFL, wheelFR);
        
        if (Input.GetAxis("Vertical") > 0)
        {                    
            driveMode = DriveMode.Front;            

            m_particleSystem1.Stop();
            m_particleSystem2.Stop();

            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            driveMode = DriveMode.Rear;

            m_particleSystem1.Stop();
            m_particleSystem2.Stop();

            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;
        }
        else if (Input.GetAxis("Vertical") == 0 && Speed() != 0 && driveMode != DriveMode.Drift)
        {
            slowDownCounter += Time.deltaTime;            

            if (slowDownCounter > 1f)
            {
                Debug.Log("Is braking");
                wheelFL.brakeTorque = brakeTorque;
                wheelFR.brakeTorque = brakeTorque;
                wheelBL.brakeTorque = brakeTorque;
                wheelBR.brakeTorque = brakeTorque;
                slowDownCounter = 0;
            }            
        }
        if (driveMode == DriveMode.Front)
        {
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * acceleration, ForceMode.Acceleration);

            wheelBR.motorTorque = scaledTorque;
            wheelBL.motorTorque = scaledTorque;

            if (Input.GetKeyDown("space") || Input.GetButton("Drift"))
            {
                driveMode = DriveMode.Drift;
            }
            else if (Input.GetKeyUp("space"))
            {
                driveMode = DriveMode.Front;
            }
                        
            //   Debug.Log("Wheel back left motor torque: " + wheelBL.motorTorque);
            //   Debug.Log("Wheel back right motor torque: " + wheelBR.motorTorque);
        }
        if (driveMode == DriveMode.Rear)
        {
            wheelBR.motorTorque = scaledTorque;
            wheelBL.motorTorque = scaledTorque;
            
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * -acceleration, ForceMode.Acceleration);
        }

        if (driveMode == DriveMode.Drift)
        {            
            DriftBehaviour(wheelBR, wheelBL, wheelFR, wheelFL);

        }        

        WheelHit hit;

        bool groundedWheel = wheelBL.GetGroundHit(out hit);

        if (!groundedWheel || transform.rotation.x > 30)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= 3)
            {
                ResetPosition();
                timeCounter = 0;
            }
        }
    }

	void DriftBehaviour(WheelCollider WheelBL, WheelCollider WheelBR, WheelCollider WheelFL, WheelCollider WheelFR)
    {
		WheelHit hit;
        Vector3 localPosition = transform.localPosition;        

        bool groundedBL = WheelBL.GetGroundHit(out hit);
        bool groundedBR = WheelBR.GetGroundHit(out hit);

        wheelFR.motorTorque = scaledTorque;
        wheelFL.motorTorque = scaledTorque;         

        if (groundedBL)
        {
            WheelBL.motorTorque = scaledTorque;
            if (Input.GetAxis("Vertical") > 0)
            {
                m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * driftForce, ForceMode.Acceleration);

                if (Input.GetAxis("Horizontal") > 0)
                {
                    wheelBRDriftFriction = 1;                  
                    m_particleSystem1.Play();
                    m_rigidbody.AddRelativeForce(new Vector3(Mathf.Abs(transform.forward.x), 0, 0) * driftForce, ForceMode.Acceleration);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    wheelBRDriftFriction = 5;
                    m_particleSystem1.Stop();
                    m_rigidbody.AddRelativeForce(new Vector3(-Mathf.Abs(transform.forward.x), 0, 0) * driftForce, ForceMode.Acceleration);
                }
            }              
        }        
        if (groundedBR)
        {
            WheelBR.motorTorque = scaledTorque;
            if (Input.GetAxis("Vertical") > 0)
            {
                m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * driftForce, ForceMode.Acceleration);

                if (Input.GetAxis("Horizontal") > 0)
                {
                    wheelBLDriftFriction = 1;
                    m_particleSystem2.Stop();
                    m_rigidbody.AddRelativeForce(new Vector3(Mathf.Abs(transform.forward.x), 0, 0) * driftForce, ForceMode.Acceleration);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    wheelBLDriftFriction = 5;
                    m_particleSystem2.Play();
                    m_rigidbody.AddRelativeForce(new Vector3(-Mathf.Abs(transform.forward.x), 0, 0) * driftForce, ForceMode.Acceleration);
                }
            }                         
        }      
    }
    void Stabilizer(WheelCollider WheelBL, WheelCollider WheelBR, WheelCollider WheelFL, WheelCollider WheelFR)
    {
        WheelHit hit;

        float travelFL = 1.0f;
        float travelFR = 1.0f;
        float travelBL = 1.0f;
        float travelBR = 1.0f;

        bool groundedBL = WheelBL.GetGroundHit(out hit);
        bool groundedBR = WheelBR.GetGroundHit(out hit);
        bool groundedFL = WheelBL.GetGroundHit(out hit);
        bool groundedFR = WheelBR.GetGroundHit(out hit);

        if (groundedBL)
            travelBL = (-WheelBL.transform.InverseTransformPoint(hit.point).y - WheelBL.radius) / WheelBL.suspensionDistance;

        if (groundedBR)
            travelBR = (-WheelBR.transform.InverseTransformPoint(hit.point).y - WheelBR.radius) / WheelBR.suspensionDistance;

        if (groundedFL)
            travelFL = (-WheelBL.transform.InverseTransformPoint(hit.point).y - WheelBL.radius) / WheelBL.suspensionDistance;

        if (groundedFR)
            travelFR = (-WheelBR.transform.InverseTransformPoint(hit.point).y - WheelBR.radius) / WheelBR.suspensionDistance;

        float antiRollForceBack = (travelBL - travelBR) * AntiRoll;
        float antiRollForceFront = (travelFL - travelFR) * AntiRoll;

        if (groundedBL)
        {
            m_rigidbody.AddForceAtPosition(WheelBL.transform.up * -antiRollForceBack, WheelBL.transform.position);
        }           
        if (groundedBR)
        {
            m_rigidbody.AddForceAtPosition(WheelBR.transform.up * antiRollForceBack, WheelBR.transform.position);
        }
        if (groundedFL)
        {
            m_rigidbody.AddForceAtPosition(WheelFL.transform.up * -antiRollForceFront, WheelFL.transform.position);
        }
        if (groundedFR)
        {
            m_rigidbody.AddForceAtPosition(WheelFR.transform.up * antiRollForceFront, WheelFR.transform.position);
        }
        if (!groundedFR && !groundedBL && !groundedFL && !groundedBR)
        {
            Debug.Log("Is jumping");
            transform.rotation = new Quaternion (Mathf.Lerp(transform.rotation.x, 0, cameraSpeed), transform.localRotation.y, 
                                                 Mathf.Lerp(transform.rotation.z, 0, cameraSpeed), transform.localRotation.w);
        }           
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Turbo")
        {
            if (Speed() > 30f)
            {
                m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * acceleration / 2, ForceMode.VelocityChange);
            }
            else
            {
                m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * acceleration, ForceMode.VelocityChange);
            }            
        }
        if (col.tag == "Wall")
        {
            //m_rigidbody.AddRelativeForce(new Vector3(0, 0, -Mathf.Abs(transform.forward.z)) * acceleration, ForceMode.Impulse);
        }
    }
   
    private Vector3 ResetPosition()
    {
        Vector3 respawnPosition;
        GameObject kart = gameObject;

        for (int i = 0; i < nodes.Length; i++)
        {
            distanceToRespawnPoint = nodes[i].transform.position - gameObject.transform.position;

            if (distanceToRespawnPoint.magnitude <= 30)
            {              
                respawnPosition = nodes[i].transform.position;
                Debug.Log("Is Respawning");               
                transform.position = respawnPosition;
                transform.rotation = nodes[i].transform.rotation;

            }
            /*else if (distanceToRespawnPoint.magnitude > 300) // && distanceToRespawnPoint.magnitude <= 600
            {
                respawnPosition = nodes[i].transform.position;
                Debug.Log("Is Respawning");
                transform.position = respawnPosition;
                transform.rotation = nodes[i].transform.rotation;
            }*/
        }
        return transform.position;
    }
}
