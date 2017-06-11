using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_carHUD : MonoBehaviour
{
    public CarCheckPoints carCheckPoints;
    public Image itemImage;
    public Image numberImage;
    public Image positionImage;
    public Image currentLapImage;

    public GameObject timeNumbers;
    public GameObject LeaderboardEndGO, PlayerLapRecap, WrongWayImg;

    public m_carItem car_Item;
    private CarCheckPoints car_Checkpoint;
    private PositionManager positionManager;
    public Sprite[] itemSpriteList;
    public Sprite[] numberSpriteList;
    public Sprite[] numberPosition;
    public Sprite[] currentLapSprite;

    public Text time_Text, totalLaps_Text, coins_Text;
    public float time, secondsCount, minuteCount, milisecondsCount;
    private int currentPosition;

    [SerializeField]
    private float totalLaps;

    [HideInInspector]public float countDown = 3f;
    public bool StartRace = false;
    private m_carController m_car;
    private int randomItem = 0;
    private float itemRandomCounter = 0;
    [HideInInspector] public float stretchTime = 0;
    public float scaleSpeed = 1f;
    private bool hasImageChanged0, hasImageChanged1, hasImageChanged2, hasImageChanged3;

    public Text Lap1Text, Lap2Text, Lap3Text, TotalText;

    public Text pos1, pos2, pos3, pos4, pos5, pos6, pos7, pos8, pos9, pos10, pos11, pos12;
    public Image pos1Img, pos2Img, pos3Img, pos4Img, pos5Img, pos6Img, pos7Img, pos8Img, pos9Img, pos10Img, pos11Img, pos12Img;

    //private float scrollSpeed = 3f;

    void Start()
    {

        m_car = FindObjectOfType<m_carController>();
        timeNumbers = GameObject.Find("Time");
        car_Item = FindObjectOfType<m_carItem>();
        car_Checkpoint = GameObject.FindGameObjectWithTag("Player").GetComponent<CarCheckPoints>();
        positionManager = GameObject.Find("HUDManager").GetComponent<PositionManager>();
        InvokeRepeating("PositionIconUpdate", 3.0f, 0.5f);
        LeaderboardEndGO = GameObject.Find("LeaderboardEnd");
        LeaderboardEndGO.SetActive(false);
        PlayerLapRecap = GameObject.Find("PlayerLapRecap");
        PlayerLapRecap.SetActive(false);

        WrongWayImg = GameObject.Find("WrongWay");
        WrongWayImg.SetActive(false);
    }

    void Update()
    {
        if (m_GM.managerReady)
        {     
            stretchTime -= Time.deltaTime;
            countDown -= Time.deltaTime;

            if (countDown >= -0.5f)
            {
                CountDown();
            }
        }           
        if (countDown >= -0.5f)
        {
            CountDown();
        }      


        if (carCheckPoints.currentLap >= 4)
        {
            StartRace = false;
        }


        if (StartRace == true)
        {
            milisecondsCount += Time.deltaTime * 1000;
            //audioManager.audioInstance.Music1stLap();

            if (milisecondsCount >= 999)
            {
                secondsCount++;
                milisecondsCount = 0;
            }
            else if (secondsCount >= 60)
            {
                minuteCount++;
                secondsCount = 0;
            }

            UpdateItemUI();
            UpdateLap();
            CoinUIFix();

            if (stretchTime > 0)
            {
                StretchTime();
                time_Text.color = Color.yellow;
            }
            else
            {
                UpdateTimerUI();
                time_Text.color = Color.white;
            }

            if (Input.GetKey(KeyCode.U))
            {
                LeaderboardEnd();
            }
            else if (carCheckPoints.currentLap == 4)
            {
                LeaderboardEnd();
            }
            else
            {
                LeaderboardEndGO.SetActive(false);
            }
            randomItem = UnityEngine.Random.Range(1, itemSpriteList.Length);
        }       

        if (Input.GetKey(KeyCode.U))
        {
            LeaderboardEnd();
        }
        else if (carCheckPoints.currentLap == 4)
        {
           LeaderboardEnd();
        }
        else
        {
            LeaderboardEndGO.SetActive(false);
        }

       // randomItem = UnityEngine.Random.Range(1, itemSpriteList.Length);

    }

    public void UpdateTimerUI()
    {
        time_Text.text = minuteCount.ToString("00") + " : " + secondsCount.ToString("00") + ", " + milisecondsCount.ToString("000").Truncate(3);
    }

    public void UpdateItemUI()
    {
        if (car_Item.currentPlayerObject == "none")
        {
            itemImage.sprite = itemSpriteList[0];
        }
        if (car_Item.currentPlayerObject == "turbo" || (car_Item.currentPlayerObject == "tripleturbo" && car_Item.turbosUsed == 2))
        {
            itemImage.sprite = itemSpriteList[1];
        }
        if (car_Item.currentPlayerObject == "tripleturbo" && car_Item.turbosUsed == 1)
        {
            itemImage.sprite = itemSpriteList[2];
        }
        if (car_Item.currentPlayerObject == "tripleturbo" && car_Item.turbosUsed == 0)
        {
            itemImage.sprite = itemSpriteList[3];
        }
        if (car_Item.currentPlayerObject == "coin")
        {
            itemImage.sprite = itemSpriteList[4];
        }
        if (car_Item.currentPlayerObject == "banana")
        {
            itemImage.sprite = itemSpriteList[5];
        }
        if (car_Item.currentPlayerObject == "triplebanana")
        {
            itemImage.sprite = itemSpriteList[6];
        }
        if (car_Item.currentPlayerObject == "fakemysterybox")
        {
            itemImage.sprite = itemSpriteList[7];
        }
        if (car_Item.currentPlayerObject == "straightrocket" || (car_Item.currentPlayerObject == "triplerocketstraight" && car_Item.rocketsShooted == 2))
        {
            itemImage.sprite = itemSpriteList[8];
        }
        if (car_Item.currentPlayerObject == "triplerocketstraight" && car_Item.rocketsShooted == 1)
        {
            itemImage.sprite = itemSpriteList[9];
        }
        if (car_Item.currentPlayerObject == "triplerocketstraight" && car_Item.rocketsShooted == 0)
        {
            itemImage.sprite = itemSpriteList[10];
        }
        if (car_Item.currentPlayerObject == "rockettracker" || (car_Item.currentPlayerObject == "triplerockettracker" && car_Item.rocketsShooted == 2))
        {
            itemImage.sprite = itemSpriteList[11];
        }
        if (car_Item.currentPlayerObject == "triplerockettracker" && car_Item.rocketsShooted == 1)
        {
            itemImage.sprite = itemSpriteList[12];
        }
        if (car_Item.currentPlayerObject == "triplerockettracker" && car_Item.rocketsShooted == 0)
        {
            itemImage.sprite = itemSpriteList[13];
        }
        if (car_Item.currentPlayerObject == "rockettofirst")
        {
            itemImage.sprite = itemSpriteList[14];
        }
        if (car_Item.currentPlayerObject == "froze")
        {
            itemImage.sprite = itemSpriteList[15];
        }
        if (car_Item.currentPlayerObject == "rainbowPotion")
        {
            itemImage.sprite = itemSpriteList[16];
        }
    }

    public void UpdateLap()
    {
        if (car_Checkpoint.currentLap == 0)
        {
            currentLapImage.sprite = currentLapSprite[0];
        }
        if (car_Checkpoint.currentLap == 1)
        {
            currentLapImage.sprite = currentLapSprite[0];
        }
        if (car_Checkpoint.currentLap == 2)
        {
            currentLapImage.sprite = currentLapSprite[1];
        }
        if (car_Checkpoint.currentLap == 3)
        {
            currentLapImage.sprite = currentLapSprite[2];
        }
    }

    void CountDown()
    {        
       //audioManager.audioInstance.countDownSound();

        if (countDown <= 3 && countDown > 2.1 || countDown <= 2 && countDown > 1.1 || countDown <= 1 && countDown > 0.1 || countDown < 0 && countDown > -0.8)
        {
            numberImage.transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
        }

        if (countDown <= 3 && countDown > 2)
        {            
            if (hasImageChanged0 == false)
            {
                numberImage.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                hasImageChanged0 = true;
            }

            numberImage.sprite = numberSpriteList[0];
        }
        else if (countDown <= 2 && countDown > 1)
        {           
            if (hasImageChanged1 == false)
            {
                numberImage.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                hasImageChanged1 = true;  
            }

            numberImage.sprite = numberSpriteList[1];
        }
        else if (countDown <= 1 && countDown > 0)
        {
            if (hasImageChanged2 == false)
            {
                numberImage.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                hasImageChanged2 = true;
            }

            numberImage.sprite = numberSpriteList[2];
        }
        else if (countDown <= 0 && StartRace == false)
        {         
            if (Input.GetAxis("Vertical") == 1 || Input.GetButton("Accelerate"))
            {
                m_car.m_rigidbody.AddRelativeForce(new Vector3 (0, 0, m_car.m_rigidbody.transform.forward.z) * m_car.startTurboForce, ForceMode.Acceleration);
                //Debug.Log("is accelerating");

                audioManager.audioInstance.YeahPJ();

                audioManager.audioInstance.CarHorn();

            }
            if (hasImageChanged3 == false)
            {
                numberImage.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                hasImageChanged3 = true;
            }

            numberImage.sprite = numberSpriteList[3];            
            
            StartRace = true;

            if (countDown <= -10)
            {
               GameObject.Find("InitialCheckPoint").transform.gameObject.tag = "StartCheckPoint";
            }
        }
        else if (countDown <= -1.1f)
        {
            numberImage.GetComponent<Image>().enabled = false;
        }
    }

    void IsRandomizing(Sprite[] intemSpriteList)
    {

    }

    void PositionIconUpdate()
    {
        if (car_Item.myPosition == 1)
        {
            positionImage.sprite = numberPosition[0];
        }
        if (car_Item.myPosition == 2)
        {
            positionImage.sprite = numberPosition[1];
        }
        if (car_Item.myPosition == 3)
        {
            positionImage.sprite = numberPosition[2];
        }
        if (car_Item.myPosition == 4)
        {
            positionImage.sprite = numberPosition[3];
        }
        if (car_Item.myPosition == 5)
        {
            positionImage.sprite = numberPosition[4];
        }
        if (car_Item.myPosition == 6)
        {
            positionImage.sprite = numberPosition[5];
        }
        if (car_Item.myPosition == 7)
        {
            positionImage.sprite = numberPosition[6];
        }
        if (car_Item.myPosition == 8)
        {
            positionImage.sprite = numberPosition[7];
        }
        if (car_Item.myPosition == 9)
        {
            positionImage.sprite = numberPosition[8];
        }
        if (car_Item.myPosition == 10)
        {
            positionImage.sprite = numberPosition[9];
        }
        if (car_Item.myPosition == 11)
        {
            positionImage.sprite = numberPosition[10];
        }
        if (car_Item.myPosition == 12)
        {
            positionImage.sprite = numberPosition[11];
        }
    }

    void StretchTime()
    {
        timeNumbers.transform.localScale = new Vector3(PingPong(Time.time * 0.5f, 0.9f, 1.1f), PingPong(Time.time * 0.5f, 0.9f, 1.1f), PingPong(Time.time * 0.5f, 0.9f, 1.1f));
    }

    void LeaderboardEnd()
    {
        PlayerLapRecap.SetActive(false);
        LeaderboardEndGO.SetActive(true);
        Debug.Log("U");
    }

    void LapRecap()
    {
        Lap1Text.text = car_Item.lap1Time.ToString();
        Lap2Text.text = car_Item.lap2Time.ToString();
        Lap3Text.text = car_Item.lap3Time.ToString();

        if (float.Parse(car_Item.lap1Time) < float.Parse(car_Item.lap2Time))
        {

        }
        PlayerLapRecap.SetActive(true);
    }

    void CoinUIFix()
    {
        //Coins UI
        coins_Text.text = car_Item.money.ToString();

        if (coins_Text.text == "10")
        {
            coins_Text.color = Color.blue;
        }
        else
        {
            coins_Text.color = Color.white;
        }
        if (coins_Text.text == "11")
        {
            coins_Text.text = "10";
        }
        else if (coins_Text.text == "12")
        {
            coins_Text.text = "10";
        }
        else if (coins_Text.text == "13")
        {
            coins_Text.text = "10";
        }
        else if (coins_Text.text == "14")
        {
            coins_Text.text = "10";
        }
        else if (coins_Text.text == "15")
        {
            coins_Text.text = "10";
        }
    }

    public void WrongWay()
    {
        WrongWayImg.SetActive(true);
    }

    public void RightWay()
    {
        WrongWayImg.SetActive(false);
    }

    float PingPong(float aValue, float aMin, float aMax)
    {
        return Mathf.PingPong(aValue, aMax - aMin) + aMin;
    }
}


public static class StringExt
{
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }
}