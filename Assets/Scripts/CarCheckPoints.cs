using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckPoints : MonoBehaviour
{

    public Transform[] checkPointArray; //Checkpoint GameObjects stored as an array
    public int currentCheckpoint = 0; //Current checkpoint
    public int currentLap; //Current lap
    public Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }


}
