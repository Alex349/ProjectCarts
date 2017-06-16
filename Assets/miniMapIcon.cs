using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapIcon : MonoBehaviour
{

    public float height;
    private Transform m_position;
    private m_carController m_kart;


    void Start()
    {

    }

    void Update()
    {
        m_kart = FindObjectOfType<m_carController>();        

        if (m_position == null && m_kart != null)
        {
            m_position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            transform.position = new Vector3(m_position.position.x, m_position.position.y + height, m_position.position.z);
        }
        else if (m_position != null)
        {
            transform.position = new Vector3(m_position.position.x, Mathf.Lerp(height, m_position.position.y + height, Time.deltaTime), m_position.position.z);
            transform.rotation = new Quaternion(0, m_position.rotation.y, 0, 0);
        }       
    }
}

