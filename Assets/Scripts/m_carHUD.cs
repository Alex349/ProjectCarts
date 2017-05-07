using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_carHUD : MonoBehaviour
{
    //public CarCheckPoints carCheckPoints;
    public CarCheckPoints carCheckPoints;
    public Image itemImage;
    public Image numberImage;

    public m_carItem car_Item;
    public Sprite[] itemSpriteList;
    public Sprite[] numberSpriteList;

    public Text currentPosition_Text, time_Text, currentLap_Text, totalLaps_Text, coins_Text;
    private float currentPosition, time, secondsCount, minuteCount, milisecondsCount, currentLap, totalLaps;

    private float countDown = 3f;
    private bool StartRace = false;
    private m_carController m_car;

    void Start()
    {
        m_car = FindObjectOfType<m_carController>();
    }

    // Update is called once per frame
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
        currentLap = carCheckPoints.currentLap;
        currentLap_Text.text = currentLap.ToString();
        totalLaps_Text.text = totalLaps.ToString();
        //Coins UI
        coins_Text.text = "Coins:" + car_Item.money.ToString();
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
        if (car_Item.currentPlayerObject == "rocket")
        {
            itemImage.sprite = itemSpriteList[1];
        }
        if (car_Item.currentPlayerObject == "turbo")
        {
            itemImage.sprite = itemSpriteList[2];
        }
        if (car_Item.currentPlayerObject == "banana")
        {
            itemImage.sprite = itemSpriteList[3];
        }
        if (car_Item.currentPlayerObject == "coin")
        {
            itemImage.sprite = itemSpriteList[4];
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
            if (Input.GetAxis("Vertical") == 1)
            {
                Debug.Log("Acceleration");
                m_car.m_rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Abs(transform.forward.z)).normalized * m_car.miniTurboForce, ForceMode.Acceleration);
            }

            Destroy(numberImage);
            StartRace = true;
            m_car.currentAcc = 10f;
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