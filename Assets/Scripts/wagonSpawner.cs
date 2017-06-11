using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wagonSpawner : MonoBehaviour
{
    private wagonScript m_barrel;
    public float currentTime = 0f;
    public float spawningTime = 5f;
    //public GameObject firstWall, lastWall;
    //private Vector3 distanceToFirstWall, distanceToLastWall;
    //public Transform firstWallInitPoint, firstWallFinalPoint;
    //public Transform lastWallInitPoint, lastWallFinalPoint;
    private bool barrelSpawned, firstBarrelDestroyed = false;
    private bool firstWallReached, secondWallReached;
    private GameObject instanceWagon;
    public Transform initPoint, finalPoint;
    public float deltaUpdate = 5f;

    void Start ()
    {       
        barrelSpawned = false;
        firstBarrelDestroyed = false;
        firstWallReached = false;
        secondWallReached = false;
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
