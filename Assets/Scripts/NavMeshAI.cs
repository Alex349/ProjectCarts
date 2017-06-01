using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour
{

    public Transform path;
    public List<Transform> points;

    public int destPoint = 0;
    public float distanceToNextPoint;
    public float camSpeed = 10f;
    public float changeVelocityTimer, changeVelocityCooldown = 15;


    private NavMeshAgent agent;
    private OffMeshLink link;

    private Rigidbody m_rigidbody;
    private IA_Item ia_Item;

    [SerializeField]
    private float nodeRange = 10f;

    public Transform backLeft;
    public Transform backRight;
    public Transform frontLeft;
    public Transform frontRight;
    public RaycastHit hit;
    public RaycastHit lr;
    public RaycastHit rr;
    public RaycastHit lf;
    public RaycastHit rf;
    public Vector3 upDir;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        m_rigidbody = GetComponent<Rigidbody>();
        ia_Item = GetComponent<IA_Item>();
        link = null;
        agent.updateRotation = true;

        agent.autoBraking = false;

        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        points = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                points.Add(pathTransforms[i]);
            }
        }
    }


    void Update()
    {
        changeVelocityTimer -= Time.deltaTime;

        // Choose the next destination point when the agent gets
        // close to the current one.

        if (agent.remainingDistance < nodeRange)
        {
            GotoNextPoint();
        }

        distanceToNextPoint = Vector3.Distance(this.transform.position, points[destPoint].position);

        if (changeVelocityTimer < 0)
        {
            VelocityandAccelerationRandom();
            Debug.Log("Velochng");
            changeVelocityTimer = changeVelocityCooldown;
        }

        if (agent.currentOffMeshLinkData.valid)
        {
            AcquireOffmeshLink();
        }
        else
        {
            ReleaseOffmeshLink();
        }

        //Vector3 relativePos = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z) - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * camSpeed);

        Physics.Raycast(backLeft.position + Vector3.up, Vector3.down, out lr);
        Physics.Raycast(backRight.position + Vector3.up, Vector3.down, out rr);
        Physics.Raycast(frontLeft.position + Vector3.up, Vector3.down, out lf);
        Physics.Raycast(frontRight.position + Vector3.up, Vector3.down, out rf);

        upDir = (Vector3.Cross(rr.point - Vector3.up, lr.point - Vector3.up) +
                 Vector3.Cross(lr.point - Vector3.up, lf.point - Vector3.up) +
                 Vector3.Cross(lf.point - Vector3.up, rf.point - Vector3.up) +
                 Vector3.Cross(rf.point - Vector3.up, rr.point - Vector3.up)
                ).normalized;

        Debug.DrawRay(rr.point, Vector3.up);
        Debug.DrawRay(lr.point, Vector3.up);
        Debug.DrawRay(lf.point, Vector3.up);
        Debug.DrawRay(rf.point, Vector3.up);

        //transform.up = upDir;

        //var rotation = Quaternion.LookRotation(points[destPoint].position - new Vector3(transform.position.x, transform.position.y -upDir, ));
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Turbo")
        {
            Debug.Log("IATurbo");
            //  m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * agent.acceleration / 3, ForceMode.VelocityChange);
        }
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Count == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Count;
    }

    void AcquireOffmeshLink()
    {
        if (link == null)
        {
            link = agent.currentOffMeshLinkData.offMeshLink;
            link.activated = false;
        }
    }

    void ReleaseOffmeshLink()
    {
        if (link != null)
        {
            link.activated = true;
            link = null;
        }
    }

    void OnDestroy()
    {
        ReleaseOffmeshLink();
    }

    public void VelocityandAccelerationRandom()
    {
        float spd = (Random.Range(10f, 15f));
        float acc = (Random.Range(40f, 60f));

        ia_Item.iADefaultSpeed = spd;
        ia_Item.iADefaultAcc = acc;
    }
}