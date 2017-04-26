using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wagonSpawner : MonoBehaviour
{
    public GameObject wagon;
    private wagonScript m_wagon;
    public float currentTime = 0f;
    public float spawningTime = 5f;
    public GameObject firstWall, lastWall;
    private Vector3 distanceToFirstWall, distanceToLastWall;
    public Transform firstWallInitPoint, firstWallFinalPoint;
    public Transform lastWallInitPoint, lastWallFinalPoint;
    private bool wagonSpawned, firstWagonDestroyed = false;
    private bool firstWallReached, secondWallReached;
    public GameObject instanceWagon;
    public Transform initPoint, finalPoint;
    public float deltaUpdate = 0.01f;

    void Start ()
    {       
        wagonSpawned = false;
        firstWagonDestroyed = false;
        firstWallReached = false;
        secondWallReached = false;
        wagon = GameObject.FindGameObjectWithTag("Wagon");
    }
	
	void Update ()
    {
      //  
      //  m_wagon = wagon.GetComponent<wagonScript>();
        currentTime += Time.deltaTime;

        if (currentTime > spawningTime && firstWagonDestroyed)
        {
            wagonSpawned = true;
            instanceWagon = Instantiate(Resources.Load("Wagon"), initPoint.position, initPoint.rotation, null) as GameObject;
            currentTime = 0;
        }
        if (wagonSpawned == true && firstWagonDestroyed)
        {            
            distanceToFirstWall = firstWall.transform.position - instanceWagon.transform.position;
            distanceToLastWall = lastWall.transform.position - instanceWagon.transform.position;            

            if (distanceToFirstWall.magnitude <= 7)
            {
                firstWall.transform.position = Vector3.MoveTowards(firstWall.transform.position, firstWallFinalPoint.position, deltaUpdate);
                firstWallReached = true;
            }
            else if (distanceToFirstWall.magnitude > 7 && firstWallReached)
            {
                firstWall.transform.position = Vector3.MoveTowards(firstWall.transform.position, firstWallInitPoint.position, deltaUpdate);
                firstWallReached = false;
            }
            if (distanceToLastWall.magnitude <= 7)
            {
                lastWall.transform.position = Vector3.MoveTowards(lastWall.transform.position, lastWallFinalPoint.position, deltaUpdate);
                secondWallReached = true;
            }
            else if (distanceToLastWall.magnitude > 7 && secondWallReached )
            {
                lastWall.transform.position = Vector3.MoveTowards(lastWall.transform.position, lastWallInitPoint.position, deltaUpdate);

                if (distanceToLastWall.magnitude > 30)
                {
                    secondWallReached = false;
                    Destroy(instanceWagon);
                    wagonSpawned = false;
                    currentTime = 0f;
                }
            }                        
        }
        if (firstWagonDestroyed == false && wagon != null)
        {
            distanceToFirstWall = firstWall.transform.position - wagon.transform.position;
            distanceToLastWall = lastWall.transform.position - wagon.transform.position;

            Debug.Log(distanceToFirstWall.magnitude);
            Debug.Log(distanceToLastWall.magnitude);

            if (distanceToFirstWall.magnitude <= 7)
            {
                firstWall.transform.position = Vector3.MoveTowards(firstWall.transform.position, firstWallFinalPoint.position, deltaUpdate);
                firstWallReached = true;
            }
            else if (distanceToFirstWall.magnitude > 7 && firstWallReached)
            {
                firstWall.transform.position = Vector3.MoveTowards(firstWall.transform.position, firstWallInitPoint.position, deltaUpdate);
                firstWallReached = false;
            }
            if (distanceToLastWall.magnitude <= 7)
            {
                lastWall.transform.position = Vector3.MoveTowards(lastWall.transform.position, lastWallFinalPoint.position, deltaUpdate);
                secondWallReached = true;
            }
            else if (distanceToLastWall.magnitude > 7 && secondWallReached)
            {
                lastWall.transform.position = Vector3.MoveTowards(lastWall.transform.position, lastWallInitPoint.position, deltaUpdate);
                
                if (distanceToLastWall.magnitude > 30)
                {
                    secondWallReached = false;
                    Destroy(wagon);
                    firstWagonDestroyed = true;
                }                               
            }      
        }       
    }
}
