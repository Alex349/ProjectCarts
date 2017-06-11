using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateKartMenu : MonoBehaviour
{
    public float deltaRotation;

	void Start ()
    {
		
	}	
	
	void Update ()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.y + deltaRotation * Time.deltaTime, 0);
    }
}
