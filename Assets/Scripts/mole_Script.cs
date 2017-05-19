using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole_Script : MonoBehaviour
{
    private float exitCounter;
    public float moleMaxCounter;
    private Rigidbody mole_rigidbody;
    public Transform initPoint, endPoint;
    public float speed;
    //public CapsuleCollider m_capsuleCollider;

	void Start ()
    {
        mole_rigidbody = GetComponent<Rigidbody>();
        //m_capsuleCollider = GetComponent<CapsuleCollider>();
        //m_capsuleCollider.isTrigger = true;
	}
	
	void Update ()
    {
        exitCounter += Time.deltaTime;
        //m_capsuleCollider.isTrigger = true;

        if (exitCounter >= moleMaxCounter)
        {
            mole_rigidbody.transform.position = new Vector3(mole_rigidbody.transform.position.x, Mathf.Lerp(mole_rigidbody.transform.position.y,
                                                            endPoint.position.y, Time.deltaTime * speed), mole_rigidbody.transform.position.z);
            //m_capsuleCollider.isTrigger = false;

            if (exitCounter >= moleMaxCounter + 2)
            {
                exitCounter = 0;
            }            
        }
        else
        {
            mole_rigidbody.transform.position = new Vector3(mole_rigidbody.transform.position.x, Mathf.Lerp(mole_rigidbody.transform.position.y,
                                                            initPoint.position.y, Time.deltaTime * speed), mole_rigidbody.transform.position.z);
        }

	}
}
