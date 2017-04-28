using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_carItem : MonoBehaviour {

	public string currentIAItem = "none";
    private m_carController carController;
    public int money;
    [SerializeField]
    private float carDefaultSpeed = 10;
    [SerializeField]
    private float carDefaultAcc = 40;
    private float bananaEffect;
    [SerializeField]
    private float bananaEffectDuration = 3;
    private float turboEffect;
    [SerializeField]
    private float turboEffectDuration = 3;
    [SerializeField]
    private float bananaSlowedSpeed = 2;
    [SerializeField]
    private float bananaSlowedAcc = 8;
    [SerializeField]
    private float turboSpeed = 30;
    [SerializeField]
    private float turboAcc = 120;
    [SerializeField]
    private Transform backSpawn;
    private Vector3 backSpawnVector;
    [SerializeField]
    private Transform frontSpawn;
    private Vector3 frontSpawnVector;

    void Start()
    {
        carController = GameObject.FindGameObjectWithTag("Kart").GetComponent<m_carController>();
    }
    // Update is called once per frame
    void Update()
    {
        backSpawnVector = backSpawn.transform.position;
        frontSpawnVector = frontSpawn.transform.position;

        UpdateItems();
        IncreaseSpeedOnMoney();

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
            bananaEffect = bananaEffectDuration;

            if (bananaEffect > 0)
            {
                Debug.Log("Slowed");
                //carController.maxSpeed = bananaSlowedSpeed;
                carController.acceleration = bananaSlowedAcc;
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
            Instantiate(Resources.Load("Items/Rocket"), frontSpawnVector, Quaternion.identity);
            currentIAItem = "none";
        }
        if (currentIAItem == "turbo")
        {
            Debug.Log("Turbo");

            turboEffect = turboEffectDuration;
            currentIAItem = "none";
        }
        if (currentIAItem == "banana")
        {
            Instantiate(Resources.Load("Items/Banana"), backSpawnVector, Quaternion.identity);
            currentIAItem = "none";

        }
    }

    void UpdateItems()
    {
        //BananaItemUpdate
        bananaEffect -= Time.deltaTime;

        if (bananaEffect < 0)
        {
            //carController.maxSpeed = carDefaultSpeed;
            carController.acceleration = carDefaultAcc;
        }
        //TurboItemUpdate
        turboEffect -= Time.deltaTime;

        if (turboEffect > 0)
        {
            //carController.maxSpeed = turboSpeed;
            carController.acceleration = turboAcc;

        }

        if (turboEffect < 0)
        {
            //carController.maxSpeed = carDefaultSpeed;
            carController.acceleration = carDefaultAcc;
        }
    }

    void IncreaseSpeedOnMoney()
    {
        //carController.maxSpeed = carController.maxSpeed * ( 1 + money * 0.1f);
        carController.acceleration = carController.acceleration * (1 + money * 0.4f);
    }
}
