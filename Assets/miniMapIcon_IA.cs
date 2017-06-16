using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapIcon_IA : MonoBehaviour {

    public float height;
    private IA_Item[] m_IA;
    private Transform[] IA_position;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_IA = FindObjectsOfType<IA_Item>();
        IA_position = new Transform[m_IA.Length];

        if (m_IA != null)
        {
            for (int i = 0; i < m_IA.Length; i++)
            {                
                IA_position[i] = m_IA[i].gameObject.GetComponent<Transform>();
                transform.position = new Vector3(IA_position[i].position.x, IA_position[i].position.y + height, IA_position[i].position.z);
            }
        }
        else if (IA_position != null)
        {
            for (int i = 0; i < m_IA.Length; i++)
            {
                transform.position = new Vector3(IA_position[i].position.x, Mathf.Lerp(height, IA_position[i].position.y + height, Time.deltaTime), IA_position[i].position.z);
                transform.rotation = new Quaternion(0, IA_position[i].rotation.y, 0, 0);
            }           
        }
    }
}
