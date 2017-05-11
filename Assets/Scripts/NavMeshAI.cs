using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour {

    public Transform path;
    private List<Transform> points;

    public int destPoint = 0;
    public float distanceToNextPoint;

    public float changeVelocityTimer, changeVelocityCooldown = 15;


    private NavMeshAgent agent;
    private OffMeshLink link;
    private float oldLinkCost;

    private Rigidbody m_rigidbody;
    private IA_Item ia_Item;

    [SerializeField]
    private float nodeRange = 10f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        m_rigidbody = GetComponent<Rigidbody>();
        ia_Item = GetComponent<IA_Item>();
        link = null;

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).

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
        GotoNextPoint();
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
            float spd = (Random.Range(10f, 15f));
            float acc = (Random.Range(45f, 70f));

            ia_Item.iADefaultSpeed = spd;
            ia_Item.iADefaultAcc = acc;

            Debug.Log("Velocity Change");

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

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Turbo")
        {
            Debug.Log("IATurbo");
          //  m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * agent.acceleration / 3, ForceMode.VelocityChange);
        }
    }

    void AcquireOffmeshLink()
    {
        if (link == null)
        {
            link = agent.currentOffMeshLinkData.offMeshLink;
            //oldLinkCost = link.costOverride;
            //link.costOverride = 1000.0f;
            link.activated = false;
        }
    }

    void ReleaseOffmeshLink()
    {
        if (link != null)
        {
            //link.costOverride = oldLinkCost;
            link.activated = true;
            link = null;
        }
    }

    void OnDestroy()
    {
        // Remember to release the link when the agent is killed/etc.
        ReleaseOffmeshLink();
    }

}
