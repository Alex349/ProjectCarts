using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationKart : MonoBehaviour {

    public float deltaRotation;
    private Rigidbody m_rigidbody;

	void Start ()
    {
        m_rigidbody = GetComponentInParent<Rigidbody>();		
	}
	
	void Update ()
    {
        m_rigidbody.transform.Rotate(0, deltaRotation * Time.deltaTime, 0);
	}
}
