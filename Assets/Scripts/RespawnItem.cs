using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour {

    public GameObject m_mysteryBox;
    private GameObject m_instanceBox;
    public float itemCounter = 10;
    public float deltaRotation = 5;

	void Start ()
    {
        m_instanceBox = Instantiate(m_mysteryBox, new Vector3(transform.localPosition.x, 
                        transform.localPosition.y + 0.7f, transform.localPosition.z), this.transform.rotation, this.transform);
	}
	
	void Update ()
    {
        if (m_instanceBox != null)
        {
            m_instanceBox.transform.Rotate(new Vector3(0, 1, 0), deltaRotation * Time.deltaTime);
            m_instanceBox.transform.position = new Vector3(transform.localPosition.x, (transform.localPosition.y + 1) + Mathf.Sin(deltaRotation * Time.deltaTime), transform.localPosition.z);
        }
        

		if (m_instanceBox == null)
        {
            itemCounter -= Time.deltaTime;

            if (itemCounter <= 0)
            {
                m_instanceBox = Instantiate(m_mysteryBox, new Vector3(transform.localPosition.x, 
                                transform.localPosition.y + 0.7f, transform.localPosition.z), this.transform.rotation, this.transform);
                itemCounter = 10;
            }
        }
	}
}
