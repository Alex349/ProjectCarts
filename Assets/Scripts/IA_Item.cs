using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Item : MonoBehaviour
{
    public string currentIAItem = "none";
    private bool rainbowPotion = false;
    public int money;

    public bool spin, knockUp, canUseItems;
    public float spinCountDown, knockUpCountDown, spinDuration = 1, knockUpDuration = 1;
    private Animator anim;
    private float slipstream, slipstreamDuration, slipstreamCountDown;
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
    private float frozeAcc = 1000;
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
        anim = GetComponentInChildren<Animator>();
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
        knockUpCountDown -= Time.deltaTime;
        spinCountDown -= Time.deltaTime;

        if (rainbowPotion == false)
        {
            if (spin == true)
            {
                iADefaultSpeed = 0;
                iADefaultAcc = 25;
                canUseItems = false;
                //SpinAnimation
                if (spinCountDown < 0 && spinCountDown > -0.1f)
                {
                    spin = false;
                    canUseItems = true;
                    UseItem();
                    navmeshAI.changeVelocityTimer = -1f;
                }
            }

            if (knockUp == true)
            {
                iADefaultSpeed = 0;
                iADefaultAcc = 5000;
                canUseItems = false;
                //KnockUpAnimation
                if (knockUpCountDown < 0 && knockUpCountDown > -0.1f)
                {
                    knockUp = false;
                    canUseItems = true;
                    UseItem();
                    navmeshAI.changeVelocityTimer = -1f;
                }
            }
        }

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

        if (knockUpDuration < 0)
        {
            anim.SetBool("isKnockedUp", false);
        }

        if (turboEffect < 0 && currentIAItem == "tripleturbo")
        {
            UseItem();
        }
        if (rocketsShooted < 4 && currentIAItem == "triplerocketstraight")
        {
            UseItem();
        }
        if (rocketsShooted < 4 && currentIAItem == "triplerockettracker")
        {
            UseItem();
        }

        //MyPosition
        CheckLeaderboards();

        backSpawnVector = backSpawn.transform.position;
        backSpawnVectorMiddle = backSpawnMiddle.transform.position;
        backSpawnVectorLast = backSpawnLast.transform.position;
        frontSpawnVector = frontSpawn.transform.position;

        UpdateItems();
        IncreaseSpeedOnMoney();

        if (currentIAItem == "banana" )
        {
            UseBanana();
            ReleaseBanana();
        }
        else if (currentIAItem == "triplebanana")
        {
            UseTripleBanana();
            ReleaseTripleBanana();
            currentIAItem = "none";
        }
        else if (currentIAItem == "fakemysterybox")
        {
            UseFakeBox();
            ReleaseFakeBox();
            currentIAItem = "none";
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

            if (spin == false)
            {
                spin = true;
                spinCountDown = spinDuration;
            }

        }

        if (other.tag == "Rocket")
        {
            Destroy(other.gameObject);
            if (knockUp == false)
            {
                anim.SetBool("isKnockedUp", true);
                knockUp = true;
                knockUpCountDown = knockUpDuration;
                turboEffectDuration = -1;
            }

        }

        if (other.tag == "FakeMysteryBox")
        {
            Destroy(other.gameObject);
            if (knockUp == false)
            {
                anim.SetBool("isKnockedUp", true);
                knockUp = true;
                knockUpCountDown = knockUpDuration;
                turboEffectDuration = -1;
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
        if (other.tag == "RoughTerrain" && rainbowPotion == false)
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
            GameObject rocketTracker = (GameObject)Instantiate(Resources.Load("Items/RocketTracker"), frontSpawnVector, frontSpawn.rotation) as GameObject;
            rocketTracker.GetComponent<HoamingRocket>().shooterListPosition = myPosition;
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
            Instantiate(Resources.Load("Items/RocketStraight"), frontSpawnVector, frontSpawn.rotation);

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
            GameObject rocketTracker = (GameObject)Instantiate(Resources.Load("Items/RocketTracker"), frontSpawnVector, frontSpawn.rotation) as GameObject;
            rocketTracker.GetComponent<HoamingRocket>().shooterListPosition = myPosition;

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
            currentIAItem = "none";
        }
    }

    void ReleaseBanana()
    {
        backSpawn.DetachChildren();
    }

    void UseFakeBox()
    {
        if (currentIAItem == "fakemysterybox")
        {
            (Instantiate(Resources.Load("Items/FakeMysteryBox"), backSpawnVectorMiddle, Quaternion.identity) as GameObject).transform.parent = backSpawnMiddle.transform;
            //currentIAItem = "none";

        }
    }

    void ReleaseFakeBox()
    {
        backSpawnMiddle.DetachChildren();
    }

    void UseTripleBanana()
    {
        if (currentIAItem == "triplebanana")
        {
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVector, Quaternion.identity) as GameObject).transform.parent = backSpawn.transform;
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVectorMiddle, Quaternion.identity) as GameObject).transform.parent = backSpawnMiddle.transform;
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVectorLast, Quaternion.identity) as GameObject).transform.parent = backSpawnLast.transform;
            //currentIAItem = "none";

        }
    }

    void ReleaseTripleBanana()
    {
        backSpawn.DetachChildren();
        backSpawnMiddle.DetachChildren();
        backSpawnLast.DetachChildren();
    }



    void UpdateItems()
    {
       
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
                karts[i].GetComponent<IA_Item>().iADefaultSpeed = frozeAcc;
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
        if (canUseItems == true)
        {
            float rnd = (Random.Range(5f, 15f));

            IaUseItemCooldown = rnd;
        }
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
