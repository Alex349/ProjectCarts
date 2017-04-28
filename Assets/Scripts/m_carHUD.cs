using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_carHUD : MonoBehaviour
{
    public CarCheckPoints carCheckPoints;
    public Image itemImage;
    public m_carItem car_Item;
    public Sprite[] itemSpriteList;
    public Text currentPosition_Text, time_Text, currentLap_Text, totalLaps_Text, coins_Text;
    private float currentPosition, time, secondsCount, minuteCount, milisecondsCount, currentLap, totalLaps;

    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();
        UpdateItemUI();
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
        if (car_Item.currentIAItem == "none")
        {
            itemImage.sprite = itemSpriteList[0];
        }
        if (car_Item.currentIAItem == "rocket")
        {
            itemImage.sprite = itemSpriteList[1];
        }
        if (car_Item.currentIAItem == "turbo")
        {
            itemImage.sprite = itemSpriteList[2];
        }
        if (car_Item.currentIAItem == "car")
        {
            itemImage.sprite = itemSpriteList[4];
        }
        if (car_Item.currentIAItem == "coins")
        {
            //car_Item.currentIAItem.money = money + 5;
            car_Item.currentIAItem = "none";

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