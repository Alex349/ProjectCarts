using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxRotation : MonoBehaviour {

    private GameObject barrel;

	void Start ()
    {
        barrel = GameObject.Find("Barrel");
	}
	
	
	void Update ()
    {
        transform.Rotate(0, barrel.transform.rotation.y, 0);
	}
}
