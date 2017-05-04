using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class m_carController : MonoBehaviour {

	public float g_RPM = 500f;
	public float max_RPM = 1000f;
    public Rigidbody m_rigidbody;

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
    public float maxSpeed = 100f;
	public enum DriveMode { Front, Rear, Drift, All };
    public float turboForce = 10f;
    public float miniTurboForce = 10f;

    public DriveMode driveMode = DriveMode.All;

    private float timeCounter, slowDownCounter;
    private float scaledTorque;

    private GameObject[] nodes;
    private Vector3 distanceToRespawnPoint;

    public Transform startPos;
    public Transform targetPosL, targetPosR;
    public Transform RearPos;
    private float cameraSpeed = 0.01f;
    public float AntiRoll = 1000f;

    public float wheelBLDriftFriction;
    public float wheelBRDriftFriction;

    public float driftForce = 10f;
    public float currentSpeed;
    private float driftCounter = 2f;

    void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");

        m_rigidbody = GetComponent<Rigidbody>();
		m_rigidbody.centerOfMass = centerOfGravity.localPosition;

        m_particleSystem1 = wheelBL.GetComponent<ParticleSystem>();
        m_particleSystem2 = wheelBR.GetComponent<ParticleSystem>();

        wheelBLDriftFriction = wheelBL.sidewaysFriction.extremumSlip;
        wheelBRDriftFriction = wheelBR.sidewaysFriction.extremumSlip;        
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
        currentSpeed = m_rigidbody.velocity.magnitude;

        m_rigidbody.AddForce(new Vector3(0, Mathf.Abs(transform.forward.y), 0).normalized * -gravity, ForceMode.Acceleration);    

		scaledTorque = Input.GetAxis("Vertical") * torque * acceleration;

		if(wheelBL.rpm < g_RPM)
        {
            scaledTorque = Mathf.Lerp(scaledTorque / 10, scaledTorque, wheelBL.rpm / g_RPM);
        }			
		else
        {
            scaledTorque = Mathf.Lerp(scaledTorque, 0, (wheelBL.rpm - g_RPM) / (max_RPM - g_RPM));
        }        			

        if (currentSpeed >= maxSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, maxSpeed / currentSpeed);
        }
        else if (currentSpeed < maxSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, currentSpeed / maxSpeed);
        }

		wheelFR.steerAngle = Input.GetAxis("Horizontal") * turnRadius;
		wheelFL.steerAngle = Input.GetAxis("Horizontal") * turnRadius;

        Stabilizer(wheelBL, wheelBR, wheelFL, wheelFR);

        
        if (Input.GetAxis("Vertical") > 0)
        {                    
            driveMode = DriveMode.Front;            
            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            driveMode = DriveMode.Rear;
            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;
        }
        else if (Input.GetAxis("Vertical") == 0 && currentSpeed != 0 && driveMode != DriveMode.Drift)
        {
            slowDownCounter += Time.deltaTime;       
            if (slowDownCounter > 1f)
            {
                currentSpeed = currentSpeed - Time.deltaTime;

                if (currentSpeed <= 0.5f)
                {
                    slowDownCounter = 0;
                }               
            }            
        }
        if (Input.GetKey("space") || Input.GetButton("Drift"))
        {
            driveMode = DriveMode.Drift;           
        }
        else if (Input.GetKeyUp("space") && Input.GetAxis("Vertical") > 0)
        {
            driveMode = DriveMode.Front;
        }
        else if (Input.GetKeyUp("space") && Input.GetAxis("Vertical") < 0)
        {
            driveMode = DriveMode.Rear;
        }

        if (driveMode == DriveMode.Front)
        {
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * acceleration, ForceMode.Force);

            wheelBLDriftFriction = 3;
            wheelBRDriftFriction = 3;

            wheelBR.motorTorque = scaledTorque;
            wheelBL.motorTorque = scaledTorque;

            driftCounter = 2f;
        }
        if (driveMode == DriveMode.Rear)
        {
            wheelBR.motorTorque = scaledTorque;
            wheelBL.motorTorque = scaledTorque;

            currentSpeed = -m_rigidbody.velocity.magnitude;
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, -Mathf.Abs(transform.forward.z)).normalized * acceleration, ForceMode.Force);

            driftCounter = 2f;
        }

        if (driveMode == DriveMode.Drift)
        {            
            DriftBehaviour(wheelBR, wheelBL, wheelFR, wheelFL);

            driftCounter -= Time.deltaTime;

            if (driftCounter <= 0 && Input.GetKey("space"))
            {
                m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * miniTurboForce, ForceMode.Force);
                Debug.Log("DriftTurbo");
                driftCounter = 2f;
            }
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
                m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * driftForce, ForceMode.Acceleration);

                if (Input.GetAxis("Horizontal") > 0)
                {
                    wheelBRDriftFriction = 5;                  
                    m_particleSystem1.Play();
                    m_rigidbody.AddRelativeForce(new Vector3(Mathf.Abs(transform.forward.x), 0, 0).normalized * driftForce, ForceMode.Acceleration);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    wheelBRDriftFriction = 1;
                    m_particleSystem1.Stop();
                    m_rigidbody.AddRelativeForce(new Vector3(-Mathf.Abs(transform.forward.x), 0, 0).normalized * driftForce, ForceMode.Acceleration);
                }
            }              
        }        
        if (groundedBR)
        {
            WheelBR.motorTorque = scaledTorque;
            if (Input.GetAxis("Vertical") > 0)
            {
                m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * driftForce, ForceMode.Acceleration);

                if (Input.GetAxis("Horizontal") > 0)
                {
                    wheelBLDriftFriction = 1;
                    m_particleSystem2.Stop();
                    m_rigidbody.AddRelativeForce(new Vector3(Mathf.Abs(transform.forward.x), 0, 0).normalized * driftForce, ForceMode.Acceleration);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    wheelBLDriftFriction = 5;
                    m_particleSystem2.Play();
                    m_rigidbody.AddRelativeForce(new Vector3(-Mathf.Abs(transform.forward.x), 0, 0).normalized * driftForce, ForceMode.Acceleration);
                }
            }                         
        }      
    }
    void Stabilizer(WheelCollider WheelBL, WheelCollider WheelBR, WheelCollider WheelFL, WheelCollider WheelFR)
    {
        WheelHit hit;
        RaycastHit hitFloor;

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
            if (Physics.Raycast(transform.position, Vector3.down, out hitFloor, 30f))
            {
                transform.rotation = new Quaternion(Mathf.Lerp(transform.rotation.x, 0, cameraSpeed * 5), transform.localRotation.y,
                                                 Mathf.Lerp(transform.rotation.z, 0, cameraSpeed * 5), transform.localRotation.w);
            }            
        }           
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Turbo")
        {
            Debug.Log("Trubo");
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * turboForce, ForceMode.VelocityChange);
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
        }
        return transform.position;
    }
}
