using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollItemBehaviour : MonoBehaviour
{
    private m_carItem carItem;
    private float scrollSpeed = 0.9f;

    void Start ()
    {
        carItem = FindObjectOfType<m_carItem>();		
	}
	
	void Update ()
    {
		if (carItem.currentPlayerObject != "none")
        {

        }
	}
}
