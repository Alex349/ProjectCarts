using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class HoamingRocket : MonoBehaviour
{

    [SerializeField]
    private float rocketSpeed = 20, rocketAcc = 80, selfDestruct = 10, triggerDelay;

    public int targetListPosition, shooterListPosition,lastPlayer;
    Vector3 myTransform;

    private NavMeshAgent agent;
    public Transform target;
    private Vector3 destination;
    private StraightRocket straightRocket;
    private PositionManager _positionManager;

    // Use this for initialization
    void Start()
    {
        triggerDelay = selfDestruct - 1;
        agent = GetComponent<NavMeshAgent>();
        straightRocket = GetComponent<StraightRocket>();
        myTransform = this.transform.position;

        _positionManager = GameObject.Find("HUDManager").GetComponent<PositionManager>();

        if (shooterListPosition == 1)
        {
            straightRocket.enabled = true;
            straightRocket.fromHoaming = 1.3f;

            this.enabled = false;
        }
        else
        {
            targetListPosition = shooterListPosition - 2;
            target = _positionManager.racersGO[targetListPosition].transform;
        }


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
            GameObject.Find("AlertBoxHUD").GetComponent<RocketsHUDScript>().hoamingIsInside = false;
            Destroy(this.gameObject);
        }

        if (selfDestruct < triggerDelay)
        {
            Component[] capsules;
            capsules = GetComponents(typeof(CapsuleCollider));
            foreach (CapsuleCollider a in capsules)
                a.enabled = true;
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "Kart")
        {
            GameObject.Find("AlertBoxHUD").GetComponent<RocketsHUDScript>().hoamingIsInside = false;
            Destroy(this.gameObject);
        }
        if (col.tag == "Banana" || col.tag == "FakeMysteryBox")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);
        }
    }
}
