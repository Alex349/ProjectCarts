using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IA_Item : MonoBehaviour
{

    public string currentIAItem = "none";
    public int money;
    private NavMeshAgent agent;
    [SerializeField]
    private float bananaEffect = 3;
    [SerializeField]
    private Transform backSpawn;
    [SerializeField]

    private Vector3 backSpawnVector;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        backSpawnVector = backSpawn.transform.position;
        bananaEffect -= Time.deltaTime;
        if (bananaEffect < 0)
        {
            Debug.Log("NoSlowed");
            agent.speed = 10;
            agent.acceleration = 40;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Item");
            UseItem();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MysteryBox")
        {
            Destroy(other.gameObject);

            if (currentIAItem == "none")
            {
                GetRandomItem();

            }
        }

        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);

            money++;

        }

        if (other.tag == "Banana")
        {
            Destroy(other.gameObject);
            bananaEffect = 3;

            if (bananaEffect > 0)
            {
                Debug.Log("Slowed");
                agent.speed = 2;
                agent.acceleration = 8;

            }
        }

    }

    void GetRandomItem()
    {
        float rnd = (Random.Range(0f, 1f));


        if (rnd < 0.45)
        {
            currentIAItem = "banana";
        }
        else if (rnd < 0.7)
        {
            currentIAItem = "turbo";
        }
        else if (rnd < 1)
        {
            currentIAItem = "rocket";
        }
        else
        {

        }
    }

    void UseItem()
    {
        if (currentIAItem == "none")
        {

        }
        if (currentIAItem == "rocket")
        {

        }
        if (currentIAItem == "turbo")
        {

        }
        if (currentIAItem == "banana")
        {
            Instantiate(Resources.Load("Banana"), backSpawnVector, Quaternion.identity);
        }
    }
}
