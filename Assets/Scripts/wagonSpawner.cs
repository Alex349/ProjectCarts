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
            barrelSpawned = true;
            Instantiate(Resources.Load("Hazards/Barrel_Def"), initPoint.position, new Quaternion (0, 0, 0, 0));
            currentTime = 0;
        }
        if (barrelSpawned == true && firstBarrelDestroyed)
        {            
           // distanceToFirstWall = firstWall.transform.position - instanceWagon.transform.position;
           // distanceToLastWall = lastWall.transform.position - instanceWagon.transform.position;            
           //
           // if (distanceToFirstWall.magnitude <= 7 && !firstWallReached)
           // {
           //     firstWall.transform.position = Vector3.Lerp(firstWall.transform.position, firstWallFinalPoint.position, deltaUpdate);
           //     firstWallReached = true;
           // }
           // else if (distanceToFirstWall.magnitude > 7 && firstWallReached)
           // {
           //     firstWall.transform.position = Vector3.Lerp(firstWall.transform.position, firstWallInitPoint.position, deltaUpdate);
           //     firstWallReached = false;
           // }
           // if (distanceToLastWall.magnitude <= 7 && !secondWallReached)
           // {
           //     lastWall.transform.position = Vector3.Lerp(lastWall.transform.position, lastWallFinalPoint.position, deltaUpdate);
           //     secondWallReached = true;
           // }
           // else if (distanceToLastWall.magnitude > 7 && secondWallReached )
           // {
           //     lastWall.transform.position = Vector3.Lerp(lastWall.transform.position, lastWallInitPoint.position, deltaUpdate);
           //
           //     if (distanceToLastWall.magnitude > 30)
           //     {
           //         secondWallReached = false;
           //         Destroy(instanceWagon);
           //         wagonSpawned = false;
           //         currentTime = 0f;
           //     }
           // }                        
        }
        if (firstBarrelDestroyed == false)
        {
           // distanceToFirstWall = firstWall.transform.position - wagon.transform.position;
           // distanceToLastWall = lastWall.transform.position - wagon.transform.position;
           //
           // if (distanceToFirstWall.magnitude <= 7 && !firstWallReached)
           // {
           //     firstWall.transform.position = Vector3.Lerp(firstWall.transform.position, firstWallFinalPoint.position, deltaUpdate);
           //     firstWallReached = true;
           // }
           // else if (distanceToFirstWall.magnitude > 7 && firstWallReached)
           // {
           //     firstWall.transform.position = Vector3.Lerp(firstWall.transform.position, firstWallInitPoint.position, deltaUpdate);
           //     firstWallReached = false;
           // }
           // if (distanceToLastWall.magnitude <= 7)
           // {
           //     lastWall.transform.position = Vector3.Lerp(lastWall.transform.position, lastWallFinalPoint.position, deltaUpdate);
           //     secondWallReached = true;
           // }
           // else if (distanceToLastWall.magnitude > 7 && secondWallReached)
           // {
           //     lastWall.transform.position = Vector3.Lerp(lastWall.transform.position, lastWallInitPoint.position, deltaUpdate);
           //     
           //     if (distanceToLastWall.magnitude > 30)
           //     {
           //         secondWallReached = false;
           //         Destroy(wagon);
           //         firstWagonDestroyed = true;
           //     }                               
           // }      
        }       
    }
}
