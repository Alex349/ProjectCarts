using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_carItem : MonoBehaviour {

	public string currentPlayerObject = "none";
    private bool bananaDefending = false, triplebananaDefending = false;
    public m_carController carController;
    public float money;

    //Defaults
    [SerializeField]
    private float carDefaultSpeed = 10;
    [SerializeField]
    private float carDefaultAcc = 10;

    [SerializeField]
    public int myPosition;

    //Banana
    [SerializeField]
    private float bananaEffect;
    [SerializeField]
    private float bananaEffectDuration = 3;
    [SerializeField]
    private float bananaSlowedSpeed = -1.5f;
    [SerializeField]
    private float bananaSlowedAcc = -1.5f;

    //Turbo
    [SerializeField]
    private float turboEffect;
    [SerializeField]
    private float turboSpeed = 1.1f;
    [SerializeField]
    private float turboAcc = 1.1f;
    [SerializeField]
    private float turboEffectDuration = 3;
    private bool turboReset = false;
    //TurboTriple
    private int turbosUsed = 0;

    //Rocket
    [SerializeField]
    private float rocketEffect;
    [SerializeField]
    private float rocketSpeed = 0f;
    [SerializeField]
    private float rocketAcc = 1.1f;
    [SerializeField]
    private float rocketEffectDuration = 1;
    //TripleRocket
    [SerializeField]
    private int rocketsShooted = 0;

    //Froze
    [SerializeField]
    private float frozeEffect;
    [SerializeField]
    private float frozeSpeed = 0;
    [SerializeField]
    private float frozeAcc = 0;
    [SerializeField]
    private float frozeEffectDuration = 5;

    //ItemSpawners
    [SerializeField]
    private Transform backSpawn, backSpawnMiddle, backSpawnLast;
    private Vector3 backSpawnVector, backSpawnVectorMiddle, backSpawnVectorLast;
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
        backSpawnVectorMiddle = backSpawnMiddle.transform.position;
        backSpawnVectorLast = backSpawnLast.transform.position;
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
        else if (currentPlayerObject == "triplebanana" || triplebananaDefending == true)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                UseTripleBanana();
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.L))
                {
                    ReleaseTripleBanana();
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

    void UseTripleBanana()
    {
        if (currentPlayerObject == "triplebanana")
        {
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVector, Quaternion.identity) as GameObject).transform.parent = backSpawn.transform;
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVectorMiddle, Quaternion.identity) as GameObject).transform.parent = backSpawnMiddle.transform;
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVectorLast, Quaternion.identity) as GameObject).transform.parent = backSpawnLast.transform;
            //currentIAItem = "none";
            triplebananaDefending = true;

        }
    }

    void ReleaseTripleBanana()
    {
        backSpawn.DetachChildren();
        backSpawnMiddle.DetachChildren();
        backSpawnLast.DetachChildren();
        triplebananaDefending = false;
    }

    void UpdateItems()
    {
        //BananaItemUpdate
        bananaEffect -= Time.deltaTime;

        if (bananaEffect > 0)
        {
            Debug.Log("Bananed");
            //agent.speed = iADefaultSpeed - 30;
        }

        if (bananaEffect < 0 && bananaEffect > -0.1f) //&& startRaceCooldown < 0
        {
            Debug.Log("ResetBanana");
            //navmeshAI.changeVelocityTimer = -1f;
        }

        //RocketItemUpdate
        rocketEffect -= Time.deltaTime;

        if (rocketEffect > 0)
        {
            Debug.Log("Rocked");
            //agent.speed = iADefaultSpeed - 30;
        }

        if (rocketEffect < 0 && rocketEffect > -0.1f) //&& startRaceCooldown < 0
        {
            Debug.Log("ResetRocket");

            //navmeshAI.changeVelocityTimer = -1f;
        }

        //TurboItemUpdate
        turboEffect -= Time.deltaTime;

        if (turboEffect > 0)
        {
            //agent.speed = iADefaultSpeed + turboSpeed;
            //agent.acceleration = iADefaultAcc + turboAcc;
            Debug.Log("Turbo is on");

        }

        if (turboEffect < 0 && turboEffect > -0.1f)
        {
            //navmeshAI.changeVelocityTimer = -1f;
            Debug.Log("Turbo is reset");

        }

        //FrostItemUpdate
        frozeEffect -= Time.deltaTime;

        if (frozeEffect > 0)
        {
            List<GameObject> karts = new List<GameObject>();

            foreach (GameObject kart in GameObject.FindGameObjectsWithTag("Kart"))
            {
                if (kart.Equals(this.gameObject))
                    continue;
                karts.Add(kart);
            }

            for (int i = 0; i < karts.Count; i++)
            {
                karts[i].GetComponent<IA_Item>().iADefaultSpeed = frozeSpeed;
                Debug.Log(karts[i].GetComponent<IA_Item>().name);
            }

        }

        if (frozeEffect < 0 && frozeEffect > 0.1f) //&& startRaceCooldown < 0
        {
            List<GameObject> karts = new List<GameObject>();

            foreach (GameObject kart in GameObject.FindGameObjectsWithTag("Kart"))
            {
                if (kart.Equals(this.gameObject))
                    continue;
                karts.Add(kart);
            }

            for (int i = 0; i < karts.Count; i++)
            {
                karts[i].GetComponent<IA_Item>().iADefaultSpeed = 12;
            }
        }
    }

    void IncreaseSpeedOnMoney()
    {
        //carController.maxSpeed = carController.maxSpeed * ( 1 + money * 0.1f);
        //carController.currentAcc = carController.currentAcc * (1 + money * 0.01f);
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
