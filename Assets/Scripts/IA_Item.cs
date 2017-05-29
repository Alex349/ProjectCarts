using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Item : MonoBehaviour
{
    public string currentIAItem = "none";
    private bool bananaDefending = false, triplebananaDefending = false;
    public int money;

    private PositionManager _positionManager;
    [SerializeField]
    public int myPosition;
    bool keepChecking = true;

    private string lap1Time, lap2Time, lap3Time;
    private float lapCountdown;

    private HudManager hudManager;
    private NavMeshAgent agent;
    private NavMeshAI navmeshAI;

    //Defaults
    [SerializeField]
    public float iADefaultSpeed = 10;
    [SerializeField]
    public float iADefaultAcc = 40;

    [SerializeField]
    private float IaUseItemCooldown = 5, startRaceCooldown = 3;

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
    private GameObject thePlayer;


    //ItemSpawners
    [SerializeField]
    private Transform backSpawn, backSpawnMiddle, backSpawnLast;
    private Vector3 backSpawnVector, backSpawnVectorMiddle, backSpawnVectorLast;
    [SerializeField]
    private Transform frontSpawn;
    private Vector3 frontSpawnVector;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        navmeshAI = GetComponent<NavMeshAI>();
        hudManager = GameObject.Find("HUDManager").GetComponent<HudManager>();
        _positionManager = GameObject.Find("HUDManager").GetComponent<PositionManager>();

        StartCoroutine(CheckLeaderboards());

        agent.speed = 10;
        agent.acceleration = 40;
    }
    // Update is called once per frame
    void Update()
    {
        //CountDowns
        lapCountdown -= Time.deltaTime;
        IaUseItemCooldown -= Time.deltaTime;
        startRaceCooldown -= Time.deltaTime;

        //IA uses the item when the cooldown is over
        if (IaUseItemCooldown < 0)
        {
            //UseItem();
        }

        if (startRaceCooldown < 0)
        {

        }

        agent.speed = iADefaultSpeed;
        agent.acceleration = iADefaultAcc;
 

        //MyPosition
        CheckLeaderboards();

        backSpawnVector = backSpawn.transform.position;
        backSpawnVectorMiddle = backSpawnMiddle.transform.position;
        backSpawnVectorLast = backSpawnLast.transform.position;
        frontSpawnVector = frontSpawn.transform.position;

        UpdateItems();
        IncreaseSpeedOnMoney();

        if (currentIAItem == "banana" || bananaDefending == true)
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
        else if (currentIAItem == "triplebanana" || triplebananaDefending == true)
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

            if (currentIAItem == "none")
            {
                GetRandomItem();
                IaUseItem();
            }
        }

        if (other.tag == "Banana")
        {
            Destroy(other.gameObject);

            bananaEffect = bananaEffectDuration;
        }

        if (other.tag == "Rocket")
        {
            Destroy(other.gameObject);

            rocketEffect = rocketEffectDuration;
        }

        if (other.tag == "RoughTerrain")
        {
            agent.speed = agent.speed / 2;
            agent.acceleration = agent.acceleration / 2;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "RoughTerrain")
        {
            agent.speed = agent.speed * 2;
            agent.acceleration = agent.acceleration * 2;
        }
    }

    void GetRandomItem()
    {
        float rnd = (Random.Range(0f, 1f));

        if (rnd < 0.2)
        {
            currentIAItem = "banana";
        }
        else if (rnd < 0.4)
        {
            currentIAItem = "turbo";
        }
        else if (rnd < 0.6)
        {
            currentIAItem = "rocket";
        }
        else if (rnd < 1)
        {
            if (money > 10)
            {
                currentIAItem = "none";
            }
            else
            {
                currentIAItem = "none";

            }
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
        if (currentIAItem == "rocketstraight")
        {
            Instantiate(Resources.Load("Items/RocketStraight"), frontSpawnVector, frontSpawn.rotation);
            //currentIAItem = "none";
        }

        if (currentIAItem == "rockettracker")
        {
            Instantiate(Resources.Load("Items/RocketTracker"), frontSpawnVector, frontSpawn.rotation);
            //currentIAItem = "none";
        }

        if (currentIAItem == "rockettofirst")
        {
            Instantiate(Resources.Load("Items/RocketToFirst"), frontSpawnVector, frontSpawn.rotation);
            //currentPlayerObject = "none";
        }
        if (currentIAItem == "turbo")
        {
            turboEffect = turboEffectDuration;
            currentIAItem = "none";
        }

        if (currentIAItem == "coins")
        {
            if (money > 10)
            {
                currentIAItem = "none";
                Debug.Log("10");
            }
            else
            {
                money = money + 5;
                currentIAItem = "none";
            }
        }

        if (currentIAItem == "froze")
        {
            frozeEffect = frozeEffectDuration;
            currentIAItem = "none";
        }

        if (currentIAItem == "triplerocketstraight")
        {
            Instantiate(Resources.Load("Items/RocketTracker"), frontSpawnVector, frontSpawn.rotation);

            rocketsShooted++;

            if (rocketsShooted >= 3)
            {
                Debug.Log("NoMoreRockets");
                currentIAItem = "none";
                rocketsShooted = 0;
            }
        }

        if (currentIAItem == "triplerockettracker")
        {
            Instantiate(Resources.Load("Items/RocketTracker"), frontSpawnVector, frontSpawn.rotation);

            rocketsShooted++;

            if (rocketsShooted >= 3)
            {
                Debug.Log("NoMoreRockets");
                currentIAItem = "none";
                rocketsShooted = 0;
            }
        }

        if (currentIAItem == "tripleturbo")
        {
            turboEffect = turboEffectDuration;

            turbosUsed++;

            if (turbosUsed >= 3)
            {
                Debug.Log("NoMoreTurvos");
                currentIAItem = "none";
                turbosUsed = 0;
            }
        }
    }

    void UseBanana()
    {
        if (currentIAItem == "banana")
        {
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVector, Quaternion.identity) as GameObject).transform.parent = backSpawn.transform;
            //currentIAItem = "none";
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
        if (currentIAItem == "triplebanana")
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

            agent.speed = iADefaultSpeed -30;
        }

        if (bananaEffect < 0 && bananaEffect > -0.1f) //&& startRaceCooldown < 0
        {

            navmeshAI.changeVelocityTimer = -1f;
        }

        //RocketItemUpdate
        rocketEffect -= Time.deltaTime;

        if (rocketEffect > 0)
        {
            Debug.Log("Rocked");
            agent.speed = iADefaultSpeed - 30;
        }

        if (rocketEffect < 0 && rocketEffect > -0.1f) //&& startRaceCooldown < 0
        {


            navmeshAI.changeVelocityTimer = -1f;
        }

        //TurboItemUpdate
        turboEffect -= Time.deltaTime;

        if (turboEffect > 0)
        {
            agent.speed = iADefaultSpeed + turboSpeed;
            agent.acceleration = iADefaultAcc + turboAcc;
            Debug.Log("Turbo is on");

        }

        if (turboEffect < 0 && turboEffect > -0.1f)
        {
            navmeshAI.changeVelocityTimer = -1f;


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

            thePlayer = GameObject.FindWithTag("Player");
            thePlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;

            for (int i = 0; i < karts.Count; i++)
            {
                karts[i].GetComponent<IA_Item>().iADefaultSpeed = frozeSpeed;
                Debug.Log(karts[i].GetComponent<IA_Item>().name);
            }

        }

        if (frozeEffect < 0 && frozeEffect > 0.5f) //&& startRaceCooldown < 0
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
            Debug.Log("Unfreez");
            thePlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

        if (frozeEffect < 0)
        {

            thePlayer = GameObject.FindWithTag("Player");
            //OPTIMIZE
            //Debug.Log("Unfreez");
            thePlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    void IncreaseSpeedOnMoney()
    {
        agent.speed = agent.speed * (1 + money * 0.01f);
        agent.acceleration = agent.acceleration * (1 + money * 0.01f);
    }

    void IaUseItem()
    {
        float rnd = (Random.Range(1f, 15f));

        IaUseItemCooldown = rnd;
    }

    public void SetTimeLap()
    {
        if (lap1Time == string.Empty)
        {
            //lap1Time = hudManager.time_Text.text.ToString();
            Debug.Log("Lap1Set");
            lapCountdown = 5;
        }

        if ((lap1Time != string.Empty && lap2Time == string.Empty) && lapCountdown < 0)
        {
            //lap2Time = hudManager.time_Text.text.ToString();
            Debug.Log("Lap2Set");
            lapCountdown = 5;
        }

        if ((lap1Time != string.Empty && lap2Time != string.Empty) && lapCountdown < 0)
        {
            // lap3Time = hudManager.time_Text.text.ToString();
            lapCountdown = 400;
        }
    }

    IEnumerator CheckLeaderboards()
    {
        while (keepChecking)
        {
            //myPosition = _positionManager.racersGO.IndexOf(this.gameObject) + 1;
            yield return new WaitForSeconds(1f);
        }
    }


}
