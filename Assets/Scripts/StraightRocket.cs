using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightRocket : MonoBehaviour
{

    [SerializeField]
    private float rocketSpeed = -30;
    [SerializeField]
    private float rocketBounces;
    // Use this for initialization
    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.forward * rocketSpeed, ForceMode.Impulse);


    }

    // Update is called once per frame
    void Update()
    {
        if (rocketBounces >= 5)
        {
            Destroy(this.gameObject);
        }
 
    }

    void OnCollisionEnter(Collision collision)
    {
        rocketBounces++;

    }



}
