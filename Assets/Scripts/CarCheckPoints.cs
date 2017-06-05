using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckPoints : MonoBehaviour
{
    public Transform[] checkPointArray; //Checkpoint GameObjects stored as an array
    public int currentCheckpoint = 0; //Current checkpoint
    public int currentLap; //Current lap



    private m_carHUD carHUD;

    void Start()
    {
        carHUD = GameObject.Find("HUDManager").GetComponent<m_carHUD>();
    }


}

