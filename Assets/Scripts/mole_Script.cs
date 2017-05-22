using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole_Script : MonoBehaviour
{
    private float exitCounter;
    public float moleMaxCounter;

    public Transform initPoint, endPoint;
    public float speed;
    public CapsuleCollider m_capsuleCollider;

	void Start ()
    {
        m_capsuleCollider = GetComponent<CapsuleCollider>();
        m_capsuleCollider.isTrigger = true;
	}
	
	void Update ()
    {
        exitCounter += Time.deltaTime;
        m_capsuleCollider.isTrigger = true;

        if (exitCounter >= moleMaxCounter)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y,
                                                            endPoint.position.y, Time.deltaTime * speed), transform.position.z);
            m_capsuleCollider.isTrigger = false;

            if (exitCounter >= moleMaxCounter + 2)
            {
                exitCounter = 0;
            }            
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y,
                                                            initPoint.position.y, Time.deltaTime * speed), transform.position.z);

            m_capsuleCollider.isTrigger = true;
        }

	}
}
