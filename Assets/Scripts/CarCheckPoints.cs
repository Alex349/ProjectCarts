using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckPoints : MonoBehaviour
{
    public GameObject gameObjectPoints;
    public Transform[] checkPointArray; //Checkpoint GameObjects stored as an array
    public int currentCheckpoint = 0; //Current checkpoint
    public int currentLap; //Current lap

    void Start()
    {
        gameObjectPoints = GameObject.Find("LapCheckPoints");

        for (int i = 0; i < checkPointArray.Length; i++)
        {
            checkPointArray[i] = gameObjectPoints.GetComponentsInChildren<Transform>()[i];
        }        
    }
}

