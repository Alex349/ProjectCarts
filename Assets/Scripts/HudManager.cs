using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public CarCheckPoints carCheckPoints;
    public Image itemImage;
    public IA_Item ia_Item;
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
        currentLap = carCheckPoints.currentLap ;
        currentLap_Text.text = currentLap.ToString();
        totalLaps_Text.text = totalLaps.ToString();
        //Coins UI
        coins_Text.text = "Coins:" + ia_Item.money.ToString();
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
        if (ia_Item.currentIAItem == "none")
        {
            itemImage.sprite = itemSpriteList[0];
        }
        if (ia_Item.currentIAItem == "rocket")
        {
            itemImage.sprite = itemSpriteList[1];
        }
        if (ia_Item.currentIAItem == "turbo")
        {
            itemImage.sprite = itemSpriteList[2];
        }
        if (ia_Item.currentIAItem == "banana")
        {
            itemImage.sprite = itemSpriteList[3];
        }
        if (ia_Item.currentIAItem == "coin")
        {
            itemImage.sprite = itemSpriteList[4];
        }
    }
}