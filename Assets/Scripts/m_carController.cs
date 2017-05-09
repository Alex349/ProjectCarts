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

    public float baseAcc;
    public float currentAcc;
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

    private float cameraSpeed = 0.01f;
    public float AntiRoll = 1000f;

    public WheelFrictionCurve wheelBLDriftFriction;
    public WheelFrictionCurve wheelBRDriftFriction;
    public WheelFrictionCurve wheelFRDriftFriction;
    public WheelFrictionCurve wheelFLDriftFriction;

    public float driftForce = 10f;
    public float currentSpeed;
    private float driftCounter = 2f;
    private m_carHUD m_hud;

    private bool isSlowingDown = false;
    public MeshRenderer carMesh;
    public MeshRenderer wheelBLMesh, wheelBRMesh, wheelFRMesh, wheelFLMesh;
    public bool Drifting;

    void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        m_hud = FindObjectOfType<m_carHUD>();

        m_rigidbody = GetComponent<Rigidbody>();
		m_rigidbody.centerOfMass = centerOfGravity.localPosition;

        m_particleSystem1 = wheelBL.GetComponent<ParticleSystem>();
        m_particleSystem2 = wheelBR.GetComponent<ParticleSystem>();

        currentAcc = 0;
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
		
        if (m_hud.StartRace == true)
        {
            scaledTorque = Input.GetAxis("Vertical") * torque * currentAcc;

            if (wheelBL.rpm < g_RPM)
            {
                scaledTorque = Mathf.Lerp(scaledTorque / 10, scaledTorque, wheelBL.rpm / g_RPM);
            }
            else
            {
                scaledTorque = Mathf.Lerp(scaledTorque, 0, (wheelBL.rpm - g_RPM) / (max_RPM - g_RPM));
            }
            if (currentSpeed >= maxSpeed)
            {
                currentAcc = 0;
            }
            else if (currentSpeed < maxSpeed)
            {
                currentAcc = baseAcc;
            }
        }
        

        wheelFR.steerAngle = Input.GetAxis("Horizontal") * turnRadius;
		wheelFL.steerAngle = Input.GetAxis("Horizontal") * turnRadius;

        Stabilizer(wheelBL, wheelBR, wheelFL, wheelFR);  
              
        if (Input.GetAxis("Vertical") > 0)
        {
            driveMode = DriveMode.Front;
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * currentAcc, ForceMode.Acceleration);

            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            driveMode = DriveMode.Rear;
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, -Mathf.Abs(transform.forward.z)).normalized * currentAcc, ForceMode.Acceleration);

            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;
        }
        else if (Input.GetAxis("Vertical") == 0 && currentSpeed != 0)
        {
            slowDownCounter += Time.deltaTime;
            isSlowingDown = true;              

            if (slowDownCounter > 1f)
            {                
                if (driveMode == DriveMode.Front)
                {
                    m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * -10, ForceMode.Acceleration);

                    if (currentSpeed <= 1f)
                    {
                        slowDownCounter = 0;
                        isSlowingDown = false;

                    }
                }
                else if (driveMode == DriveMode.Rear)
                {
                    m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * 10, ForceMode.Acceleration);

                    if (currentSpeed >= -1f)
                    {
                        slowDownCounter = 0;
                        isSlowingDown = false;

                    }
                }                 
            }            
        }
        if ((Input.GetKey("space") || Input.GetButton("Drift")) && 
            (Input.GetAxis("Horizontal") < -0.5f || Input.GetAxis("Horizontal") > 0.5f))
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
            wheelBR.motorTorque = scaledTorque * 2;
            wheelBL.motorTorque = scaledTorque * 2;

            wheelBRDriftFriction = wheelBR.sidewaysFriction;
            wheelBRDriftFriction.extremumValue = 30;
            wheelBRDriftFriction.asymptoteValue = 30;
            wheelBR.sidewaysFriction = wheelBRDriftFriction;

            wheelBLDriftFriction = wheelBL.sidewaysFriction;
            wheelBLDriftFriction.extremumValue = 30;
            wheelBLDriftFriction.asymptoteValue = 30;
            wheelBL.sidewaysFriction = wheelBLDriftFriction;

            wheelFLDriftFriction = wheelFL.sidewaysFriction;
            wheelFLDriftFriction.extremumValue = 30;
            wheelFLDriftFriction.asymptoteValue = 30;
            wheelFL.sidewaysFriction = wheelFLDriftFriction;

            wheelFRDriftFriction = wheelFR.sidewaysFriction;
            wheelFRDriftFriction.extremumValue = 30;
            wheelFRDriftFriction.asymptoteValue = 30;
            wheelFR.sidewaysFriction = wheelFRDriftFriction;

            driftCounter = 2f;

            Drifting = false;

            if (currentSpeed >= 1 && currentSpeed <= maxSpeed / 4)
            {
                turnRadius = 5;
            }
            else
            {
                turnRadius = 2.5f;
            }
        }
        if (driveMode == DriveMode.Rear)
        {
            wheelBR.motorTorque = scaledTorque;
            wheelBL.motorTorque = scaledTorque;

            wheelBRDriftFriction = wheelBR.sidewaysFriction;
            wheelBRDriftFriction.extremumValue = 30;
            wheelBRDriftFriction.asymptoteValue = 30;
            wheelBR.sidewaysFriction = wheelBRDriftFriction;

            wheelBLDriftFriction = wheelBL.sidewaysFriction;
            wheelBLDriftFriction.extremumValue = 30;
            wheelBLDriftFriction.asymptoteValue = 30;
            wheelBL.sidewaysFriction = wheelBLDriftFriction;

            wheelFLDriftFriction = wheelFL.sidewaysFriction;
            wheelFLDriftFriction.extremumValue = 30;
            wheelFLDriftFriction.asymptoteValue = 30;
            wheelFL.sidewaysFriction = wheelFLDriftFriction;

            wheelFRDriftFriction = wheelFR.sidewaysFriction;
            wheelFRDriftFriction.extremumValue = 30;
            wheelFRDriftFriction.asymptoteValue = 30;
            wheelFR.sidewaysFriction = wheelFRDriftFriction;
        
            driftCounter = 2f;

            Drifting = false;

            if (currentSpeed <= -1 && currentSpeed >= -maxSpeed / 4)
            {
                turnRadius = 5;
            }
            else
            {
                turnRadius = 2.5f;
            }
        }

        if (driveMode == DriveMode.Drift)
        {            
            DriftBehaviour(wheelBL, wheelBR, wheelFL, wheelFR);

            Drifting = true;

            driftCounter -= Time.deltaTime;

            if (driftCounter <= 0 && Input.GetKey("left shift"))
            {
                m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * miniTurboForce, ForceMode.Acceleration);
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

        if (groundedBL && groundedBR)
        {
            WheelBL.motorTorque = scaledTorque;
            WheelFL.motorTorque = 0;
            WheelBR.motorTorque = scaledTorque;
            WheelFR.motorTorque = 0;

            if (Input.GetAxis("Vertical") > 0)
            {              
                //m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * driftForce, ForceMode.Force);

                if (Input.GetAxis("Horizontal") > 0.5f)
                {
                    wheelBLDriftFriction = WheelBL.sidewaysFriction;
                    wheelBLDriftFriction.extremumValue = 1;
                    wheelBLDriftFriction.asymptoteValue = 1;
                    WheelBL.sidewaysFriction = wheelBLDriftFriction;

                    wheelBRDriftFriction = WheelBR.sidewaysFriction;
                    wheelBRDriftFriction.extremumValue = 1;
                    wheelBRDriftFriction.asymptoteValue = 1;
                    WheelBR.sidewaysFriction = wheelBRDriftFriction;

                    wheelFLDriftFriction = WheelFL.sidewaysFriction;
                    wheelFLDriftFriction.extremumValue = 1;
                    wheelFLDriftFriction.asymptoteValue = 1;
                    WheelFL.sidewaysFriction = wheelFLDriftFriction;                    

                    wheelFRDriftFriction = WheelFR.sidewaysFriction;
                    wheelFRDriftFriction.extremumValue = 1;
                    wheelFRDriftFriction.asymptoteValue = 1;
                    WheelFR.sidewaysFriction = wheelFRDriftFriction;

                    m_particleSystem1.Play();
                    m_particleSystem2.Stop();
                    //m_rigidbody.AddRelativeForce(new Vector3(-Mathf.Abs(transform.forward.x), 0, 0).normalized * driftForce, ForceMode.Acceleration);
                    m_rigidbody.AddForceAtPosition(new Vector3(transform.forward.x, 0, 0) * driftForce,
                                                   transform.position, ForceMode.Acceleration);                    
                }
                else if (Input.GetAxis("Horizontal") < -0.5)
                {
                    wheelBLDriftFriction = wheelBL.sidewaysFriction;
                    wheelBLDriftFriction.extremumValue = 1;
                    wheelBLDriftFriction.asymptoteValue = 1;
                    wheelBL.sidewaysFriction = wheelBLDriftFriction;

                    wheelBRDriftFriction = WheelBR.sidewaysFriction;
                    wheelBRDriftFriction.extremumValue = 1;
                    wheelBRDriftFriction.asymptoteValue = 1;
                    WheelBR.sidewaysFriction = wheelBRDriftFriction;

                    wheelFLDriftFriction = WheelFL.sidewaysFriction;
                    wheelFLDriftFriction.extremumValue = 1;
                    wheelFLDriftFriction.asymptoteValue = 1;
                    WheelFL.sidewaysFriction = wheelFLDriftFriction;                   

                    wheelFRDriftFriction = WheelFR.sidewaysFriction;
                    wheelFRDriftFriction.extremumValue = 1;
                    wheelFRDriftFriction.asymptoteValue = 1;
                    WheelFR.sidewaysFriction = wheelFRDriftFriction;

                    m_particleSystem1.Stop();
                    m_particleSystem2.Play();

                    m_rigidbody.AddForceAtPosition(new Vector3(-transform.forward.x, 0, 0) * driftForce, 
                                                   transform.position, ForceMode.Impulse);                    
                }
                //transform.rotation = new Quaternion(transform.rotation.x, Mathf.Lerp(transform.localRotation.y, 
                //                                    transform.localRotation.y + (10 * Input.GetAxis("Horizontal")), cameraSpeed * 5), 
                //                                    transform.rotation.z, transform.rotation.w);

              
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

                Vector3 distanceToPoint = transform.position - hit.point;

                if (distanceToPoint.magnitude <= 5)
                {
                    gravity = gravity * 3;
                }
                else
                {
                    gravity = 5;
                }           
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
