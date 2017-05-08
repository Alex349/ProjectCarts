using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_carItem : MonoBehaviour {

	public string currentPlayerObject = "none";
    public m_carController carController;
    public float money;
    [SerializeField]
    private float carDefaultSpeed = 10;
    [SerializeField]
    private float carDefaultAcc = 10;
    [SerializeField]
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
       // carController = GameObject.FindGameObjectWithTag("Kart").GetComponent<m_carController>();
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

            if (currentPlayerObject == "none")
            {
                GetRandomItem();
            }
        }

        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);

            money = money + 0.5f;

        }

        if (other.tag == "Banana")
        {
            Destroy(other.gameObject);

            bananaEffect = bananaEffectDuration;

            if (bananaEffect > 0)
            {
                Debug.Log("Slowed");
                //carController.maxSpeed = bananaSlowedSpeed;
                carController.currentAcc = bananaSlowedAcc;
            }
        }

    }

    void GetRandomItem()
    {
        float rnd = (Random.Range(0f, 1f));


        if (rnd < 0.2)
        {
            currentPlayerObject = "banana";
        }
        else if (rnd < 0.4)
        {
            currentPlayerObject = "turbo";
        }
        else if (rnd < 0.6)
        {
            currentPlayerObject = "rocket";
        }
        else if (rnd < 1)
        {
            currentPlayerObject = "coin";
        }
    }

    void UseItem()
    {
        if (currentPlayerObject == "none")
        {

        }
        if (currentPlayerObject == "rocket")
        {
            Instantiate(Resources.Load("Items/Rocket"), frontSpawnVector, Quaternion.identity);
            currentPlayerObject = "none";
        }
        if (currentPlayerObject == "turbo")
        {
            Debug.Log("Turbo");

            turboEffect = turboEffectDuration;
            currentPlayerObject = "none";
        }
        if (currentPlayerObject == "banana")
        {
            Instantiate(Resources.Load("Items/Banana"), backSpawnVector, Quaternion.identity);
            currentPlayerObject = "none";
        }

        if (currentPlayerObject == "coin")
        {
            money = money + 5;
            currentPlayerObject = "none";

        }
    }

    void UpdateItems()
    {
        //BananaItemUpdate
        bananaEffect -= Time.deltaTime;

        if (bananaEffect < 0)
        {
            //carController.maxSpeed = carDefaultSpeed;
           // carController.currentAcc = carDefaultAcc;
        }
        //TurboItemUpdate
        turboEffect -= Time.deltaTime;

        if (turboEffect > 0)
        {
            //carController.maxSpeed = turboSpeed;
            carController.currentAcc = turboAcc;

        }

        if (turboEffect < 0)
        {
            //carController.maxSpeed = carDefaultSpeed;
           //carController.currentAcc = carDefaultAcc;
        }
    }

    void IncreaseSpeedOnMoney()
    {
        //carController.maxSpeed = carController.maxSpeed * ( 1 + money * 0.1f);
        carController.currentAcc = carController.currentAcc * (1 + money * 0.01f);
    }
}
