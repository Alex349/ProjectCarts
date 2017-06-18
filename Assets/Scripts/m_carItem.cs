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
    private float turboEffect = 0;
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
    [SerializeField]
    public float countdownPotion, rainbowEffectDuration = 8f;
    
    //ItemSpawners
    [SerializeField]
    private Transform backSpawn, backSpawnMiddle, backSpawnLast;
    private Vector3 backSpawnVector, backSpawnVectorMiddle, backSpawnVectorLast;
    [SerializeField]
    private Transform frontSpawn;
    private Vector3 frontSpawnVector;

    //Laps
    public string lap1Time, lap2Time, lap3Time, currentLapTime;
    public Text lap1TimeTxt, lap2TimeTxt, lap3TimeTxt, lapTotalTimeTxt;
    private float lapCountdown, kartFrontMaxSpeed;
    private CarCheckPoints checkPoints;

    private m_carHUD carHUD;
    public Image[] cakeStains;
    private float randomMaxLeftStainSize, randomMaxMiddleStainSize, randomMaxRightStainSize;
    private float startCheckReverseCountdown;
    public ParticleSystem[] ItemSystems;

    //public GameObject[] ItemSystems;

    private float setLapCooldown = 10;  
    public ParticleSystem dotsPotion;
    private bool boxEntered, objThrown;
    private float delayBoxEffect = 3, delayItemAnim = 2;
    private Animator ramsesAnimator;
    private GameObject banana;
    private bool throwObject;


    void Start()
    {
        carHUD = GameObject.Find("HUDManager").GetComponent<m_carHUD>();
        myRigidbody = carController.GetComponent<Rigidbody>();
        _positionManager = GameObject.Find("HUDManager").GetComponent<PositionManager>();
        checkPoints = GameObject.FindGameObjectWithTag("Player").GetComponent<CarCheckPoints>();
        cakeStains = GameObject.Find("CakeStains").GetComponentsInChildren<Image>(true);
        startCheckReverseCountdown = 3f;
        ramsesAnimator = GameObject.Find("RamsesAnimado").GetComponent<Animator>();

        foreach (Image img in cakeStains)
            img.enabled = false;

        for (int i = 0; i < ItemSystems.Length; i++)
        {
           // ItemSystems[i].gameObject.SetActive(false);
        }

        kartFrontMaxSpeed = carController.frontMaxSpeed;

        frozeEffect = -6;
        countdownPotion = -10;
    }

    // Update is called once per frame
    void Update()
    {
        backSpawnVector = backSpawn.transform.position;
        backSpawnVectorMiddle = backSpawnMiddle.transform.position;
        backSpawnVectorLast = backSpawnLast.transform.position;
        frontSpawnVector = frontSpawn.transform.position;

        myPosition = _positionManager.racersGO.IndexOf(this.gameObject) + 1;

        setLapCooldown -= Time.deltaTime;

        UpdateItems();
        IncreaseSpeedOnMoney();

        if (Input.GetButton("ThrowObject"))
        {
            throwObject = true;            
        }
        else if (Input.GetButton("ThrowObject") && throwObject == true)
        {
            throwObject = false;
        }


        if (!boxEntered)
        {
            if (currentPlayerObject == "banana" || bananaDefending == true)
            {
                if (Input.GetKeyDown(KeyCode.L) || throwObject)
                {
                    UseBanana();
                    audioManager.audioInstance.ThrowCake();

                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.L) || !throwObject)
                    {
                        ReleaseBanana();
                        audioManager.audioInstance.ThrowItemGeneral();
                        ramsesAnimator.SetBool("throwingObj?", true);
                        objThrown = true;
                    }
                }
            }
            else if (currentPlayerObject == "triplebanana" || triplebananaDefending == true)
            {
                if (Input.GetKeyDown(KeyCode.L) || throwObject)
                {
                    UseTripleBanana();
                    audioManager.audioInstance.ThrowCake();
                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.L) || !throwObject)
                    {
                        ReleaseTripleBanana();
                        audioManager.audioInstance.ThrowItemGeneral();
                        ramsesAnimator.SetBool("throwingObj?", true);
                        objThrown = true;

                    }
                }
            }
            else if (currentPlayerObject == "fakemysterybox" || fakeboxDefending == true)
            {
                if (Input.GetKeyDown(KeyCode.L) || throwObject)
                {
                    UseFakeBox();
                    audioManager.audioInstance.ThrowCake();
                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.L) || !throwObject)
                    {
                        ReleaseFakeBox();
                        audioManager.audioInstance.ThrowItemGeneral();
                        ramsesAnimator.SetBool("throwingObj?", true);
                        objThrown = true;

                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.L) || throwObject)
                {
                    UseItem();
                    ramsesAnimator.SetBool("throwingObj?", true);
                    objThrown = true;
                }
            }
        }        

        if (boxEntered)
        {
            delayBoxEffect -= Time.deltaTime;
            GetRandomItem();           

            if (delayBoxEffect <= 0 && delayBoxEffect > -1)
            {
                if (currentPlayerObject != "none")
                {
                    if (audioManager.audioInstance.m_audios[36].isPlaying)
                    {
                        audioManager.audioInstance.m_audios[36].Stop();
                    }
                    
                }
                audioManager.audioInstance.ItemChoosed();
                delayBoxEffect = 2;
                boxEntered = false;
            }
        }
        if (objThrown)
        {
            delayItemAnim -= Time.deltaTime;

            if (delayItemAnim <= 0)
            {
                ramsesAnimator.SetBool("throwingObj?", false);
                ramsesAnimator.SetBool("rear?", false);

                objThrown = false;
            }           
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MysteryBox" && currentPlayerObject == "none")
        {
            Destroy(other.gameObject);
            audioManager.audioInstance.PickBox();

            boxEntered = true;

            audioManager.audioInstance.ItemLoop();

            boxEntered = true;           
        }
        else if (other.tag == "MysteryBox" && currentPlayerObject != "none")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "Banana")
        {
            Destroy(other.gameObject);
            audioManager.audioInstance.HitCake();

            randomMaxLeftStainSize = (Random.Range(0.2f, 1f));
            randomMaxMiddleStainSize = (Random.Range(0.2f, 1f));
            randomMaxRightStainSize = (Random.Range(0.2f, 1f));

            money--;

            bananaEffect = bananaEffectDuration;
        }

        if (other.tag == "StartCheckPoint" && setLapCooldown < 0)
        {
            SetTimeLap();
            setLapCooldown = 10;
            audioManager.audioInstance.NewLap();
        }

        if (other.tag == "StartCheckPoint" && checkPoints.currentLap == 3 && checkPoints.currentCheckpoint <= 70)
        {
            audioManager.audioInstance.EndMusic();
            if (myPosition == 1)
            {
                carHUD.pos1.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos1Img.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Player first position" + myPosition);
            }
            else if (myPosition == 2)
            {
                carHUD.pos2.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos2Img.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Player secont position" + myPosition);
            }
            else if (myPosition == 3)
            {
                carHUD.pos3.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos3Img.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Player 3 position" + myPosition);
            }
            else if (myPosition == 4)
            {
                carHUD.pos4.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos4Img.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Player 4 position" + myPosition);
            }
            else if (myPosition == 5)
            {
                carHUD.pos5.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos5Img.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Player 5 position" + myPosition);
            }
            else if (myPosition == 6)
            {
                carHUD.pos6.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos6Img.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Player 6 position" + myPosition);
            }
            else if (myPosition == 7)
            {
                carHUD.pos7.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos7Img.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Player 7 position" + myPosition);
            }
            else if (myPosition == 8)
            {
                carHUD.pos8.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos8Img.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Player 8 position" + myPosition);
            }
            else if (myPosition == 9)
            {
                carHUD.pos9.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos9Img.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Player 9 position" + myPosition);
            }
            else if (myPosition == 10)
            {
                carHUD.pos10.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos10Img.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Player 10 position" + myPosition);
            }
            else if (myPosition == 11)
            {
                carHUD.pos11.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos11Img.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Player 11 position" + myPosition);
            }
            else if (myPosition == 12)
            {
                carHUD.pos12.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);
                carHUD.pos12Img.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Player 12 position" + myPosition);
            }
        }


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

    void GetRandomItem()
    {
        float rnd = (Random.Range(0f, 1f));

        if (rnd < 0.5 && myPosition >= 1)
        {
            currentPlayerObject = "triplebanana";
        }
        if (rnd < 0.15 && myPosition >= 1)
        {
            currentPlayerObject = "triplebanana";
        }
        else if (rnd < 0.2 && myPosition >= 4)
        {
            currentPlayerObject = "tripleturbo";
        }
        else if (rnd < 0.3 && myPosition >= 1)
        {
            currentPlayerObject = "turbo";
        }
        else if (rnd < 0.4 && myPosition >= 1)
        {
            currentPlayerObject = "straightrocket";
        }
        else if (rnd < 0.5 && myPosition >= 1)
        {
            currentPlayerObject = "rockettracker";
        }
        else if (rnd < 0.55 && myPosition >= 8)
        {
            currentPlayerObject = "rockettofirst";
        }
        else if (rnd < 0.6 && myPosition >= 5)
        {
            currentPlayerObject = "triplerocketstraight";
        }
        else if (rnd < 0.65 && myPosition >= 4)
        {
            currentPlayerObject = "triplerockettracker";
        }
        else if (rnd < 0.75 && myPosition >= 2)
        {
            currentPlayerObject = "fakemysterybox";
        }
        else if (rnd < 0.85 && myPosition >= 8)
        {
            currentPlayerObject = "rainbowPotion";
        }
        else if (rnd < 0.90 && myPosition >= 2)
        {
            currentPlayerObject = "froze";
        }
        else if (rnd < 1 && myPosition >= 2)
        {
            if (money < 10)
            {
                currentPlayerObject = "coin";
            }
            else
            {
                currentPlayerObject = "fakemysterybox";
                currentPlayerObject = "none";

            }
        }
    }

    void UseItem()
    {
        if (currentPlayerObject == "none")
        {
            ramsesAnimator.SetBool("throwingObj?", false);
        }
        if (currentPlayerObject == "rainbowPotion")
        {
            currentPlayerObject = "none";
            audioManager.audioInstance.RainbowPotion();            

            countdownPotion = rainbowEffectDuration;
        }
        if (currentPlayerObject == "straightrocket")
        {
            Instantiate(Resources.Load("Items/RocketStraight"), frontSpawnVector, frontSpawn.rotation);
            audioManager.audioInstance.LaunchRocket();
            currentPlayerObject = "none";
        }

        if (currentPlayerObject == "rockettracker")
        {
            GameObject rocketTracker = (GameObject)Instantiate(Resources.Load("Items/RocketTracker"), frontSpawnVector, frontSpawn.rotation) as GameObject;
            rocketTracker.GetComponent<HoamingRocket>().shooterListPosition = myPosition;
            audioManager.audioInstance.LauchHoamingRocket();
            currentPlayerObject = "none";
        }

        if (currentPlayerObject == "rockettofirst")
        {
            Instantiate(Resources.Load("Items/RocketToFirst"), frontSpawnVector, frontSpawn.rotation);
            audioManager.audioInstance.LaunchRocketFirst();
            currentPlayerObject = "none";
        }

        if (currentPlayerObject == "triplerocketstraight")
        {
            Instantiate(Resources.Load("Items/RocketStraight"), frontSpawnVector, frontSpawn.rotation);
            audioManager.audioInstance.LaunchRocket();
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
            audioManager.audioInstance.LauchHoamingRocket();

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
            audioManager.audioInstance.TurboItem();
            currentPlayerObject = "none";
        }

        if (currentPlayerObject == "tripleturbo")
        {
            turboEffect = turboEffectDuration;
            audioManager.audioInstance.StopTurbo();
            audioManager.audioInstance.TurboItem();

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
            audioManager.audioInstance.CoinSound();
            currentPlayerObject = "none";
            ItemSystems[2].gameObject.SetActive(true);
            ItemSystems[2].Play();
        }

        if (currentPlayerObject == "froze")
        {
            frozeEffect = frozeEffectDuration;
            audioManager.audioInstance.FrozeEffect();
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
            //(Instantiate(Resources.Load("Items/Banana"), backSpawnVector, Quaternion.identity) as GameObject).transform.parent = backSpawn.transform;
            //Instantiate(Resources.Load("Items/Banana"), backSpawnVector, Quaternion.identity, backSpawn.transform);
            (Instantiate(Resources.Load("Items/Banana"), backSpawnVector, Quaternion.identity) as GameObject).transform.parent = backSpawn.transform;
            //(Instantiate(Resources.Load("Items/Banana"), backSpawnVector, Quaternion.identity) as GameObject).transform.parent = backSpawn.transform;

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
            (Instantiate(Resources.Load("Items/FakeMysteryBox"), backSpawnVector, Quaternion.identity) as GameObject).transform.parent = backSpawn.transform;

            currentPlayerObject = "none";
            fakeboxDefending = true;

        }
    }

    void ReleaseFakeBox()
    {
        backSpawn.DetachChildren();
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
            myRigidbody.AddForce(myRigidbody.transform.forward * carController.turboForce, ForceMode.Acceleration);

            for (int i = 0; i < carController.turboEffects.Length; i++)
            {
                carController.turboEffects[i].Play();
            }
        }
        else if (turboEffect < 0 && turboEffect > -1)
        {
            for (int i = 0; i < carController.turboEffects.Length; i++)
            {
                carController.turboEffects[i].Stop();
            }
        }

        countdownPotion -= Time.deltaTime;

        if (countdownPotion > 0)
        {
            carController.frontMaxSpeed = 25;
            Debug.Log("is POTION IN");
            ItemSystems[0].gameObject.SetActive(true);
            ItemSystems[6].gameObject.SetActive(true);

            if (!ItemSystems[0].isPlaying || !ItemSystems[6].isPlaying)
            {
                ItemSystems[0].Play();
                ItemSystems[6].Play();
            }
          
            ParticleSystem.VelocityOverLifetimeModule dotsVelocity = dotsPotion.velocityOverLifetime;
            dotsVelocity.z = 0;
            dotsVelocity.x = 0;

            if (carController.rightDrift)
            {
                dotsVelocity.xMultiplier = carController.currentSpeed/3;
            }
            else if (carController.leftDrift)
            {
                dotsVelocity.xMultiplier = -carController.currentSpeed/3;
            }
        }
        else if (countdownPotion < 0 && countdownPotion > -1)
        {
            carController.frontMaxSpeed = kartFrontMaxSpeed;

            ItemSystems[0].Stop();
            ItemSystems[6].Stop();
            ItemSystems[7].Stop();

            ItemSystems[0].gameObject.SetActive(false);
            ItemSystems[6].gameObject.SetActive(false);
            ItemSystems[7].gameObject.SetActive(false);

            audioManager.audioInstance.StopPotion();

            ItemSystems[0].gameObject.SetActive(false);
            ItemSystems[6].gameObject.SetActive(false);


            countdownPotion = 0;
        }
        //FrostItemUpdate
        frozeEffect -= Time.deltaTime;
        List<GameObject> karts = new List<GameObject>();

        if (frozeEffect > 0)
        {         
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
            }
        }
        
        if (frozeEffect < 0 && frozeEffect > -1f) //&& startRaceCooldown < 0
        {
            foreach (GameObject kart in GameObject.FindGameObjectsWithTag("Kart"))
            {
                if (kart.Equals(this.gameObject))
                    continue;
                karts.Add(kart);
            }

            for (int i = 0; i < karts.Count; i++)
            {
                karts[i].GetComponent<IA_Item>().iADefaultSpeed = 12;

                if (frozeEffect < -0.9f)
                {
                    karts[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    karts[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    karts[i].transform.GetChild(0).GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
                }               
            }
        }
        if (frozeEffect <= -5)
        {
            for (int i = 0; i < karts.Count; i++)
            {
                

                karts[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
        }
        

    }

    void IncreaseSpeedOnMoney()
    {
        carController.maxSpeed = carController.frontMaxSpeed * ( 1 + money * 0.1f);
        //carController.currentAcc = carController.currentAcc * (1 + money * 0.01f);
    }

    public void SetTimeLap()
    {

        if (lap1Time == string.Empty && checkPoints.currentLap == 1)
        {
            lap1Time = carHUD.time_Text.text.ToString();
            lap1TimeTxt.text = carHUD.minuteCountLap.ToString("00") + " : " + carHUD.secondsCountLap.ToString("00") + ", " + carHUD.milisecondsCountLap.ToString("000").Truncate(3);

            carHUD.milisecondsCountLap = 0;
            carHUD.secondsCountLap = 0;
            carHUD.minuteCountLap = 0;

            carHUD.stretchTime = 3;
            Debug.Log("Lap1Set");
        }

        if (lap2Time == string.Empty && checkPoints.currentLap == 2)
        {
            lap2Time = carHUD.time_Text.text.ToString();
            lap2TimeTxt.text = carHUD.minuteCountLap.ToString("00") + " : " + carHUD.secondsCountLap.ToString("00") + ", " + carHUD.milisecondsCountLap.ToString("000").Truncate(3);

            carHUD.milisecondsCountLap = 0;
            carHUD.secondsCountLap = 0;
            carHUD.minuteCountLap = 0;

            carHUD.stretchTime = 3;
            Debug.Log("Lap2Set");
        }

        if (lap3Time == string.Empty && checkPoints.currentLap == 3)
        {
            lap3Time = carHUD.time_Text.text.ToString();
            lap3TimeTxt.text = carHUD.minuteCountLap.ToString("00") + " : " + carHUD.secondsCountLap.ToString("00") + ", " + carHUD.milisecondsCountLap.ToString("000").Truncate(3);

            carHUD.milisecondsCountLap = 0;
            carHUD.secondsCountLap = 0;
            carHUD.minuteCountLap = 0;

            lapTotalTimeTxt.text = carHUD.minuteCount.ToString("00") + " : " + carHUD.secondsCount.ToString("00") + ", " + carHUD.milisecondsCount.ToString("000").Truncate(3);

        }
    }
}
