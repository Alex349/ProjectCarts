using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCarScript : MonoBehaviour {

    Rigidbody m_body;
    float m_deadZone = 0.1f;

    public float m_forwardAcl = 100f;
    public float m_backwardAcl = 20f;
    float m_currThrust = 0f;

    public float m_turnStrength = 10f;
    float m_currTurn = 0f;

    int m_layerMask;
    public float m_hoverForce = 10f;
    public float m_hoverHeight = 5f;
    public GameObject[] m_hoverPoints;

    // Use this for initialization
    void Start () {
        m_body = GetComponent<Rigidbody>();
        m_layerMask = 1 << LayerMask.NameToLayer("Characters");
        m_layerMask = ~m_layerMask;
	}
	
	// Update is called once per frame
	void Update () {
        //Thrust
        m_currThrust = 0f;
        float aclAxis = Input.GetAxis("Vertical");
        if (aclAxis > m_deadZone)
        {
            m_currThrust = aclAxis * m_forwardAcl;
        }
        else if (aclAxis < -m_deadZone)
        {
            m_currThrust = aclAxis * m_backwardAcl;
        }

        //Turn

        m_currTurn = 0f;
        float turnAxis = Input.GetAxis("Horizontal");
        if (Mathf.Abs(turnAxis) > m_deadZone)
        {
            m_currTurn = turnAxis;
        }

    }
    void FixedUpdate()
    {
        // Hover
        RaycastHit hit;
        for (int i=0; i<m_hoverPoints.Length; i++)
        {
            var hoverPoint = m_hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position,
                -Vector3.up, out hit,
                m_hoverHeight,
                m_layerMask))
                m_body.AddForceAtPosition(Vector3.up * m_hoverForce *
                    (1f - (hit.distance / m_hoverHeight)),
                    hoverPoint.transform.position);
            else
            {
                if (transform.position.y > hoverPoint.transform.position.y)
                    m_body.AddForceAtPosition(hoverPoint.transform.up * m_hoverForce,
                        hoverPoint.transform.position);
                else
                    m_body.AddForceAtPosition(
                        hoverPoint.transform.up * -m_hoverForce, 
                        hoverPoint.transform.position);
            }
        }
        //Forward
        if (Mathf.Abs(m_currThrust) > 0)
        {
            m_body.AddForce(transform.forward * m_currThrust);
        }

        //Turn
        if(m_currTurn>0)
        {
            m_body.AddRelativeTorque(Vector3.up * m_currTurn * m_turnStrength);
        }
        else if (m_currTurn<0)
        {
            m_body.AddRelativeTorque(Vector3.up * m_currTurn * m_turnStrength);
        }
    }
}
