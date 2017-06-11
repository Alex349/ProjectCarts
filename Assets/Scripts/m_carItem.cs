using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_carItem : MonoBehaviour
{

    public string currentPlayerObject = "none";
    private bool bananaDefending = false, triplebananaDefending = false, fakeboxDefending = false;
    public m_carController carController;
    private Rigidbody myRigidbody;
    public float money;
    private GameObject player;

    //Defaults
    [SerializeField]
    private float carDefaultSpeed = 10;
    [SerializeField]
    private float carDefaultAcc = 10;

    private PositionManager _positionManager;
    [SerializeField]
    public int myPosition;
    bool keepChecking = true;

    //Banana
    [SerializeField]
    public float bananaEffect;
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
    public int turbosUsed = 0;

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
    public int rocketsShooted = 0;

    //Froze
    [SerializeField]
    private float frozeEffect;
    [SerializeField]
    private float frozeSpeed = 0;
    [SerializeField]
    private float frozeAcc = 1000;
    [SerializeField]
    private float frozeEffectDuration = 5;

    //ItemSpawners
    [SerializeField]
    private Transform backSpawn, backSpawnMiddle, backSpawnLast;
    private Vector3 backSpawnVector, backSpawnVectorMiddle, backSpawnVectorLast;
    [SerializeField]
    private Transform frontSpawn;
    private Vector3 frontSpawnVector;

    //Laps
    public string lap1Time, lap2Time, lap3Time;
    private float lapCountdown;
    private CarCheckPoints checkPoints;

    private m_carHUD carHUD;
    public Image[] cakeStains;
    private float randomMaxLeftStainSize, randomMaxMiddleStainSize, randomMaxRightStainSize;
    private float startCheckReverseCountdown;

    void Start()
    {
        startCheckReverseCountdown = 10f;
        player = GameObject.FindWithTag("Player");

    //if (m_GM.CameraTravel())
    //{
    carHUD = GameObject.Find("HUDManager").GetComponent<m_carHUD>();
            myRigidbody = carController.GetComponent<Rigidbody>();
            _positionManager = GameObject.Find("HUDManager").GetComponent<PositionManager>();
            checkPoints = GameObject.FindGameObjectWithTag("Player").GetComponent<CarCheckPoints>();
            cakeStains = GameObject.Find("CakeStains").GetComponentsInChildren<Image>(true);
        //}

        foreach (Image img in cakeStains)
            img.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        backSpawnVector = backSpawn.transform.position;
        backSpawnVectorMiddle = backSpawnMiddle.transform.position;
        backSpawnVectorLast = backSpawnLast.transform.position;
        frontSpawnVector = frontSpawn.transform.position;
        startCheckReverseCountdown -= Time.deltaTime;

        myPosition = _positionManager.racersGO.IndexOf(this.gameObject) + 1;

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
        else if (currentPlayerObject == "fakemysterybox" || fakeboxDefending == true)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                UseFakeBox();
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.L))
                {
                    ReleaseFakeBox();
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

        if (other.tag == "Banana")
        {
            Destroy(other.gameObject);

            randomMaxLeftStainSize = (Random.Range(0.2f, 1f));
            randomMaxMiddleStainSize = (Random.Range(0.2f, 1f));
            randomMaxRightStainSize = (Random.Range(0.2f, 1f));

            bananaEffect = bananaEffectDuration;
        }

        if (other.tag == "StartCheckPoint")
        {
            SetTimeLap();
        }

        if (other.tag == "StartCheckPoint" && checkPoints.currentLap == 4)
        {
            if (myPosition == 1)
            {
                carHUD.pos1.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos1.GetComponentInParent<Image>().color = Color.yellow;
                Debug.Log("Player first position" + myPosition);
            }
            else if (myPosition == 2)
            {
                carHUD.pos2.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos2.GetComponentInParent<Image>().color = Color.yellow;
                Debug.Log("Player secont position" + myPosition);
            }
            else if (myPosition == 3)
            {
                carHUD.pos3.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos3.GetComponentInParent<Image>().color = Color.yellow;
                Debug.Log("Player 3 position" + myPosition);
            }
            else if (myPosition == 4)
            {
                carHUD.pos4.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos4.GetComponentInParent<Image>().color = Color.yellow;
                Debug.Log("Player 4 position" + myPosition);
            }
            else if (myPosition == 5)
            {
                carHUD.pos5.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos5.GetComponentInParent<Image>().color = Color.yellow;
                Debug.Log("Player 5 position" + myPosition);
            }
            else if (myPosition == 6)
            {
                carHUD.pos6.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos6.GetComponentInParent<Image>().color = Color.yellow;
                Debug.Log("Player 6 position" + myPosition);
            }
            else if (myPosition == 7)
            {
                carHUD.pos7.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos7.GetComponentInParent<Image>().color = Color.yellow;
                Debug.Log("Player 7 position" + myPosition);
            }
            else if (myPosition == 8)
            {
                carHUD.pos8.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos8.GetComponentInParent<Image>().color = Color.yellow;
                Debug.Log("Player 8 position" + myPosition);
            }
            else if (myPosition == 9)
            {
                carHUD.pos9.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos9.GetComponentInParent<Image>().color = Color.yellow;
                Debug.Log("Player 9 position" + myPosition);
            }
            else if (myPosition == 10)
            {
                carHUD.pos10.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos10.GetComponentInParent<Image>().color = Color.yellow;
                Debug.Log("Player 10 position" + myPosition);
            }
            else if (myPosition == 11)
            {
                carHUD.pos11.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos11.GetComponentInParent<Image>().color = Color.yellow;
                Debug.Log("Player 11 position" + myPosition);
            }
            else if (myPosition == 12)
            {
                carHUD.pos12.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos12.GetComponentInParent<Image>().color = Color.yellow;
                Debug.Log("Player 12 position" + myPosition);
            }
        }

        if (startCheckReverseCountdown < 0)
        {
            if (other.tag == "CheckPoint")
            {
                checkPoints.currentCheckpointReal = other.GetComponent<CheckPoints>().myPositionOnArray;

                if (checkPoints.currentCheckpointReal == checkPoints.previousCheckpoint)
                {
                    carHUD.WrongWay();

                }
                if (checkPoints.currentCheckpointReal == checkPoints.expectedCheckpoint)
                {
                    carHUD.RightWay();
                }



                checkPoints.previousCheckpoint = other.GetComponent<CheckPoints>().myPositionOnArray - 1;

                checkPoints.expectedCheckpoint = other.GetComponent<CheckPoints>().myPositionOnArray + 1;



            }
        }
        else
        {
            checkPoints.currentCheckpointReal = other.GetComponent<CheckPoints>().myPositionOnArray;
            checkPoints.previousCheckpoint = other.GetComponent<CheckPoints>().myPositionOnArray - 1;
            checkPoints.expectedCheckpoint = other.GetComponent<CheckPoints>().myPositionOnArray + 1;
        }


    }

    void GetRandomItem()
    {
        float rnd = (Random.Range(0f, 1f));

        if (rnd < 0.15)
        {
            currentPlayerObject = "banana";
        }
        else if (rnd < 0.2)
        {
            currentPlayerObject = "tripleturbo";
        }
        else if (rnd < 0.3)
        {
            currentPlayerObject = "turbo";
        }
        else if (rnd < 0.4)
        {
            currentPlayerObject = "straightrocket";
        }
        else if (rnd < 0.5)
        {
            currentPlayerObject = "rockettracker";
        }
        else if (rnd < 0.55)
        {
            currentPlayerObject = "rockettofirst";
        }
        else if (rnd < 0.6)
        {
            currentPlayerObject = "triplerocketstraight";
        }
        else if (rnd < 0.65)
        {
            currentPlayerObject = "triplerockettracker";
        }
        else if (rnd < 0.75)
        {
            currentPlayerObject = "fakemysterybox";
        }
        else if (rnd < 0.85)
        {
            currentPlayerObject = "rainbowPotion";
        }
        else if (rnd < 0.90)
        {
            currentPlayerObject = "froze";
        }
        else if (rnd < 1)
        {
            if (money < 10)
            {
                currentPlayerObject = "coin";
            }
            else
            {
                currentPlayerObject = "none";

            }
        }
    }

    void UseItem()
    {
        if (currentPlayerObject == "none")
        {
            turboEffect = turboEffectDuration;
        }
        if (currentPlayerObject == "rainbowPotion")
        {
            currentPlayerObject = "none";
        }
        if (currentPlayerObject == "straightrocket")
        {
            Instantiate(Resources.Load("Items/RocketStraight"), frontSpawnVector, frontSpawn.rotation);
            currentPlayerObject = "none";
        }

        if (currentPlayerObject == "rockettracker")
        {
            GameObject rocketTracker = (GameObject)Instantiate(Resources.Load("Items/RocketTracker"), frontSpawnVector, frontSpawn.rotation) as GameObject;
            rocketTracker.GetComponent<HoamingRocket>().shooterListPosition = myPosition;
            currentPlayerObject = "none";
        }

        if (currentPlayerObject == "rockettofirst")
        {
            Instantiate(Resources.Load("Items/RocketToFirst"), frontSpawnVector, frontSpawn.rotation);
            currentPlayerObject = "none";
        }

        if (currentPlayerObject == "triplerocketstraight")
        {
            Instantiate(Resources.Load("Items/RocketStraight"), frontSpawnVector, frontSpawn.rotation);

            rocketsShooted++;

            if (rocketsShooted >= 3)
            {
                currentPlayerObject = "none";
                rocketsShooted = 0;
            }
        }

        if (currentPlayerObject == "triplerockettracker")
        {
            GameObject rocketTracker = (GameObject)Instantiate(Resources.Load("Items/RocketTracker"), frontSpawnVector, frontSpawn.rotation) as GameObject;
            rocketTracker.GetComponent<HoamingRocket>().shooterListPosition = myPosition;

            rocketsShooted++;

            if (rocketsShooted >= 3)
            {
                currentPlayerObject = "none";
                rocketsShooted = 0;
            }
        }

        if (currentPlayerObject == "turbo")
        {
            Debug.Log("Turbo");
            turboEffect = turboEffectDuration;
            currentPlayerObject = "none";
        }

        if (currentPlayerObject == "tripleturbo")
        {
            turboEffect = turboEffectDuration;

            turbosUsed++;

            if (turbosUsed >= 3)
            {
                currentPlayerObject = "none";
                turbosUsed = 0;
            }
        }

        if (currentPlayerObject == "coin")
        {
            money = money + 5;
            currentPlayerObject = "none";

        }

        if (currentPlayerObject == "froze")
        {
            frozeEffect = frozeEffectDuration;
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

    void UseFakeBox()
    {
        if (currentPlayerObject == "fakemysterybox")
        {
            (Instantiate(Resources.Load("Items/FakeMysteryBox"), backSpawnVectorMiddle, Quaternion.identity) as GameObject).transform.parent = backSpawnMiddle.transform;
            currentPlayerObject = "none";
            fakeboxDefending = true;

        }
    }

    void ReleaseFakeBox()
    {
        backSpawnMiddle.DetachChildren();
        fakeboxDefending = false;
    }

    void UseTripleBanana()
    {
        if (currentPlayerObject == "triplebanana")
        {
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVector, Quaternion.identity) as GameObject).transform.parent = backSpawn.transform;
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVectorMiddle, Quaternion.identity) as GameObject).transform.parent = backSpawnMiddle.transform;
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVectorLast, Quaternion.identity) as GameObject).transform.parent = backSpawnLast.transform;
            currentPlayerObject = "none";
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
            carController.maxSpeed = carController.frontMaxSpeed * 0.5f;

            foreach (Image img in cakeStains)
            {
                img.enabled = true;
            }

            if (cakeStains[0].transform.localScale.x < randomMaxRightStainSize)
            {
                cakeStains[0].transform.localScale += Vector3.one * Time.deltaTime * 2;
            }
            if (cakeStains[1].transform.localScale.x < randomMaxMiddleStainSize)
            {
                cakeStains[1].transform.localScale += Vector3.one * Time.deltaTime * 2;
            }
            if (cakeStains[2].transform.localScale.x < randomMaxRightStainSize)
            {
                cakeStains[2].transform.localScale += Vector3.one * Time.deltaTime * 2;
            }

            carController.currentAcc = bananaSlowedAcc;
        }

        if (bananaEffect < 0 && bananaEffect > -5.5f)
        {
            foreach (Image img in cakeStains)
                img.enabled = false;

            cakeStains[0].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            cakeStains[1].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            cakeStains[2].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            carController.maxSpeed = carController.frontMaxSpeed;
        }

        //RocketItemUpdate
        rocketEffect -= Time.deltaTime;

        if (rocketEffect > 0)
        {
            Debug.Log("Rocked");
            carController.maxSpeed = carController.frontMaxSpeed * 0.5f;
        }

        if (rocketEffect < 0 && rocketEffect > -0.1f) //&& startRaceCooldown < 0
        {
            carController.maxSpeed = carController.frontMaxSpeed;
        }

        //TurboItemUpdate
        turboEffect -= Time.deltaTime;

        if (turboEffect > 0)
        {
            myRigidbody.AddRelativeForce(myRigidbody.transform.forward * carController.turboForce, ForceMode.Acceleration);
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
                karts[i].GetComponent<IA_Item>().iADefaultSpeed = frozeAcc;
                karts[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                Debug.Log(karts[i].GetComponent<IA_Item>().name);
            }
            Debug.Log("Froze");
        }

        if (frozeEffect < 0 && frozeEffect > -0.1f) //&& startRaceCooldown < 0
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
                karts[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
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

        if (lap1Time == string.Empty && checkPoints.currentLap == 1)
        {
            lap1Time = carHUD.time_Text.text.ToString();
            carHUD.stretchTime = 3;
            Debug.Log("Lap1Set");
        }

        if (lap2Time == string.Empty && checkPoints.currentLap == 2)
        {
            lap2Time = carHUD.time_Text.text.ToString();
            carHUD.stretchTime = 3;
            Debug.Log("Lap2Set");
        }

        if (lap3Time == string.Empty && checkPoints.currentLap == 3)
        {
            lap3Time = carHUD.time_Text.text.ToString();
        }
    }
}
