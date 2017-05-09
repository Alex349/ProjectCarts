using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IA_Item : MonoBehaviour
{
    public string currentIAItem = "none";
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
    private float iADefaultSpeed = 10;
    [SerializeField]
    private float iADefaultAcc = 40;

    [SerializeField]
    private float IaUseItemCooldown = 5;

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
    private Transform backSpawn;
    private Vector3 backSpawnVector;
    [SerializeField]
    private Transform frontSpawn;
    private Vector3 frontSpawnVector;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        hudManager = GameObject.Find("HudManager").GetComponent<HudManager>();
        _positionManager = GameObject.Find("HudManager").GetComponent<PositionManager>();

        StartCoroutine(CheckLeaderboards());
    }
    // Update is called once per frame
    void Update()
    {
        //CountDowns
        lapCountdown -= Time.deltaTime;
        IaUseItemCooldown -= Time.deltaTime;

        //IA uses the item when the cooldown is over
        if (IaUseItemCooldown < 0)
        {
           // UseItem();
        }

        //MyPosition
        CheckLeaderboards();
        //Invoke("CheckLeaderboards", 2);


        backSpawnVector = backSpawn.transform.position;
        frontSpawnVector = frontSpawn.transform.position;

        UpdateItems();
        IncreaseSpeedOnMoney();

        if (Input.GetKeyDown(KeyCode.L))
        {
           // Debug.Log("Item");
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
                IaUseItem();
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
                agent.speed = bananaSlowedSpeed;
                agent.acceleration = bananaSlowedAcc;
            }
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
            currentIAItem = "coins";
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
            //currentIAItem = "none";
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

        if (currentIAItem == "coins")
        {
            money = money + 5;
            currentIAItem = "none";

        }
    }

    void UpdateItems()
    {
        //BananaItemUpdate
        bananaEffect -= Time.deltaTime;

        if (bananaEffect < 0)
        {
            //agent.speed = iADefaultSpeed;
            //agent.acceleration = iADefaultAcc;
        }
        //TurboItemUpdate
        turboEffect -= Time.deltaTime;

        if (turboEffect > 0)
        {
            agent.speed = turboSpeed;
            agent.acceleration = turboAcc;

        }

        if (turboEffect < 0)
        {
            //agent.speed = iADefaultSpeed;
            //agent.acceleration = iADefaultAcc;
        }
    }

    void IncreaseSpeedOnMoney()
    {
        agent.speed = agent.speed * (1 + money * 0.1f);
        agent.acceleration = agent.acceleration * (1 + money * 0.4f);
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

    IEnumerator CheckLeaderboards()
    {
        while (keepChecking)
        {
            myPosition = _positionManager.racersGO.IndexOf(this.gameObject) + 1;
            yield return new WaitForSeconds(1f);
        }
    }
}
