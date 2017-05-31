using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wagonScript : MonoBehaviour {

    public GameObject startPoint, endPoint;
    public float speed = 5f;
    private float lifeTimeCounter;
    private Vector3 distanceToEnd;
	void Start ()
    {
        startPoint = GameObject.FindGameObjectWithTag("StartPoint");
        endPoint = GameObject.FindGameObjectWithTag("EndPoint");
        transform.position = startPoint.transform.position;
	}
	
	void Update ()
    {
        distanceToEnd = endPoint.transform.position - this.transform.position;

        lifeTimeCounter += Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, speed * Time.deltaTime);
        transform.Rotate(Vector3.forward, 3);

        if (distanceToEnd.magnitude <= 1)
        {
            Destroy(gameObject);
        }
	}
}
