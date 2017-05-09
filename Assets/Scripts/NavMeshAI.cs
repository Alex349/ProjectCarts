using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour {

    public Transform path;
    private List<Transform> points;
    [SerializeField]
    public int destPoint = 0;
    private NavMeshAgent agent;
    private Rigidbody m_rigidbody;
    [SerializeField]
    private float nodeRange = 10f;

    public float distanceToNextPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        m_rigidbody = GetComponent<Rigidbody>();
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
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.remainingDistance < nodeRange)
        {
            GotoNextPoint();
        }

        distanceToNextPoint = Vector3.Distance(this.transform.position, points[destPoint].position);
        
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Turbo")
        {
            Debug.Log("IATurbo");
          //  m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)) * agent.acceleration / 3, ForceMode.VelocityChange);
        }
    }
}
