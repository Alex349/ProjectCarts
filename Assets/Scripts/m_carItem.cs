using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_carItem : MonoBehaviour {

	public string currentPlayerObject = "none";
    private bool bananaDefending = false;
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

    private string lap1Time, lap2Time, lap3Time;
    private float lapCountdown;
    private HudManager hudManager;

    void Start()
    {
        // carController = GameObject.FindGameObjectWithTag("Kart").GetComponent<m_carController>();
        hudManager = GameObject.Find("HUDManager").GetComponent<HudManager>();
    }
    // Update is called once per frame
    void Update()
    {
        backSpawnVector = backSpawn.transform.position;
        frontSpawnVector = frontSpawn.transform.position;

        UpdateItems();
        IncreaseSpeedOnMoney();

        if (currentPlayerObject == "banana" || bananaDefending == true)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                UseBanana();
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.L))
                {
                    ReleaseBanana();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                UseItem();
            }
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

            Debug.Log("Coin");

            if (money > 10)
            {

            }
            else
            {
                money++;

            }

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
            if (money > 10)
            {
                currentPlayerObject = "none";
            }
            else
            {
                currentPlayerObject = "FakeBox";

            }
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

        if (currentPlayerObject == "coin")
        {
            money = money + 5;
            currentPlayerObject = "none";

        }

        if (currentPlayerObject == "FakeBox")
        {
            currentPlayerObject = "none";

        }
    }

    void UseBanana()
    {
        if (currentPlayerObject == "banana")
        {
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVector, Quaternion.identity) as GameObject).transform.parent = backSpawn.transform;
            currentPlayerObject = "none";
            bananaDefending = true;
        }
    }

    void ReleaseBanana()
    {
        backSpawn.DetachChildren();
        bananaDefending = false;
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

    public void SetTimeLap()
    {
        if (lap1Time == string.Empty)
        {
            lap1Time = hudManager.time_Text.text.ToString();
            Debug.Log("Lap1Set");
            lapCountdown = 5;
        }

        if ((lap1Time != string.Empty && lap2Time == string.Empty) && lapCountdown < 0)
        {
            lap2Time = hudManager.time_Text.text.ToString();
            Debug.Log("Lap2Set");
            lapCountdown = 5;
        }

        if ((lap1Time != string.Empty && lap2Time != string.Empty) && lapCountdown < 0)
        {
            lap3Time = hudManager.time_Text.text.ToString();
            lapCountdown = 400;
        }
    }
}
