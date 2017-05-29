using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoamingRocket : MonoBehaviour {

    [SerializeField]
    private float rocketSpeed = 1000;
    [SerializeField]
    private float rocketBounces, distanceAwayFromSurface;
    private Rigidbody bulletBody, carBody;

    Vector3 myTransform;

    public float distanceToGround;

    // Use this for initialization
    void Start()
    {
        bulletBody = GetComponent<Rigidbody>();
        bulletBody = GetComponent<Rigidbody>();

        bulletBody.AddForce(this.transform.forward * rocketSpeed);

        myTransform = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (rocketBounces >= 5)
        {
            Destroy(this.gameObject);
        }

        RaycastHit hit = new RaycastHit();

        Debug.DrawRay(transform.position, -Vector3.up, Color.red);

        if (Physics.Raycast(bulletBody.transform.position, -Vector3.up, out hit))
        {
            myTransform.x = transform.position.x;
            myTransform.z = transform.position.z;
            transform.position = hit.point + hit.normal * distanceAwayFromSurface;

        }

    }

    void OnCollisionEnter(Collision collision)
    {
        rocketBounces++;

    }
}
