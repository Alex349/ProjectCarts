using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{

    /*
  
    Yeah you have to stagger that into a series of checks.
    The AI and player vehicles would continuously check: 
    1.) What lap is this AI or player currently on
    2.) Which waypoint is this AI or player currently at
    3.) What is the current distance to the next waypoint for this AI or the player

     */
    public Transform playerTransform;
    public CarCheckPoints carCheckPoints;
    public int myPositionOnArray;
    public CarCheckPoints playerArray;

    void Start()
    {
        playerArray = GameObject.FindGameObjectWithTag("Player").GetComponent<CarCheckPoints>();
        myPositionOnArray = System.Array.IndexOf(playerArray.checkPointArray, this.gameObject.transform);
    }


    void OnTriggerEnter(Collider other)
    {

        //Is it the Player who enters the collider?
        if (other.CompareTag("Player") || other.CompareTag("Kart"))
        {
            playerTransform = other.GetComponent<Transform>();
            carCheckPoints = other.GetComponent<CarCheckPoints>();
        }
        else
        {
            return;
        }

        if (other.CompareTag("Kart") && carCheckPoints.currentCheckpointReal == 73 && carCheckPoints.checkPointscountDown < 0)
        {
            carCheckPoints.currentLap++;
            carCheckPoints.checkPointscountDown = 20;
            Debug.Log("KartLapIncreased");
        }

        //Is this transform equal to the transform of checkpointArrays[currentCheckpoint]?

        if (transform == carCheckPoints.checkPointArray[carCheckPoints.currentCheckpoint].transform)
        {
            //Check so we dont exceed our checkpoint quantity
            if (carCheckPoints.currentCheckpoint + 1 < carCheckPoints.checkPointArray.Length)
            {
                //Add to currentLap if currentCheckpoint is 0
                //if (carCheckPoints.currentCheckpoint == 0)
                //{
                //    carCheckPoints.currentLap++;
                //}

                carCheckPoints.currentCheckpoint++;
            }
            else if (carCheckPoints.currentCheckpoint == carCheckPoints.checkPointArray.Length)
            {

            }
            else if (other.CompareTag("Player"))
            {
                //If we dont have any Checkpoints left, go back to 0
                carCheckPoints.currentCheckpoint = 0;
                carCheckPoints.currentLap++;
                Debug.Log("PlayerLapIncreased");
            }
        }
    }
}
