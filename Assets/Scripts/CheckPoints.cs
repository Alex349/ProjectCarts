using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour {
    private Transform playerTransform;
    private CarCheckPoints carCheckPoints;
    // Use this for initialization
    void Start ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Kart").transform;
        carCheckPoints = GameObject.FindGameObjectWithTag("Kart").GetComponent<CarCheckPoints>();
    }

    void OnTriggerEnter(Collider col)
    {
        //Is it the Player who enters the collider?
        if (!col.CompareTag("Kart"))
            return; //If it's not the player dont continue

        //Is this transform equal to the transform of checkpointArrays[currentCheckpoint]?
        if (this.transform == playerTransform.GetComponent<CarCheckPoints>().checkPointArray[carCheckPoints.currentCheckpoint].transform)
        {
            //Check so we dont exceed our checkpoint quantity
            if (carCheckPoints.currentCheckpoint + 1 < playerTransform.GetComponent<CarCheckPoints>().checkPointArray.Length)
            {
                //Add to currentLap if currentCheckpoint is 0
                if (carCheckPoints.currentCheckpoint == 0)
                    carCheckPoints.currentLap++;
                carCheckPoints.currentCheckpoint++;
            }
            else
            {
                //If we dont have any Checkpoints left, go back to 0
                carCheckPoints.currentCheckpoint = 0;
            }
        }
    }
}
