using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowScript : MonoBehaviour {

    public Rigidbody m_rigidbody;
    public m_carController m_kart;

	void Start ()
    {
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        
	}
    void FixedUpdate()
    {
        m_rigidbody.transform.position = Vector3.Lerp(m_kart.transform.position, m_rigidbody.transform.position, Time.deltaTime);
    }

}
