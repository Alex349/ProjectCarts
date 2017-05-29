using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoamingRocket : MonoBehaviour
{

    [SerializeField]
    private float rocketSpeed = 20, rocketAcc = 80;
    [SerializeField]
    private float rocketBounces;

    Vector3 myTransform;

    private NavMeshAgent agent;
    public Transform target;
    private Vector3 destination;

    // Use this for initialization
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();

        myTransform = this.transform.position;
        destination = agent.destination;
    }

    // Update is called once per frame
    void Update()
    {
        destination = target.position;
        agent.destination = destination;

        agent.speed = rocketSpeed;
        agent.acceleration = rocketAcc;


        if (rocketBounces >= 5)
        {
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter (Collider col)
    {
        if (col.tag == "Player" || col.tag == "Kart")
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        rocketBounces++;

    }
}
