using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class m_carController : MonoBehaviour
{

    public float g_RPM;
    public float max_RPM;
    public Rigidbody m_rigidbody;

    public Transform centerOfGravity;

    public WheelCollider wheelFR;
    public WheelCollider wheelFL;
    public WheelCollider wheelBR;
    public WheelCollider wheelBL;

    public float baseAcc;
    public float currentAcc;
    public float gravity;
    public float turnRadius;
    public float torque ;
    public float brakeTorque;
    public float frontMaxSpeed;
    public float rearMaxSpeed;
    public float maxSpeed;
    public enum DriveMode {Front, Rear, Drift, Stopped, All};
    public float turboForce, startTurboForce;
    public float miniTurboForce, endDriftTurboForce;

    public DriveMode driveMode = DriveMode.All;

    private float timeCounter, slowDownCounter;
    private float scaledTorque;

    private GameObject[] nodes;
    private Vector3 distanceToRespawnPoint;

    public float AntiRoll;

    private WheelFrictionCurve wheelBLDriftFriction;
    private WheelFrictionCurve wheelBRDriftFriction;
    private WheelFrictionCurve wheelFRDriftFriction;
    private WheelFrictionCurve wheelFLDriftFriction;

    private WheelFrictionCurve wheelBLFrontFriction;
    private WheelFrictionCurve wheelBRFrontFriction;
    private WheelFrictionCurve wheelFLFrontFriction;
    private WheelFrictionCurve wheelFRFrontFriction;

    private float wheelFRDamp, wheelFLDamp, wheelBRDamp, wheelBLDamp;

    public float driftForce;
    public float currentSpeed;
    private float driftCounter = 2f;
    private float rebufoCounter = 0;
    private m_carHUD m_hud;

    public bool Drifting;
    private float driftDelay = 1f;
    public bool leftDrift, rightDrift;
    private bool isDriftingXbox = false, isDriftingXbox1 = false;

    private Vector3 driftFrwd;
    private float stifness = 0;
    public ParticleSystem[] Sfire;
    private ParticleSystem[] SubSFire = new ParticleSystem[16];

    private bool isSpaceDown = false;
    private bool isSpaceJustUp = false;

    public float rotationSpeed;
    public float slowDownForce;
    public float frontTurnRadius, rearTurnRadius, driftTurnRadius;
    public Animator m_animator;
    private int inputAcc;
    public TrailRenderer wheelBRTrail, wheelBLTrail;
    public float knockUpForce, slipperyForce;
    private bool canTurn, notDriftingXbox, canTurbo;
    public GameObject particleSystemBLWheel, particleSystemBRWheel;
    public Material sparkMaterialYellow, sparkMaterialBlue;

    void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        m_hud = FindObjectOfType<m_carHUD>();

        m_rigidbody = GetComponentInChildren<Rigidbody>();
        m_rigidbody.centerOfMass = centerOfGravity.localPosition;

        currentAcc = 0;

        wheelBLTrail.enabled = false;
        wheelBRTrail.enabled = false;

        particleSystemBLWheel.SetActive(false);
        particleSystemBRWheel.SetActive(false);
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
        isDriftingXbox = Input.GetButton("Drift");
        notDriftingXbox = Input.GetButtonUp("Drift");

        if (m_hud.StartRace == true)
        {
            if (wheelBL.rpm < g_RPM)
            {
                scaledTorque = Mathf.Lerp(scaledTorque / 10, scaledTorque, wheelBL.rpm / g_RPM);
            }
            else if (wheelBL.rpm >= g_RPM)
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

            if (Input.GetButton("Accelerate"))
            {
                inputAcc = 1;

            }
            else
            {
                inputAcc = 0;
            }
        }
        else
        {
            wheelBL.brakeTorque = brakeTorque;
            wheelBR.brakeTorque = brakeTorque;
        }        

        wheelFR.steerAngle = Input.GetAxis("Horizontal") * turnRadius;
        wheelFL.steerAngle = Input.GetAxis("Horizontal") * turnRadius;

        if (Input.GetAxis("Vertical") > 0 || inputAcc != 0)
        {
            scaledTorque = torque * currentAcc;
            Debug.Log("Scaled torque: " + scaledTorque);
            Debug.Log("Current speed: " + currentSpeed + "Current acceleration: " + currentAcc);

            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;
        }       
    }

    void FixedUpdate()
    {
        m_rigidbody.AddRelativeForce(new Vector3(0, Mathf.Abs(m_rigidbody.transform.forward.y), 0).normalized * -gravity, ForceMode.Acceleration);

        currentSpeed = m_rigidbody.velocity.magnitude;                

        Stabilizer(wheelBL, wheelBR, wheelFL, wheelFR);        

        for (int i = 0; i < Sfire.Length; i++)
        {
            for (int r = 0; r < SubSFire.Length; r++)
            {
                SubSFire = Sfire[i].GetComponentsInChildren<ParticleSystem>();
                ParticleSystem.VelocityOverLifetimeModule fireVelocity = SubSFire[r].GetComponent<ParticleSystem>().velocityOverLifetime;
                fireVelocity.xMultiplier = -currentSpeed;
                ParticleSystem.ColorOverLifetimeModule fireColor = SubSFire[r].GetComponent<ParticleSystem>().colorOverLifetime;
                fireVelocity.z = 0;                
            }

        }
        if (currentSpeed <= 0.5f)
        {
            driveMode = DriveMode.Stopped;
        }
        if (driveMode == DriveMode.Stopped)
        {
            maxSpeed = 0;
            Drifting = false;
            rightDrift = false;
            leftDrift = false;         
        }
        if ((Input.GetAxis("Vertical") > 0 || Input.GetButton("Accelerate")) && !Drifting && driveMode == DriveMode.Stopped)
        {
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * currentAcc * 5, ForceMode.Acceleration);
            
            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;

            wheelBR.motorTorque = scaledTorque;
            wheelBL.motorTorque = scaledTorque;
            wheelFR.motorTorque = scaledTorque;
            wheelFL.motorTorque = scaledTorque;

            wheelBLTrail.enabled = false;
            wheelBRTrail.enabled = false;

            driveMode = DriveMode.Front;
        }

        else if ((Input.GetAxis("Vertical") < 0 || Input.GetButton("Brake")) && !Drifting && driveMode == DriveMode.Stopped)
        {
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, -Mathf.Abs(transform.forward.z)).normalized * currentAcc * 5, ForceMode.Acceleration);

            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;

            wheelBR.motorTorque = scaledTorque;
            wheelBL.motorTorque = scaledTorque;
            wheelFR.motorTorque = scaledTorque;
            wheelFL.motorTorque = scaledTorque;

            wheelBLTrail.enabled = false;
            wheelBRTrail.enabled = false;

            driveMode = DriveMode.Rear;
        }
        else if ((Input.GetAxis("Vertical") == 0 && !Input.GetButton("Accelerate") && !Input.GetButton("Brake")) && currentSpeed > 0.5f)
        {
            if (driveMode == DriveMode.Front)
            {
                if (currentSpeed <= 2f)
                {
                    wheelFR.brakeTorque = brakeTorque;
                    wheelFL.brakeTorque = brakeTorque;
                    wheelBR.brakeTorque = brakeTorque;
                    wheelBL.brakeTorque = brakeTorque;
                    driveMode = DriveMode.Stopped;
                }
                else
                {
                    m_rigidbody.AddRelativeForce(new Vector3(0, 0, -Mathf.Abs(m_rigidbody.transform.forward.z)).normalized * slowDownForce, ForceMode.Force);
                }

            }
            else if (driveMode == DriveMode.Rear)
            {
                if (currentSpeed <= 2f)
                {
                    wheelFR.brakeTorque = brakeTorque;
                    wheelFL.brakeTorque = brakeTorque;
                    wheelBR.brakeTorque = brakeTorque;
                    wheelBL.brakeTorque = brakeTorque;
                    driveMode = DriveMode.Stopped;
                }
                else
                {
                    m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(m_rigidbody.transform.forward.z)).normalized * slowDownForce, ForceMode.Force);
                }
            }
        }

        if ((isSpaceDown || isDriftingXbox) && ((Input.GetAxis("Horizontal") < -0.5f || Input.GetAxis("Horizontal") > 0.5f) ||
            (Input.GetAxis("HorizontalXbox") < -0.5f || Input.GetAxis("HorizontalXbox") > 0.5f)) && (Input.GetAxis("Vertical") > 0 || inputAcc == 1))            
        {                       
            if (!Drifting && driftDelay >= 0.9f)
            {
                driftDelay = 0;
                m_rigidbody.AddForceAtPosition(Vector3.up * 150, m_rigidbody.transform.position, ForceMode.Acceleration);

                if (Input.GetAxis("Horizontal") < -0.5f || Input.GetAxis("HorizontalXbox") < -0.5f)
                {
                    m_rigidbody.transform.Rotate(Vector3.up, Mathf.Lerp(m_rigidbody.transform.rotation.y,
                                                m_rigidbody.transform.rotation.y - 40f, rotationSpeed * Time.deltaTime));
                    leftDrift = true;
                    rightDrift = false;
                    stifness = 0;
                    driveMode = DriveMode.Drift;
                }
                else if (Input.GetAxis("Horizontal") > 0.5f || Input.GetAxis("HorizontalXbox") > 0.5f)
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

        //driveMode front
        if (driveMode == DriveMode.Front)
        {
            maxSpeed = frontMaxSpeed;            

            if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("HorizontalXbox") < 0)
            {
                wheelFR.steerAngle = -turnRadius;
                wheelFL.steerAngle = -turnRadius;
            }
            else if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("HorizontalXbox") > 0)
            {
                wheelFR.steerAngle = turnRadius;
                wheelFL.steerAngle = turnRadius;
            }            
            else if (m_animator.GetBool("isSpinning") || m_animator.GetBool("isKnockedUp"))
            {
                wheelFR.steerAngle = 0;
                wheelFL.steerAngle = 0;
            }
            else if (Input.GetAxis("HorizontalXbox") == 0 || Input.GetAxis("Horizontal") == 0)
            {
                wheelFR.steerAngle = 0;
                wheelFL.steerAngle = 0;
            }           
            

            wheelBLFrontFriction = wheelBL.forwardFriction;

            if (wheelBLFrontFriction.stiffness < 0.9f)
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

            if (wheelBLDriftFriction.stiffness < 0.9f)
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
            wheelBLDamp = 0.1f;
            wheelBL.wheelDampingRate = wheelBLDamp;

            wheelBRDamp = wheelBR.wheelDampingRate;
            wheelBRDamp = 0.1f;
            wheelBR.wheelDampingRate = wheelBRDamp;

            wheelFLDamp = wheelFL.wheelDampingRate;
            wheelFLDamp = 0.1f;
            wheelFL.wheelDampingRate = wheelFLDamp;

            wheelFRDamp = wheelFR.wheelDampingRate;
            wheelFRDamp = 0.1f;
            wheelFR.wheelDampingRate = wheelFRDamp;

            driftCounter = 2f;
            driftDelay += Time.deltaTime;

            Drifting = false;
            rightDrift = false;
            leftDrift = false;

            if (currentSpeed >= 1 && currentSpeed <= maxSpeed / 1.5f)
            {
                turnRadius = frontTurnRadius * 1.5f;
            }
            else
            {
                turnRadius = frontTurnRadius;
            }
            particleSystemBLWheel.SetActive(false);
            particleSystemBRWheel.SetActive(false);


            //wheelBLTrail.startColor = Color.red;
            //wheelBRTrail.startColor = Color.red;
            //wheelBLTrail.endColor = Color.black;
            //wheelBRTrail.endColor = Color.black;

            if (Input.GetAxis("Vertical") < 0)
            {
                m_rigidbody.AddRelativeForce(new Vector3(0, 0, -Mathf.Abs(transform.forward.z)).normalized * currentAcc * 2, ForceMode.Acceleration);

                if (currentSpeed <= 0.5f)
                {
                    driveMode = DriveMode.Stopped;
                }
                else
                {
                    driveMode = DriveMode.Front;
                }
            }
        }
        if (driveMode == DriveMode.Rear)
        {
            maxSpeed = rearMaxSpeed;

            if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("HorizontalXbox") < 0)
            {
                wheelFR.steerAngle = -turnRadius;
                wheelFL.steerAngle = -turnRadius;
            }
            else if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("HorizontalXbox") > 0)
            {
                wheelFR.steerAngle = turnRadius;
                wheelFL.steerAngle = turnRadius;
            }            
            else if (m_animator.GetBool("isSpinning") || m_animator.GetBool("isKnockedUp"))
            {
                wheelFR.steerAngle = 0;
                wheelFL.steerAngle = 0;
            }
            else if (Input.GetAxis("HorizontalXbox") == 0 || Input.GetAxis("Horizontal") == 0)
            {
                Debug.Log("Está anant recte");
                wheelFR.steerAngle = 0;
                wheelFL.steerAngle = 0;
            }

            if (Input.GetButton("Brake"))
            {
                scaledTorque = torque * -currentAcc;

                wheelFR.brakeTorque = 0;
                wheelFL.brakeTorque = 0;
                wheelBR.brakeTorque = 0;
                wheelBL.brakeTorque = 0;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                scaledTorque = Input.GetAxis("Vertical") * torque * currentAcc;

                wheelFR.brakeTorque = 0;
                wheelFL.brakeTorque = 0;
                wheelBR.brakeTorque = 0;
                wheelBL.brakeTorque = 0;
            }

            //sideawaysFriction
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

            //frontFriction
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

            driftCounter = 2;
            driftDelay += Time.deltaTime;

            Drifting = false;
            rightDrift = false;
            leftDrift = false;

            turnRadius = rearTurnRadius;

            particleSystemBLWheel.SetActive(false);
            particleSystemBRWheel.SetActive(false);

            if (Input.GetAxis("Vertical") > 0)
            {
                m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * currentAcc * 2, ForceMode.Acceleration);

                if (currentSpeed <= 0.5f)
                {
                    driveMode = DriveMode.Stopped;
                }
                else
                {
                    driveMode = DriveMode.Rear;
                }
            }
        }

        if (driveMode == DriveMode.Drift)
        {
            Drifting = true;

            //wheelBLTrail.enabled = true;
            //wheelBRTrail.enabled = true;
            

            driftFrwd = m_rigidbody.transform.right;            

            if (rightDrift && (Input.GetAxis("Horizontal") == -1 || Input.GetAxis("HorizontalXbox") == -1))
            {
                m_rigidbody.AddRelativeForce((m_rigidbody.transform.forward * 2 - driftFrwd) * driftForce, ForceMode.Force);

                Debug.DrawRay(m_rigidbody.transform.position, (m_rigidbody.transform.forward * 2 - driftFrwd) * driftForce, Color.green);

                m_rigidbody.transform.Rotate(m_rigidbody.transform.up, Mathf.Lerp(m_rigidbody.transform.rotation.y,
                                                                                  m_rigidbody.transform.rotation.y - 10, 0.1f));

                //Debug.Log("SPACE:" + Input.GetKey("space") + ", DRIFT: " + Input.GetButton("Drift") + ", HORI:" + Input.GetAxis("Horizontal"));

            }
            else if (leftDrift && (Input.GetAxis("Horizontal") == 1 || Input.GetAxis("HorizontalXbox") == 1))
            {
                m_rigidbody.AddRelativeForce((m_rigidbody.transform.forward + driftFrwd) * driftForce, ForceMode.Force);

                Debug.DrawRay(m_rigidbody.transform.position, (m_rigidbody.transform.forward * 2 + driftFrwd) * driftForce, Color.magenta);

                m_rigidbody.transform.Rotate(m_rigidbody.transform.up, Mathf.Lerp(m_rigidbody.transform.rotation.y,
                                                                                  m_rigidbody.transform.rotation.y + 10, 0.1f));
            }

            DriftBehaviour(wheelBL, wheelBR, wheelFL, wheelFR);

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

            if (stifness >= 0.1f)
            {
                stifness = 0.1f;
            }       
             
            driftCounter -= Time.deltaTime;

            if (Input.GetAxis("Vertical") > 0)
            {
                if (driftCounter <= 0 && isSpaceDown)
                {
                   //wheelBLTrail.startColor = Color.blue;
                   //wheelBRTrail.startColor = Color.blue;
                   //wheelBLTrail.endColor = Color.blue;
                   //wheelBRTrail.endColor = Color.blue;

                    canTurbo = true;
                }
                else if (isSpaceJustUp && canTurbo)
                {
                    m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(m_rigidbody.transform.forward.z)) * endDriftTurboForce, ForceMode.VelocityChange);
                    driftCounter = 2f;

                    Drifting = false;
                    rightDrift = false;
                    leftDrift = false;

                    if (Input.GetAxis("Vertical") > 0 || Input.GetButton("Accelerate"))
                    {
                        driveMode = DriveMode.Front;
                    }

                    canTurbo = false;
                }
                else if (driftCounter > 0 && isSpaceJustUp)
                {
                    Drifting = false;
                    rightDrift = false;
                    leftDrift = false;
                    driftCounter = 2f;

                    if (Input.GetAxis("Vertical") > 0 || Input.GetButton("Accelerate"))
                    {
                        driveMode = DriveMode.Front;
                    }
                }
            }
            
            if (inputAcc > 0)
            {
                if (driftCounter <= 0 && isDriftingXbox)
                {
                    //wheelBLTrail.startColor = Color.blue;
                    //wheelBRTrail.startColor = Color.blue;
                    //wheelBLTrail.endColor = Color.blue;
                    //wheelBRTrail.endColor = Color.blue;

                    canTurbo = true;

                }
                else if (!isDriftingXbox && canTurbo)
                {
                    m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(m_rigidbody.transform.forward.z)) * endDriftTurboForce, ForceMode.VelocityChange);
                    driftCounter = 2f;

                    Drifting = false;
                    rightDrift = false;
                    leftDrift = false;

                    if (Input.GetAxis("Vertical") > 0 || Input.GetButton("Accelerate"))
                    {
                        driveMode = DriveMode.Front;
                    }

                    canTurbo = false;
                }
                else if (driftCounter > 0 && !isDriftingXbox)
                {
                    Drifting = false;
                    rightDrift = false;
                    leftDrift = false;
                    driftCounter = 2f;

                    if (Input.GetAxis("Vertical") > 0 || Input.GetButton("Accelerate"))
                    {
                        driveMode = DriveMode.Front;
                    }
                }
            }

            particleSystemBLWheel.SetActive(true);
            particleSystemBRWheel.SetActive(true);

            if (particleSystemBLWheel.activeInHierarchy)
            {
                ParticleSystem m_PSBackLeft = particleSystemBLWheel.GetComponent<ParticleSystem>();
                ParticleSystem m_PSBackRight = particleSystemBRWheel.GetComponent<ParticleSystem>();

                Light lightBL = particleSystemBLWheel.GetComponent<Light>();
                Light lightBR = particleSystemBRWheel.GetComponent<Light>();

                m_PSBackLeft.GetComponent<Renderer>().material = null;
                m_PSBackRight.GetComponent<Renderer>().material = null;

                m_PSBackLeft.Play();
                m_PSBackRight.Play();
                
                if (driftCounter <= 0)
                {                   
                    m_PSBackLeft.GetComponent<Renderer>().material = sparkMaterialBlue;
                    m_PSBackRight.GetComponent<Renderer>().material = sparkMaterialBlue;

                    lightBL.color = Color.red;
                    lightBR.color = Color.red;
                }
                else
                {
                    m_PSBackLeft.GetComponent<Renderer>().material = sparkMaterialYellow;
                    m_PSBackRight.GetComponent<Renderer>().material = sparkMaterialYellow;

                    lightBL.color = Color.yellow;
                    lightBR.color = Color.yellow;
                }
            }

            Sfire[0].Play();
            Sfire[1].Play();
            Sfire[2].Play();
            Sfire[3].Play();
            Sfire[4].Play();
            Sfire[5].Play();
            Sfire[6].Play();
            Sfire[7].Play();
           
        }

        WheelHit hit;
        RaycastHit hitFloor1;

        bool groundedWheel = wheelBL.GetGroundHit(out hit);
        bool groundedWheel2 = wheelBR.GetGroundHit(out hit);
        bool groundedWheel3 = wheelFR.GetGroundHit(out hit);
        bool groundedWheel4 = wheelFL.GetGroundHit(out hit);

        if ((!groundedWheel && !groundedWheel2 && !groundedWheel3 && !groundedWheel4) && !Drifting)
        {
            if (Physics.Raycast(transform.position, -m_rigidbody.transform.up, out hitFloor1, 10f))
            {
                gravity = 8;

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
            }
            else
            {
                gravity = 15;

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
        else if (groundedWheel ||groundedWheel2 ||groundedWheel3 || groundedWheel4)
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

        turnRadius = driftTurnRadius;

        wheelFR.motorTorque = scaledTorque;
        wheelFL.motorTorque = scaledTorque;

        driftFrwd = m_rigidbody.transform.right;

        if (groundedBL && groundedBR)
        {
            if (Input.GetAxis("Vertical") > 0 || Input.GetButton("Accelerate"))
            {
                WheelFL.motorTorque = scaledTorque;
                WheelFR.motorTorque = scaledTorque;
                WheelBL.motorTorque = scaledTorque;
                WheelBR.motorTorque = scaledTorque;

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

                    if ((Input.GetAxis("Horizontal") <= 1 && Input.GetAxis("Horizontal") > 0))
                    {
                        m_rigidbody.AddRelativeForce((-m_rigidbody.transform.forward * Input.GetAxis("Vertical") +
                                                      driftFrwd * 2) * driftForce, ForceMode.Force);

                        Debug.DrawRay(m_rigidbody.transform.position, (-m_rigidbody.transform.forward * Input.GetAxis("Vertical") +
                                                                       driftFrwd * 2) * driftForce, Color.yellow);
                    }
                    else if ((Input.GetAxis("HorizontalXbox") <= 1 && Input.GetAxis("HorizontalXbox") > 0))
                    {
                        m_rigidbody.AddRelativeForce((-m_rigidbody.transform.forward * inputAcc +
                                                      driftFrwd * 2) * driftForce, ForceMode.Force);

                        Debug.DrawRay(m_rigidbody.transform.position, (-m_rigidbody.transform.forward * inputAcc +
                                                                       driftFrwd * 2) * driftForce, Color.yellow);
                    }
                }

                else if (leftDrift)
                {
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

                    if (Input.GetAxis("Horizontal") >= -1 && Input.GetAxis("Horizontal") < 0)
                    {
                        m_rigidbody.AddRelativeForce((-m_rigidbody.transform.forward * Input.GetAxis("Vertical") -
                                                      driftFrwd * 2) * driftForce, ForceMode.Force);

                        Debug.DrawRay(m_rigidbody.transform.position, (-m_rigidbody.transform.forward * Input.GetAxis("Vertical") -
                                                                        driftFrwd * 2) * driftForce, Color.black);
                    }
                    else if (Input.GetAxis("HorizontalXbox") >= -1 && Input.GetAxis("HorizontalXbox") < 0)
                    {
                        m_rigidbody.AddRelativeForce((-m_rigidbody.transform.forward * inputAcc -
                                                      driftFrwd * 2) * driftForce, ForceMode.Force);

                        Debug.DrawRay(m_rigidbody.transform.position, (-m_rigidbody.transform.forward * inputAcc -
                                                                        driftFrwd * 2) * driftForce, Color.black);
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
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(m_rigidbody.transform.forward.z)).normalized * turboForce, ForceMode.Impulse);
        }
        if (col.tag == "Spear" || col.tag == "Barrel" || col.tag == "FakeMysteryBox" || col.tag == "Rocket")
        {
            m_animator.SetBool("isKnockedUp", true);
            m_rigidbody.AddRelativeForce(new Vector3(0, Mathf.Abs(m_rigidbody.transform.forward.y), 0).normalized * knockUpForce, ForceMode.Impulse);
        }
        else if (col.tag == "Water" || col.tag == "Banana")
        {
            m_animator.SetBool("isSpinning", true);
            m_rigidbody.AddRelativeForce(new Vector3(0, 0, -Mathf.Abs(m_rigidbody.transform.forward.z)).normalized * slipperyForce, ForceMode.Impulse);
        }
        else
        {
            m_animator.SetBool("isKnockedUp", false);
            m_animator.SetBool("isSpinning", false);            
        }  
        //if (col.tag == "Wall")
        //{
        //
        //}      
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "RoughFloor")
        {
            maxSpeed = 10;
        }
        
        if (col.tag == "IA")
        {
            rebufoCounter += Time.deltaTime;

            if (rebufoCounter >= 2)
            {
                m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(m_rigidbody.transform.forward.z)).normalized * miniTurboForce, ForceMode.Acceleration);
                rebufoCounter = 0;
            }
        }
        if (col.tag == "Cheese" || col.tag == "Ramp")
        {
            m_rigidbody.AddRelativeForce(new Vector3(col.gameObject.transform.rotation.y, 0, 0) * 5, ForceMode.Force);
            
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "IA")
        {
            rebufoCounter = 0;
        }
        else if (col.tag == "RoughFloor")
        {
            maxSpeed = frontMaxSpeed;

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
