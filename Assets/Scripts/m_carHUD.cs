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
    public Sprite[] itemSpriteList;
    public Sprite[] numberSpriteList;
    public Sprite[] numberPosition;
    public Sprite[] currentLapSprite;

    public Text currentPosition_Text, time_Text, currentLap_Text, totalLaps_Text, coins_Text;
    private float currentPosition, time, secondsCount, minuteCount, milisecondsCount, currentLap;

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
        //Laps UI
        currentPosition_Text.text = currentPosition.ToString();

        if (currentPosition.ToString("1") != null)
        {
            positionImage.sprite = numberPosition[0];
        }
        else if (currentPosition.ToString("2") != null)
        {
            positionImage.sprite = numberPosition[1];
        }
        else if (currentPosition.ToString("3") != null)
        {
            positionImage.sprite = numberPosition[2];
        }
        else if (currentPosition.ToString("4") != null)
        {
            positionImage.sprite = numberPosition[3];
        }
        else if (currentPosition.ToString("5") != null)
        {
            positionImage.sprite = numberPosition[4];
        }
        else if (currentPosition.ToString("6") != null)
        {
            positionImage.sprite = numberPosition[5];
        }
        else if (currentPosition.ToString("7") != null)
        {
            positionImage.sprite = numberPosition[6];
        }
        else if (currentPosition.ToString("8") != null)
        {
            positionImage.sprite = numberPosition[7];
        }

        currentLap = carCheckPoints.currentLap;
        currentLap_Text.text = currentLap.ToString();

        if (currentLap.ToString("1") != null)
        {
            currentLapImage.sprite = currentLapSprite[0];
        }
        else if (currentLap.ToString("2") != null)
        {
            currentLapImage.sprite = currentLapSprite[1];
        }
        else if (currentLap.ToString("3") != null)
        {
            currentLapImage.sprite = currentLapSprite[2];
        }
        totalLaps_Text.text = totalLaps.ToString();
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
        if (car_Item.currentPlayerObject == "none")
        {
            itemImage.sprite = itemSpriteList[0];
            itemRandomCounter = 0;
        }
        else
        {
            itemRandomCounter += Time.deltaTime;

          //  itemImage.sprite = itemSpriteList[randomItem];

            if (itemRandomCounter >= 2)
            {
                //if (itemImage.sprite = itemSpriteList[1])
                //{
                //    car_Item.currentPlayerObject = "rocket";
                //}
                //else if (itemImage.sprite = itemSpriteList[2])
                //{
                //    car_Item.currentPlayerObject = "turbo";
                //}
                //else if (itemImage.sprite = itemSpriteList[3])
                //{
                //    car_Item.currentPlayerObject = "banana";
                //}
                //else if (itemImage.sprite = itemSpriteList[4])
                //{
                //    car_Item.currentPlayerObject = "coin";
                //}
                //if (car_Item.currentPlayerObject == "rocket")
                //{
                //    itemImage.sprite = itemSpriteList[1];
                //}
                //else if (car_Item.currentPlayerObject == "turbo")
                //{
                //    itemImage.sprite = itemSpriteList[2];
                //}
                //else if (car_Item.currentPlayerObject == "banana")
                //{
                //    itemImage.sprite = itemSpriteList[3];
                //}
                //else if (car_Item.currentPlayerObject == "coin")
                //{
                //    itemImage.sprite = itemSpriteList[4];
                //}               
            }         
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
            //RectTransform m_desiredRectSize = numberSpriteList[2].textureRect;
            //Rect m_rectSize = numberSpriteList[2].rect;

            numberImage.sprite = numberSpriteList[2];
            //m_desiredRectSize.size = new Vector2(60, 70);
            //m_rectSize.size = m_desiredRectSize.size;
        }
        else if (countDown <= 0)
        {            
            numberImage.sprite = numberSpriteList[3];
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
    void isRandomizing(Sprite[] intemSpriteList)
    {

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