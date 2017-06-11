using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptVolant : MonoBehaviour {

    public GameObject m_volantini;
    public float deltaRotation;
    private Quaternion m_rotationi;

	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        m_volantini.transform.rotation = m_rotationi;
        m_rotationi = new Quaternion(m_rotationi.x, m_rotationi.y, m_rotationi.z + Input.GetAxis("Horizontal") * deltaRotation, m_rotationi.w);

    }
}
