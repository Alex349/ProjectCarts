using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wagonSpawner : MonoBehaviour
{
    public float currentTime = 0f;
    public float spawningTime = 5f;

    public Transform initPoint, finalPoint;

    void Start ()
    {       

    }
	
	void Update ()
    {    
        currentTime += Time.deltaTime;

        if (currentTime > spawningTime)
        {
            Instantiate(Resources.Load("Hazards/Barrel_Def"), initPoint.position, new Quaternion (0, 0, 0, 0));
            currentTime = 0;
        }            
    }
}
