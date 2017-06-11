using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectFollowingCam : MonoBehaviour {

    public Transform m_parentObject;
    public float cameraSpeed;

	void Start ()
    {
        transform.position = m_parentObject.position;
	}
	
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, m_parentObject.position, cameraSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion (m_parentObject.rotation.x, m_parentObject.rotation.y,
                                                                                  m_parentObject.rotation.z, m_parentObject.rotation.w), cameraSpeed * Time.deltaTime);
	}
}
