using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckPoints : MonoBehaviour
{

    public Transform[] checkPointArray; //Checkpoint GameObjects stored as an array
    public int currentCheckpoint = 0; //Current checkpoint
    public int currentLap; //Current lap
    private Vector3 startPos; //Starting position
}
