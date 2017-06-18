using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckPoints : MonoBehaviour
{
    public GameObject gameObjectPoints;
    public Transform[] checkPointArray; //Checkpoint GameObjects stored as an array
    public int currentCheckpoint = 0; //Current checkpoint
    public int currentLap; //Current lap

    public int currentCheckpointReal = 0, expectedCheckpoint = 0, previousCheckpoint = 0;
    public float checkPointscountDown;

    void Start()
    {
        gameObjectPoints = GameObject.Find("LapCheckPoints");
        checkPointscountDown = 20;

        for (int i = 0; i < checkPointArray.Length; i++)
        {
           // checkPointArray[i] = GameObject.Find("Checkpoint").transform;
            checkPointArray[i] = gameObjectPoints.GetComponentsInChildren<Transform>()[i + 1];

        }        
    }
    void Update()
    {
        checkPointscountDown -= Time.deltaTime;
    }

}

