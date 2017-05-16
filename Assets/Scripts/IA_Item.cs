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
    public int myPosition;
    bool keepChecking = true;

    private string lap1Time, lap2Time, lap3Time;
    private float lapCountdown;

    private HudManager hudManager;
    private NavMeshAgent agent;

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
    private float bananaSlowedSpeed = 2;
    [SerializeField]
    private float bananaSlowedAcc = 8;

    //Turbo
    private float turboEffect;
    [SerializeField]
    private float turboSpeed = 30;
    [SerializeField]
    private float turboEffectDuration = 3;
    [SerializeField]
    private float turboAcc = 120;

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
        hudManager = GameObject.Find("HUDManager").GetComponent<HudManager>();
        _positionManager = GameObject.Find("HUDManager").GetComponent<PositionManager>();

        StartCoroutine(CheckLeaderboards());

        agent.speed = 0;
        agent.acceleration = 0;
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
            agent.speed = iADefaultSpeed;
            agent.acceleration = iADefaultAcc;
        }

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

        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);

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
                agent.speed = bananaSlowedSpeed;
                agent.acceleration = bananaSlowedAcc;
            }
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
        if (currentIAItem == "rocket")
        {
            Instantiate(Resources.Load("Items/Rocket"), frontSpawnVector, frontSpawn.rotation);
            currentIAItem = "none";
        }
        if (currentIAItem == "turbo")
        {
            Debug.Log("Turbo");

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
    }

    void UseBanana()
    {
        if (currentIAItem == "banana")
        {
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVector, Quaternion.identity) as GameObject).transform.parent = backSpawn.transform;
            currentIAItem = "none";
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
            currentIAItem = "none";
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

        if (bananaEffect < 0 && startRaceCooldown < 0)
        {
            agent.speed = iADefaultSpeed;
            agent.acceleration = iADefaultAcc;
        }
        //TurboItemUpdate
        turboEffect -= Time.deltaTime;

        if (turboEffect > 0)
        {
            agent.speed = turboSpeed;
            agent.acceleration = turboAcc;

        }

        if (turboEffect < 0 && startRaceCooldown < 0)
        {
            agent.speed = iADefaultSpeed;
            agent.acceleration = iADefaultAcc;
        }
    }

    void IncreaseSpeedOnMoney()
    {
        agent.speed = agent.speed * (1 + money * 0.1f);
        agent.acceleration = agent.acceleration * (1 + money * 0.1f);
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
            myPosition = _positionManager.racersGO.IndexOf(this.gameObject) + 1;
            yield return new WaitForSeconds(1f);
        }
    }


}
