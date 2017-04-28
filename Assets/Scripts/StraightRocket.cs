using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightRocket : MonoBehaviour {

    [SerializeField]
    private float rocketSpeed = -30;
    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update ()
    {
        this.GetComponent<Rigidbody>().AddForce(0, 0, rocketSpeed,ForceMode.Force);
    }
}
