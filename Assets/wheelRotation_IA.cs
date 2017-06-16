using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wheelRotation_IA : MonoBehaviour {

    public NavMeshAgent agent;

    void Start ()
    {

	}
	
	void Update ()
    {
        if (agent != null)
        {
            transform.Rotate(-agent.speed, 0, 0);
        }
    }
}
