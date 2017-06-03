using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelScenario : MonoBehaviour {

    public float deltaRotation;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.Rotate(new Vector3(0, 0, deltaRotation * Time.deltaTime));
	}
}
