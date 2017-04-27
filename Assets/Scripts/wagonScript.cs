using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wagonScript : MonoBehaviour {

    public GameObject startPoint, endPoint;
    public float speed = 5f;
    private float lifeTimeCounter;

	void Start ()
    {
        startPoint = GameObject.FindGameObjectWithTag("StartPoint");
        endPoint = GameObject.FindGameObjectWithTag("EndPoint");
        transform.position = startPoint.transform.position;
	}
	
	void Update ()
    {
        lifeTimeCounter += Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, speed * Time.deltaTime);

        if (lifeTimeCounter >= 15)
        {
            Destroy(gameObject);
        }
	}
}
