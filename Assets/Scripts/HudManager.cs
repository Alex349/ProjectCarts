using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    
    public CarCheckPoints carCheckPoints;
    public Image itemImage;
    public Sprite bananaSprite;
    public Text currentPosition_Text, time_Text, currentLap_Text, totalLaps_Text;
    public float currentPosition, time, secondsCount, minuteCount, milisecondsCount, currentLap, totalLaps;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();
        currentPosition_Text.text = currentPosition.ToString();
        currentLap = carCheckPoints.currentLap ;
        currentLap_Text.text = currentLap.ToString();
        totalLaps_Text.text = totalLaps.ToString();
    }

    public void UpdateTimerUI()
    {
        //set timer UI 3 digits als milisegons
        milisecondsCount += Time.deltaTime * 100;
        time_Text.text = minuteCount + ": " + (int)secondsCount + ", " + (int)milisecondsCount;

        if (milisecondsCount >= 100)
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
        //if (car.currentItem == "banana")
        //{
        //    itemImage.sprite = bananaSprite;
        //}
    }
}
