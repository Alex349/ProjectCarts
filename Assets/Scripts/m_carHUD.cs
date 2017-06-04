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

    public m_carItem car_Item;
    private CarCheckPoints car_Checkpoint;
    public Sprite[] itemSpriteList;
    public Sprite[] numberSpriteList;
    public Sprite[] numberPosition;
    public Sprite[] currentLapSprite;

    public Text time_Text, totalLaps_Text, coins_Text;
    private float time, secondsCount, minuteCount, milisecondsCount;
    private int currentPosition;

    [SerializeField]
    private float totalLaps;

    private float countDown = 3f;
    public bool StartRace = false;
    private m_carController m_car;
    private int randomItem = 0;
    private float itemRandomCounter = 0;
    //private float scrollSpeed = 3f;

    void Start()
    {
        m_car = FindObjectOfType<m_carController>();
        car_Item = FindObjectOfType<m_carItem>();
        car_Checkpoint = GameObject.FindGameObjectWithTag("Player").GetComponent<CarCheckPoints>();
        InvokeRepeating("PositionIconUpdate", 3.0f, 0.5f);
    }

    void Update()
    {
        if (StartRace == false)
        {
            CountDown();
        }
        else if (StartRace == true)
        {
            UpdateTimerUI();
            UpdateItemUI();
        }

        //PositionIconUpdate();
        UpdateLap();



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


        randomItem = UnityEngine.Random.Range(1, itemSpriteList.Length);
    }

    public void UpdateTimerUI()
    {
        //set timer UI 3 digits als milisegons
        milisecondsCount += Time.deltaTime * 1000;

        time_Text.text = minuteCount + ": " + (int)secondsCount + ", " + milisecondsCount.ToString("000").Truncate(3);

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

        countDown -= Time.deltaTime;

        m_car.currentAcc = 0;

        if (countDown <= 3 && countDown > 2)
        {
            numberImage.sprite = numberSpriteList[0];
        }
        else if (countDown <= 2 && countDown > 1)
        {
            numberImage.sprite = numberSpriteList[1];
        }
        else if (countDown <= 1 && countDown > 0)
        {
            numberImage.sprite = numberSpriteList[2];
        }
        else if (countDown <= 0)
        {            
            numberImage.sprite = numberSpriteList[3];

            if (Input.GetAxis("Vertical") == 1 || Input.GetButton("Accelerate"))
            {
                m_car.m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(m_car.m_rigidbody.transform.forward.z)) * m_car.startTurboForce, ForceMode.Acceleration);
            }
            if (countDown <= -1f)
            {
                Destroy(numberImage);
                StartRace = true;
            }            
        }       
    }

    void IsRandomizing(Sprite[] intemSpriteList)
    {

    }

    void PositionIconUpdate ()
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
}


public static class StringExt
{
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }
}