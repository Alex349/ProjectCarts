using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ToFirstRocket : MonoBehaviour
{

    [SerializeField]
    private float rocketSpeed = 20, rocketAcc = 80, colliderActivator = 10;

    private NavMeshAgent agent;
    public Transform target;
    private Vector3 destination;

    public float scaleSpeed = 2.0f;

    private PositionManager _positionManager;
    private GameObject waveBefore, explosionAfter;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        _positionManager = GameObject.Find("HUDManager").GetComponent<PositionManager>();

        target = _positionManager.racersGO[0].transform;
        destination = agent.destination;

        waveBefore = transform.GetChild(1).gameObject;
        explosionAfter = transform.GetChild(2).gameObject;

        waveBefore.SetActive(false);
        explosionAfter.SetActive(false);
    }


    void Update()
    {
        colliderActivator -= Time.deltaTime;
        destination = target.position;
        agent.destination = destination;

        agent.speed = rocketSpeed;
        agent.acceleration = rocketAcc;

        if (colliderActivator < 1)
        {
            Component[] shpheres;
            shpheres = GetComponents(typeof(SphereCollider));
            foreach (SphereCollider a in shpheres)
                a.enabled = true;
        }
        if (transform.localScale.x < 0.5f)
        {
            IncreaseOnTime();
        }
        //Debug.Log(Vector3.Distance(target.transform.position, transform.position));

        if (Vector3.Distance(target.transform.position, this.transform.position) < 4f)
        {
            //GameObject.Find("AlertBoxHUD").GetComponent<RocketsHUDScript>().KingBallIsInside = false;
            //Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Banana" || col.tag == "FakeMysteryBox")
        {
            Destroy(col.gameObject);
        }
    }

    void IncreaseOnTime()
    {
        transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
    }
    void OnDestroy()
    {
        waveBefore.SetActive(true);
        explosionAfter.SetActive(true);
    }
}
