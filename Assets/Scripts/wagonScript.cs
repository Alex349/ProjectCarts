using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wagonScript : MonoBehaviour {

    public GameObject startPoint;
    private GameObject[] endPoints;
    public GameObject endPoint;

    public float speed = 5f;
    private float lifeTimeCounter;
    private Vector3 distanceToEnd;

	void Start ()
    {
        startPoint = GameObject.FindGameObjectWithTag("StartPoint");        
        endPoints = GameObject.FindGameObjectsWithTag("EndPoint");
        endPoint = endPoints[Random.Range(0, 1)];
        transform.position = startPoint.transform.position;
	}
	
	void Update ()
    {       
        distanceToEnd = endPoint.transform.position - this.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, speed * Time.deltaTime);

        lifeTimeCounter += Time.deltaTime;
        transform.Rotate(Vector3.forward, 3);

        if (distanceToEnd.magnitude <= 5 || lifeTimeCounter >= 15)
        {
            Destroy(gameObject);
        }
	}
}
