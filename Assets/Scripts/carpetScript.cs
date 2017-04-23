using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class carpetScript : MonoBehaviour {

    private Rigidbody m_rigidbody;
    float m_deadZone = 0.1f;

    public float m_forwardAcl = 100f;
    public float m_backwardAcl = 25f;

    float m_currentThrust = 0f;

    public float m_turnStrength = 10f;
    private float m_currentTurn = 0f;

    private int m_layerMask;
    public float m_hoverForce = 9f;
    public float m_hoverHeigh = 2;

    public GameObject[] m_hoverPoints;
    //public GameObject[] m_suspensionPoints;
    public Transform[] m_suspensionPoints;
    private float diferencialHeigh = 0.5f;
    private Vector3 currentPos;
    private GameObject hoverPoint;

    void Start ()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        currentPos = transform.position;
        m_layerMask = 1 << LayerMask.NameToLayer("Characters");
        m_layerMask = ~m_layerMask;
     //   m_suspensionPoints[1].localPosition = new Vector3(-0.4f, 0, 0.4f);
     //   m_suspensionPoints[2].localPosition = new Vector3(0.4f, 0, 0.4f);
     //   m_suspensionPoints[3].localPosition = new Vector3(0.5f, 0, -0.4f);
     //   m_suspensionPoints[4].localPosition = new Vector3(0.4f, 0, 0.5f);
    }
	
	void Update ()
    {
        m_currentThrust = 0f;
        float acelerationAxis = Input.GetAxis("Vertical");
        if (acelerationAxis > m_deadZone)
        {
            m_currentThrust = acelerationAxis * m_forwardAcl;
        }
        else if (acelerationAxis < -m_deadZone)
        {
            m_currentThrust = acelerationAxis * m_backwardAcl;
        }

        m_turnStrength = 0f;
        float turnAxis = Input.GetAxis("Horizontal");
        if (Mathf.Abs(turnAxis) > m_deadZone)
        {
            m_currentTurn = turnAxis;
        }
	}
    void FixedUpdate()
    {
        RaycastHit hit;
        //Vector3 SuspensionPointPos;
        
        for (int i = 0; i < m_hoverPoints.Length; i++)
        {
            hoverPoint = m_hoverPoints[i];

            for (int r = 0; i < m_suspensionPoints.Length; i++)
            {
                Transform suspensionPoint = m_suspensionPoints[r];

                if (Physics.Raycast(hoverPoint.transform.position, Vector3.down, out hit, m_hoverHeigh, m_layerMask))
                {
                //    SuspensionPointPos = suspensionPoint.transform.position;
                //    SuspensionPointPos = new Vector3(suspensionPoint.transform.position.x, suspensionPoint.transform.position.y, suspensionPoint.transform.position.z);
                //    SuspensionPointPos.y =  m_hoverForce * (1 - (hit.distance / m_hoverHeigh));
                //    currentPos = suspensionPoint.transform.position;
                    m_rigidbody.AddForceAtPosition(Vector3.up * m_hoverForce * (1 - (hit.distance / m_hoverHeigh)), hoverPoint.transform.position);
                }
                else
                {
                    if (transform.position.y > hoverPoint.transform.position.y)
                    {
                      //  SuspensionPointPos = suspensionPoint.transform.position;
                      //  SuspensionPointPos.y += diferencialHeigh;
                        m_rigidbody.AddForceAtPosition(hoverPoint.transform.up * m_hoverForce, hoverPoint.transform.position);
                    }
                    else
                    {
                    //    SuspensionPointPos = suspensionPoint.transform.position;
                    //    SuspensionPointPos.y -= diferencialHeigh;
                        m_rigidbody.AddForceAtPosition(hoverPoint.transform.up * -m_hoverForce, hoverPoint.transform.position);
                    }
                }
            }
            
        }
        if (Mathf.Abs(m_currentThrust) > 0)
        {
            m_rigidbody.AddForce(transform.forward * m_currentThrust);
            //currentPos = new Vector3(transform.position.x, transform.position.y, Mathf.Abs(transform.position.z) * m_currentThrust) ;
        }

        if (m_currentTurn > 0)
        {
            m_rigidbody.AddRelativeForce(Vector3.up * m_currentTurn * m_turnStrength);
            m_rigidbody.AddForceAtPosition(new Vector3 (Mathf.Abs(transform.forward.x), 0, 0) * m_currentTurn, hoverPoint.transform.position);
            //currentPos = new Vector3(Mathf.Abs(transform.forward.x * m_currentTurn), transform.position.y, transform.position.z) ;
        }
        else if (m_currentTurn < 0)
        {
            m_rigidbody.AddForceAtPosition(new Vector3(-Mathf.Abs(transform.forward.x), 0, 0) * m_currentTurn, hoverPoint.transform.position);
            m_rigidbody.AddRelativeForce(Vector3.up * m_currentTurn * m_turnStrength);
            //currentPos = new Vector3(-Mathf.Abs(transform.forward.x * m_currentTurn), transform.position.y, transform.position.z);
        }
    }
}
