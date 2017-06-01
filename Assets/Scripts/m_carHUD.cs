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

    private m_carItem car_Item;
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

        PositionIconUpdate();
        UpdateLap();



        //Coins UI
        coins_Text.text = "Coins:" + car_Item.money.ToString();

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
        if (car_Item.currentPlayerObject == "turbo")
        {
            itemImage.sprite = itemSpriteList[1];
        }
        if (car_Item.currentPlayerObject == "turbo" && car_Item.turbosUsed == 2)
        {
            itemImage.sprite = itemSpriteList[2];
        }
        if (car_Item.currentPlayerObject == "tripleturbo")
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
        if (car_Item.currentPlayerObject == "straightrocket")
        {
            itemImage.sprite = itemSpriteList[8];
        }
        if (car_Item.currentPlayerObject == "straightrocket" && car_Item.rocketsShooted == 2)
        {
            itemImage.sprite = itemSpriteList[9];
        }
        if (car_Item.currentPlayerObject == "triplerocketstraight")
        {
            itemImage.sprite = itemSpriteList[10];
        }
        if (car_Item.currentPlayerObject == "rockettracker")
        {
            itemImage.sprite = itemSpriteList[11];
        }
        if (car_Item.currentPlayerObject == "rockettracker" && car_Item.rocketsShooted == 2)
        {
            itemImage.sprite = itemSpriteList[12];
        }
        if (car_Item.currentPlayerObject == "triplerockettracker")
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
    //public void UpdateItemUI()
    //{
    //    if (car_Item.currentPlayerObject == "none")
    //    {
    //        itemImage.sprite = itemSpriteList[0];
    //        itemRandomCounter = 0;
    //    }
    //    else
    //    {
    //        itemRandomCounter += Time.deltaTime;

    //      //  itemImage.sprite = itemSpriteList[randomItem];

    //        if (itemRandomCounter >= 2)
    //        {
    //            //if (itemImage.sprite = itemSpriteList[1])
    //            //{
    //            //    car_Item.currentPlayerObject = "rocket";
    //            //}
    //            //else if (itemImage.sprite = itemSpriteList[2])
    //            //{
    //            //    car_Item.currentPlayerObject = "turbo";
    //            //}
    //            //else if (itemImage.sprite = itemSpriteList[3])
    //            //{
    //            //    car_Item.currentPlayerObject = "banana";
    //            //}
    //            //else if (itemImage.sprite = itemSpriteList[4])
    //            //{
    //            //    car_Item.currentPlayerObject = "coin";
    //            //}
    //            //if (car_Item.currentPlayerObject == "rocket")
    //            //{
    //            //    itemImage.sprite = itemSpriteList[1];
    //            //}
    //            //else if (car_Item.currentPlayerObject == "turbo")
    //            //{
    //            //    itemImage.sprite = itemSpriteList[2];
    //            //}
    //            //else if (car_Item.currentPlayerObject == "banana")
    //            //{
    //            //    itemImage.sprite = itemSpriteList[3];
    //            //}
    //            //else if (car_Item.currentPlayerObject == "coin")
    //            //{
    //            //    itemImage.sprite = itemSpriteList[4];
    //            //}               
    //        }         
    //    }        
    //}

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
            //RectTransform m_desiredRectSize = numberSpriteList[2].textureRect;
            //Rect m_rectSize = numberSpriteList[2].rect;

            numberImage.sprite = numberSpriteList[2];
            //m_desiredRectSize.size = new Vector2(60, 70);
            //m_rectSize.size = m_desiredRectSize.size;
        }
        else if (countDown <= 0)
        {            
            //numberImage.sprite = numberSpriteList[3];
            //numberSpriteList[2].border.Scale(new Vector4(1.2f, 1, 1, 1));

            if (Input.GetAxis("Vertical") == 1 || Input.GetButton("Accelerate"))
            {
                m_car.m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(m_car.m_rigidbody.transform.forward.z)) * m_car.startTurboForce, ForceMode.Acceleration);
            }
            if (countDown <= -0.2f)
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
        //car_Item.myPosition = currentPosition;

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