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
    public float frontMaxSpeed = 100f;
    public float rearMaxSpeed = 100f;
    private float maxSpeed;
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

    private WheelFrictionCurve wheelBLDriftFriction;
    private WheelFrictionCurve wheelBRDriftFriction;
    private WheelFrictionCurve wheelFRDriftFriction;
    private WheelFrictionCurve wheelFLDriftFriction;

    private WheelFrictionCurve wheelBLFrontFriction;
    private WheelFrictionCurve wheelBRFrontFriction;
    private WheelFrictionCurve wheelFLFrontFriction;
    private WheelFrictionCurve wheelFRFrontFriction;

    private float wheelFRDamp, wheelFLDamp, wheelBRDamp, wheelBLDamp;

    private float driftForce;
    public float currentSpeed;
    private float driftCounter = 2f;
    private m_carHUD m_hud;

    private bool isSlowingDown = false;
    public bool Drifting;
    private float driftDelay = 1f;
    public bool leftDrift, rightDrift;
    public LayerMask floor;
    public float baseDriftForce;

    private Vector3 driftFrwd;
    private float stifness = 0;
    //GameObject fire1L, fire2L, fire3L, fire4L, fire1R, fire2R, fire3R, fire4R;
    public ParticleSystem Sfire1L, Sfire2L, Sfire3L, Sfire4L, Sfire1R, Sfire2R, Sfire3R, Sfire4R;

    private bool isSpaceDown = false;
    private bool isSpaceJustUp = false;

    public float rotationSpeed;

    public float slowDownForce;

    void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        m_hud = FindObjectOfType<m_carHUD>();

        m_rigidbody = GetComponentInChildren<Rigidbody>();
		m_rigidbody.centerOfMass = centerOfGravity.localPosition;

        m_particleSystem1 = wheelBL.GetComponent<ParticleSystem>();
        m_particleSystem2 = wheelBR.GetComponent<ParticleSystem>();

        currentAcc = 0;
        driftForce = baseDriftForce;
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

    void Update()
    {
        isSpaceDown = Input.GetKey("space");
        isSpaceJustUp = Input.GetKeyUp("space");
    }

	void FixedUpdate ()
    {    
        m_rigidbody.AddRelativeForce(new Vector3(0, Mathf.Abs(m_rigidbody.transform.forward.y), 0).normalized * -gravity, ForceMode.Acceleration);   
		
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

        currentSpeed = m_rigidbody.velocity.magnitude;

        if (Input.GetAxis("Vertical") > 0 && !Drifting)
        {
            driveMode = DriveMode.Front;
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * currentAcc, ForceMode.Acceleration);

            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;

            Sfire1L.Play();
            Sfire1R.Play();
            Sfire3L.Play();
            Sfire3R.Play();
        }
        if (Input.GetAxis("Vertical") < 0 && !Drifting)
        {
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, -Mathf.Abs(transform.forward.z)).normalized * currentAcc, ForceMode.Acceleration);

            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;

            Sfire1L.Stop();
            Sfire1R.Stop();
            Sfire3L.Stop();
            Sfire3R.Stop();

            if (driveMode == DriveMode.Front && currentSpeed >= 0.5f)
            {
                driveMode = DriveMode.Front;
            }
            else
            {
                driveMode = DriveMode.Rear;
            }
            
        }       

        else if (Input.GetAxis("Vertical") == 0 && currentSpeed > 0.1f)
        {
            Sfire1L.Stop();
            Sfire1R.Stop();
            Sfire3L.Stop();
            Sfire3R.Stop();

            if (driveMode == DriveMode.Front)
            {               
                if (currentSpeed <= 1f)
                {
                    wheelFR.brakeTorque = brakeTorque;
                    wheelFL.brakeTorque = brakeTorque;
                    wheelBR.brakeTorque = brakeTorque;
                    wheelBL.brakeTorque = brakeTorque;
                }
                else
                {
                    m_rigidbody.AddRelativeForce(new Vector3(0, 0, -Mathf.Abs(transform.forward.z)) * slowDownForce, ForceMode.Force);
                }
                
            }
            else if (driveMode == DriveMode.Rear)
            {             
                if (currentSpeed <= 1f)
                {
                    wheelFR.brakeTorque = brakeTorque;
                    wheelFL.brakeTorque = brakeTorque;
                    wheelBR.brakeTorque = brakeTorque;
                    wheelBL.brakeTorque = brakeTorque;
                }
                else
                {
                    m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * slowDownForce, ForceMode.Force);
                    
                }
            }
        }
        //Debug.Log("SPACE:"+Input.GetKey("space") +", DRIFT: " + Input.GetButton("Drift")+", HORI:" +Input.GetAxis("Horizontal"));
        //drift
        if ((isSpaceDown || Input.GetButton("Drift")) && (Input.GetAxis("Horizontal") < -0.5f || Input.GetAxis("Horizontal") > 0.5f))
        {
            //.Log("IN");                           
            if (!Drifting && driftDelay >= 0.9f)
            {
                driftDelay = 0;
                m_rigidbody.AddForceAtPosition(Vector3.up * 150, m_rigidbody.transform.position, ForceMode.Acceleration);

                if (Input.GetAxis("Horizontal") < -0.5f)
                {
                    m_rigidbody.transform.Rotate(Vector3.up, Mathf.Lerp(m_rigidbody.transform.rotation.y, 
                                                m_rigidbody.transform.rotation.y - 40f, rotationSpeed * Time.deltaTime));
                    leftDrift = true;
                    rightDrift = false;
                    stifness = 0;
                    driveMode = DriveMode.Drift;
                }
                else if (Input.GetAxis("Horizontal") > 0.5f)
                {
                    m_rigidbody.transform.Rotate(Vector3.up, Mathf.Lerp(m_rigidbody.transform.rotation.y, 
                                                 m_rigidbody.transform.rotation.y + 40f, rotationSpeed * Time.deltaTime));
                    rightDrift = true;
                    leftDrift = false;
                    stifness = 0;
                    driveMode = DriveMode.Drift;
                }               
            }            
        }
        else if (isSpaceJustUp || Input.GetButtonUp("Drift"))
        {            
            driveMode = DriveMode.Front;
            Drifting = false;
            rightDrift = false;
            leftDrift = false;
        }
        
        //Debug.Log(Drifting);
        
        if (driveMode == DriveMode.Front)
        {
            maxSpeed = frontMaxSpeed;

            wheelBR.motorTorque = scaledTorque;
            wheelBL.motorTorque = scaledTorque;
            wheelFR.motorTorque = scaledTorque;
            wheelFL.motorTorque = scaledTorque;

            wheelBLFrontFriction = wheelBL.forwardFriction;

            if (wheelBLFrontFriction.stiffness < 0.99f)
            {
                wheelBLFrontFriction = wheelBL.forwardFriction;
                wheelBLFrontFriction.stiffness = wheelBLFrontFriction.stiffness + Time.deltaTime;
                wheelBL.forwardFriction = wheelBLFrontFriction;

                wheelBRFrontFriction = wheelBR.forwardFriction;
                wheelBRFrontFriction.stiffness = wheelBRFrontFriction.stiffness + Time.deltaTime;
                wheelBR.forwardFriction = wheelBRFrontFriction;

                wheelFLFrontFriction = wheelFL.forwardFriction;
                wheelFLFrontFriction.stiffness = wheelFLFrontFriction.stiffness + Time.deltaTime;
                wheelFL.forwardFriction = wheelFLFrontFriction;

                wheelFRFrontFriction = wheelFR.forwardFriction;
                wheelFRFrontFriction.stiffness = wheelFRFrontFriction.stiffness + Time.deltaTime;
                wheelFR.forwardFriction = wheelFRFrontFriction;

            }
            else
            {
                wheelBLFrontFriction = wheelBL.forwardFriction;
                wheelBLFrontFriction.stiffness = 1;
                wheelBL.forwardFriction = wheelBLFrontFriction;

                wheelBRFrontFriction = wheelBR.forwardFriction;
                wheelBRFrontFriction.stiffness = 1;
                wheelBR.forwardFriction = wheelBRFrontFriction;

                wheelFLFrontFriction = wheelFL.forwardFriction;
                wheelFLFrontFriction.stiffness = 1;
                wheelFL.forwardFriction = wheelFLFrontFriction;

                wheelFRFrontFriction = wheelFR.forwardFriction;
                wheelFRFrontFriction.stiffness = 1;
                wheelFR.forwardFriction = wheelFRFrontFriction;
            }            

            wheelBRDriftFriction = wheelBR.sidewaysFriction;

            if (wheelBLDriftFriction.stiffness < 0.99f)
            {
                wheelBRDriftFriction = wheelBR.sidewaysFriction;
                wheelBRDriftFriction.stiffness = wheelBRDriftFriction.stiffness + Time.deltaTime;
                wheelBR.sidewaysFriction = wheelBRDriftFriction;

                wheelBLDriftFriction = wheelBL.sidewaysFriction;
                wheelBLDriftFriction.stiffness = wheelBLDriftFriction.stiffness + Time.deltaTime;
                wheelBL.sidewaysFriction = wheelBLDriftFriction;

                wheelFLDriftFriction = wheelFL.sidewaysFriction;
                wheelFLDriftFriction.stiffness = wheelFLDriftFriction.stiffness + Time.deltaTime;
                wheelFL.sidewaysFriction = wheelFLDriftFriction;

                wheelFRDriftFriction = wheelFR.sidewaysFriction;
                wheelFRDriftFriction.stiffness = wheelFRDriftFriction.stiffness + Time.deltaTime;
                wheelFR.sidewaysFriction = wheelFRDriftFriction;
            }

            else
            {
                wheelBRDriftFriction = wheelBR.sidewaysFriction;
                wheelBRDriftFriction.stiffness = 1;
                wheelBR.sidewaysFriction = wheelBRDriftFriction;

                wheelBLDriftFriction = wheelBL.sidewaysFriction;
                wheelBLDriftFriction.stiffness = 1;
                wheelBL.sidewaysFriction = wheelBLDriftFriction;

                wheelFLDriftFriction = wheelFL.sidewaysFriction;
                wheelFLDriftFriction.stiffness = 1;
                wheelFL.sidewaysFriction = wheelFLDriftFriction;

                wheelFRDriftFriction = wheelFR.sidewaysFriction;
                wheelFRDriftFriction.stiffness = 1;
                wheelFR.sidewaysFriction = wheelFRDriftFriction;
            }

            wheelBLDamp = wheelBL.wheelDampingRate;
            wheelBLDamp = 0.5f;
            wheelBL.wheelDampingRate = wheelBLDamp;

            wheelBRDamp = wheelBR.wheelDampingRate;
            wheelBRDamp = 0.5f;
            wheelBR.wheelDampingRate = wheelBRDamp;

            wheelFLDamp = wheelFL.wheelDampingRate;
            wheelFLDamp = 0.5f;
            wheelFL.wheelDampingRate = wheelFLDamp;

            wheelFRDamp = wheelFR.wheelDampingRate;
            wheelFRDamp = 0.5f;
            wheelFR.wheelDampingRate = wheelFRDamp;

            driftCounter = 2f;
            driftDelay = 1;

            Drifting = false;
            rightDrift = false;
            leftDrift = false;

            if (currentSpeed >= 1 && currentSpeed <= maxSpeed / 2)
            {
                turnRadius = 8;
            }
            else
            {
                turnRadius = 4f;
            }
        }
        if (driveMode == DriveMode.Rear)
        {           
            maxSpeed = rearMaxSpeed;
       
            wheelBR.motorTorque = scaledTorque;
            wheelBL.motorTorque = scaledTorque;
            wheelFR.motorTorque = scaledTorque;
            wheelFL.motorTorque = scaledTorque;

            wheelBRDriftFriction = wheelBR.sidewaysFriction;
            wheelBRDriftFriction.stiffness = 1;
            wheelBR.sidewaysFriction = wheelBRDriftFriction;

            wheelBLDriftFriction = wheelBL.sidewaysFriction;
            wheelBLDriftFriction.stiffness = 1;
            wheelBL.sidewaysFriction = wheelBLDriftFriction;

            wheelFLDriftFriction = wheelFL.sidewaysFriction;
            wheelFLDriftFriction.stiffness = 1;
            wheelFL.sidewaysFriction = wheelFLDriftFriction;

            wheelFRDriftFriction = wheelFR.sidewaysFriction;
            wheelFRDriftFriction.stiffness = 1;
            wheelFR.sidewaysFriction = wheelFRDriftFriction;

            wheelBLDriftFriction = wheelBL.forwardFriction;
            wheelBLFrontFriction.stiffness = 1;
            wheelBL.forwardFriction = wheelBLFrontFriction;

            wheelBRFrontFriction = wheelBR.forwardFriction;
            wheelBRFrontFriction.stiffness = 1;
            wheelBR.forwardFriction = wheelBRFrontFriction;

            wheelFLFrontFriction = wheelFL.forwardFriction;
            wheelFLFrontFriction.stiffness = 1;
            wheelFL.forwardFriction = wheelFLFrontFriction;

            wheelFRFrontFriction = wheelFR.forwardFriction;
            wheelFRFrontFriction.stiffness = 1;
            wheelFR.forwardFriction = wheelFRFrontFriction;

            driftCounter = 2;
            driftDelay = 1;

            Drifting = false;
            rightDrift = false;
            leftDrift = false;

            turnRadius = 15;
        }

        if (driveMode == DriveMode.Drift)
        {
            Drifting = true;

            wheelBLFrontFriction = wheelBL.forwardFriction;
            wheelBLFrontFriction.stiffness = 1;
            wheelBL.forwardFriction = wheelBLFrontFriction;

            wheelBRFrontFriction = wheelBR.forwardFriction;
            wheelBRFrontFriction.stiffness = 1;
            wheelBR.forwardFriction = wheelBRFrontFriction;

            wheelFLFrontFriction = wheelFL.forwardFriction;
            wheelFLFrontFriction.stiffness = 1;
            wheelFL.forwardFriction = wheelFLFrontFriction;

            wheelFRFrontFriction = wheelFR.forwardFriction;
            wheelFRFrontFriction.stiffness = 1;
            wheelFR.forwardFriction = wheelFRFrontFriction;

            stifness += Time.deltaTime;

            if (stifness >= 0.2f)
            {
                stifness = 0.2f;
            }
            driftFrwd = m_rigidbody.transform.right;

            DriftBehaviour(wheelBL, wheelBR, wheelFL, wheelFR);           
            
            driftCounter -= Time.deltaTime;
           
            if (driftCounter <= 0 && Input.GetKey("left shift"))
            {
                m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(m_rigidbody.transform.forward.z)) * miniTurboForce, ForceMode.Acceleration);

                driftCounter = 2f;
                Drifting = false;
                driveMode = DriveMode.Front;
            }
            if (rightDrift)
            {
                Sfire1R.Play();
                Sfire2R.Play();
                Sfire3R.Play();
                Sfire4R.Play();

                Sfire1L.Stop();
                Sfire2L.Stop();
                Sfire3L.Stop();
                Sfire4L.Stop();

                if (Input.GetAxis("Horizontal") == -1)
                {
                    m_rigidbody.AddRelativeForce((m_rigidbody.transform.forward * 2 - driftFrwd) * driftForce, ForceMode.Force);
                    Debug.DrawRay(m_rigidbody.transform.position, (m_rigidbody.transform.forward * 2 - driftFrwd) * driftForce, Color.green);

                    m_rigidbody.transform.Rotate(m_rigidbody.transform.up, Mathf.Lerp(m_rigidbody.transform.rotation.y, 
                                                                                      m_rigidbody.transform.rotation.y - 10, 0.1f));
                }               
            }
            else if (leftDrift)
            {
                Sfire1L.Play();
                Sfire2L.Play();
                Sfire3L.Play();
                Sfire4L.Play();

                Sfire1R.Stop();
                Sfire2R.Stop();
                Sfire3R.Stop();
                Sfire4R.Stop();

                if (Input.GetAxis("Horizontal") == 1)
                {
                    m_rigidbody.AddRelativeForce((m_rigidbody.transform.forward + driftFrwd) * driftForce, ForceMode.Force);
                    Debug.DrawRay(m_rigidbody.transform.position, (m_rigidbody.transform.forward * 2 + driftFrwd) * driftForce, Color.magenta);

                    m_rigidbody.transform.Rotate(m_rigidbody.transform.up, Mathf.Lerp(m_rigidbody.transform.rotation.y, 
                                                                                      m_rigidbody.transform.rotation.y + 10, 0.1f));
                }                
            }            
        }
        
        WheelHit hit;
        RaycastHit hitFloor1;

        bool groundedWheel = wheelBL.GetGroundHit(out hit);
        bool groundedWheel2 = wheelBR.GetGroundHit(out hit);

        if ((!groundedWheel && !groundedWheel2) && (!isSpaceDown && !isSpaceJustUp))
        {
            if (Physics.Raycast(transform.position, -m_rigidbody.transform.up, out hitFloor1, 10f))
            {
                gravity = 5;

                wheelBLDamp = wheelBL.wheelDampingRate;
                wheelBLDamp = 3f;
                wheelBL.wheelDampingRate = wheelBLDamp;

                wheelBRDamp = wheelBR.wheelDampingRate;
                wheelBRDamp = 3f;
                wheelBR.wheelDampingRate = wheelBRDamp;

                wheelFLDamp = wheelFL.wheelDampingRate;
                wheelFLDamp = 3f;
                wheelFL.wheelDampingRate = wheelFLDamp;

                wheelFRDamp = wheelFR.wheelDampingRate;
                wheelFRDamp = 3f;
                wheelFR.wheelDampingRate = wheelFRDamp;

               //wheelBLDriftFriction = wheelBL.forwardFriction;
               //wheelBLFrontFriction.stiffness = 0;
               //wheelBL.forwardFriction = wheelBLFrontFriction;
               //
               //wheelBRFrontFriction = wheelBR.forwardFriction;
               //wheelBRFrontFriction.stiffness = 0;
               //wheelBR.forwardFriction = wheelBRFrontFriction;
               //
               //wheelFLFrontFriction = wheelFL.forwardFriction;
               //wheelFLFrontFriction.stiffness = 0;
               //wheelFL.forwardFriction = wheelFLFrontFriction;
               //
               //wheelFRFrontFriction = wheelFR.forwardFriction;
               //wheelFRFrontFriction.stiffness = 0;
               //wheelFR.forwardFriction = wheelFRFrontFriction;
            }
            else
            {
                gravity = 15;

                wheelBLDamp = wheelBL.wheelDampingRate;
                wheelBLDamp = 4f;
                wheelBL.wheelDampingRate = wheelBLDamp;

                wheelBRDamp = wheelBR.wheelDampingRate;
                wheelBRDamp = 4f;
                wheelBR.wheelDampingRate = wheelBRDamp;

                wheelFLDamp = wheelFL.wheelDampingRate;
                wheelFLDamp = 6f;
                wheelFL.wheelDampingRate = wheelFLDamp;

                wheelFRDamp = wheelFR.wheelDampingRate;
                wheelFRDamp = 6f;
                wheelFR.wheelDampingRate = wheelFRDamp;

                wheelBLDriftFriction = wheelBL.forwardFriction;
                wheelBLFrontFriction.stiffness = 0;
                wheelBL.forwardFriction = wheelBLFrontFriction;

                wheelBRFrontFriction = wheelBR.forwardFriction;
                wheelBRFrontFriction.stiffness = 0;
                wheelBR.forwardFriction = wheelBRFrontFriction;

                wheelFLFrontFriction = wheelFL.forwardFriction;
                wheelFLFrontFriction.stiffness = 0;
                wheelFL.forwardFriction = wheelFLFrontFriction;

                wheelFRFrontFriction = wheelFR.forwardFriction;
                wheelFRFrontFriction.stiffness = 0;
                wheelFR.forwardFriction = wheelFRFrontFriction;
            }
            

            timeCounter += Time.deltaTime;
            if (timeCounter >= 3)
            {
                ResetPosition();
                timeCounter = 0;
            }
        }
        else if (groundedWheel)
        {
            timeCounter = 0;
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

        driftFrwd = m_rigidbody.transform.right;

        if (groundedBL && groundedBR)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                WheelFL.motorTorque = scaledTorque * 5;
                WheelFR.motorTorque = scaledTorque * 5;
                WheelBL.motorTorque = scaledTorque * 5;
                WheelBR.motorTorque = scaledTorque * 5;

                if (rightDrift)
                {                          
                    wheelBLDriftFriction = WheelBL.sidewaysFriction;
                    wheelBLDriftFriction.stiffness = stifness;                                                         
                    WheelBL.sidewaysFriction = wheelBLDriftFriction;

                    wheelBRDriftFriction = WheelBR.sidewaysFriction;
                    wheelBRDriftFriction.stiffness = 0;                    
                    WheelBR.sidewaysFriction = wheelBRDriftFriction;

                    wheelFLDriftFriction = WheelFL.sidewaysFriction;
                    wheelFLDriftFriction.stiffness = stifness;
                    WheelFL.sidewaysFriction = wheelFLDriftFriction;                    

                    wheelFRDriftFriction = WheelFR.sidewaysFriction;
                    wheelFRDriftFriction.stiffness = 0;
                    WheelFR.sidewaysFriction = wheelFRDriftFriction;

                    m_particleSystem1.Play();
                    m_particleSystem2.Stop();

                    if (Input.GetAxis("Horizontal") <= 1 && Input.GetAxis("Horizontal") > 0)
                    {
                        m_rigidbody.AddRelativeForce((m_rigidbody.transform.forward * Input.GetAxis("Vertical") + 
                                                      driftFrwd * 2) * driftForce, ForceMode.Force);

                        Debug.DrawRay(m_rigidbody.transform.position, (m_rigidbody.transform.forward * Input.GetAxis("Vertical") + 
                                                                       driftFrwd * 2) * driftForce, Color.yellow);
                    }
                }

                else if (leftDrift)
                {
                    //m_rigidbody.transform.Rotate(m_rigidbody.transform.up, Mathf.Lerp(m_rigidbody.transform.rotation.y, m_rigidbody.transform.rotation.y - 10, 0.01f));

                    wheelBLDriftFriction = WheelBL.sidewaysFriction;
                    wheelBLDriftFriction.stiffness = 0;
                    WheelBL.sidewaysFriction = wheelBLDriftFriction;

                    wheelBRDriftFriction = WheelBR.sidewaysFriction;
                    wheelBRDriftFriction.stiffness = stifness;
                    WheelBR.sidewaysFriction = wheelBRDriftFriction;

                    wheelFLDriftFriction = WheelFL.sidewaysFriction;
                    wheelFLDriftFriction.stiffness = 0;
                    WheelFL.sidewaysFriction = wheelFLDriftFriction;

                    wheelFRDriftFriction = WheelFR.sidewaysFriction;
                    wheelFRDriftFriction.stiffness = stifness;
                    WheelFR.sidewaysFriction = wheelFRDriftFriction;

                    m_particleSystem1.Stop();
                    m_particleSystem2.Play();

                    if (Input.GetAxis("Horizontal") >= -1 && Input.GetAxis("Horizontal") < 0)
                    {
                        m_rigidbody.AddRelativeForce((-m_rigidbody.transform.forward * Input.GetAxis("Vertical") - 
                                                      driftFrwd) * driftForce, ForceMode.Force);

                        Debug.DrawRay(m_rigidbody.transform.position, (-m_rigidbody.transform.forward * Input.GetAxis("Vertical") - 
                                                                        driftFrwd) * driftForce, Color.black);
                    }                   
                    
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

        bool groundedBL = wheelBL.GetGroundHit(out hit);
        bool groundedBR = wheelBR.GetGroundHit(out hit);
        bool groundedFL = wheelFL.GetGroundHit(out hit);
        bool groundedFR = wheelFR.GetGroundHit(out hit);

        if (groundedBL)
            travelBL = (-wheelBL.transform.InverseTransformPoint(hit.point).y - wheelBL.radius) / wheelBL.suspensionDistance;

        if (groundedBR)
            travelBR = (-wheelBR.transform.InverseTransformPoint(hit.point).y - wheelBR.radius) / wheelBR.suspensionDistance;

        if (groundedFL)
            travelFL = (-wheelFL.transform.InverseTransformPoint(hit.point).y - wheelFL.radius) / wheelFL.suspensionDistance;

        if (groundedFR)
            travelFR = (-wheelFR.transform.InverseTransformPoint(hit.point).y - wheelFR.radius) / wheelFR.suspensionDistance;

        float antiRollForceBack = (travelBL - travelBR) * AntiRoll;
        float antiRollForceFront = (travelFL - travelFR) * AntiRoll;

        if (groundedBL)
        {
            m_rigidbody.AddForceAtPosition(wheelBL.transform.up * -antiRollForceBack, wheelBL.transform.position);
        }           
        if (groundedBR)
        {
            m_rigidbody.AddForceAtPosition(wheelBR.transform.up * antiRollForceBack, wheelBR.transform.position);
        }
        if (groundedFL)
        {
            m_rigidbody.AddForceAtPosition(wheelFL.transform.up * -antiRollForceFront, wheelFL.transform.position);
        }
        if (groundedFR)
        {
            m_rigidbody.AddForceAtPosition(wheelFR.transform.up * antiRollForceFront, wheelFR.transform.position);
        }
        else if ((!groundedFR && !groundedFL) || (!groundedBL && !groundedBR) && (!leftDrift && !rightDrift))
        {            
            if (Physics.Raycast(transform.position, -m_rigidbody.transform.up, out hitFloor, 50f))
            {
                Quaternion m_new_rotation = new Quaternion(0, m_rigidbody.transform.rotation.y, 0, 1);

                m_rigidbody.transform.rotation = new Quaternion(Mathf.Lerp(m_rigidbody.transform.rotation.x, m_new_rotation.x, 0.2f + Time.deltaTime), m_rigidbody.transform.rotation.y,
                                                                Mathf.Lerp(m_rigidbody.transform.rotation.z, m_new_rotation.z, 0.2f + Time.deltaTime), m_rigidbody.transform.rotation.w);                


                
            }
            else if (Physics.Raycast(transform.position, -m_rigidbody.transform.right, out hitFloor, 50f))
            {
                Quaternion m_new_rotation = new Quaternion(0, m_rigidbody.transform.rotation.y, 0, 1);

                m_rigidbody.transform.rotation = new Quaternion(Mathf.Lerp(m_rigidbody.transform.rotation.x, m_new_rotation.x, 0.2f + Time.deltaTime), m_rigidbody.transform.rotation.y,
                                                                Mathf.Lerp(m_rigidbody.transform.rotation.z, m_new_rotation.z, 0.2f + Time.deltaTime), m_rigidbody.transform.rotation.w);

            }          
        }                   
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Turbo")
        {
            Debug.Log("Trubo");
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(m_rigidbody.transform.forward.z)).normalized * turboForce, ForceMode.Impulse);
        }                
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "RoughFloor")
        {
            maxSpeed = 10;
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
