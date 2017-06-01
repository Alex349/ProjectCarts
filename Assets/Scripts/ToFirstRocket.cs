using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ToFirstRocket : MonoBehaviour {

    [SerializeField]
    private float rocketSpeed = 20, rocketAcc = 80, selfDestruct = 10;

    private NavMeshAgent agent;
    public Transform target;
    private Vector3 destination;

    private PositionManager _positionManager;


    // Use this for initialization
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();

        _positionManager = GameObject.Find("HUDManager").GetComponent<PositionManager>();

        target = _positionManager.racersGO[0].transform;
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

        if (selfDestruct < 8)
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
            Destroy(col.gameObject);
        }
    }
}
