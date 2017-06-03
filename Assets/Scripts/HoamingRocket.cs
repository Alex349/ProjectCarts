using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoamingRocket : MonoBehaviour
{

    [SerializeField]
    private float rocketSpeed = 20, rocketAcc = 80, selfDestruct = 10, triggerDelay;

    public int targetListPosition, shooterListPosition;
    Vector3 myTransform;

    private NavMeshAgent agent;
    public Transform target;
    private Vector3 destination;

    private PositionManager _positionManager;

    // Use this for initialization
    void Start()
    {
        triggerDelay = selfDestruct - 1;
        agent = GetComponent<NavMeshAgent>();

        myTransform = this.transform.position;

        _positionManager = GameObject.Find("HUDManager").GetComponent<PositionManager>();

        targetListPosition = shooterListPosition - 2;
        if (targetListPosition < 0)
        {
            Debug.Log("Hardcode");
            targetListPosition = 4;
        }
        target = _positionManager.racersGO[targetListPosition].transform;

        destination = agent.destination;
    }

    // Update is called once per frame
    void Update()
    {
        selfDestruct -= Time.deltaTime;
        destination = target.position;
        agent.destination = destination;

        agent.speed = rocketSpeed;
        agent.acceleration = rocketAcc;

        if (selfDestruct < 0)
        {
            Destroy(this.gameObject);
        }

        if (selfDestruct < triggerDelay)
        {
            Component[] shpheres;
            shpheres = GetComponents(typeof(SphereCollider));
            foreach (SphereCollider a in shpheres)
                a.enabled = true;
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "Kart")
        {
            Destroy(this.gameObject);
        }
        if (col.tag == "Banana" || col.tag == "FakeMysteryBox")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);
        }
    }
}
