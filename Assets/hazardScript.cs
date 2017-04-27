using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardScript : MonoBehaviour {

    public float deltaRotation = 0.5f;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.Rotate(Vector3.back * deltaRotation);
    }
}
