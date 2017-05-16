using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightRocket : MonoBehaviour
{

    [SerializeField]
    private float rocketSpeed = -30;
    [SerializeField]
    private float rocketBounces;
    private Rigidbody bulletBody;

    public float hoverHeight = 2f, hoverForce = 2f, distanceToGround;
    public float minDistance,maxDistance;

    // Use this for initialization
    void Start()
    {
        bulletBody = GetComponent<Rigidbody>();

        this.GetComponent<Rigidbody>().AddForce(transform.forward * rocketSpeed, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        if (rocketBounces >= 5)
        {
            Destroy(this.gameObject);
        }

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            distanceToGround = hit.distance;
        }

        if (distanceToGround < minDistance)
        {
            //float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            //Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            bulletBody.AddForce(Vector3.up * 500, ForceMode.Acceleration);
            //Debug.Log("Im too low");
        }
        if (distanceToGround > maxDistance)
        {
            //float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            //Vector3 appliedHoverForce = Vector3.up * -1 * proportionalHeight * hoverForce;
            bulletBody.AddForce(Vector3.down * 500, ForceMode.Acceleration);
           // Debug.Log("Im too high");
        }
    }

    //void FixedUpdate()
    //{
    //    Ray ray = new Ray(transform.position, -transform.up);
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit, hoverHeight))
    //    {
    //        Debug.Log(hit.distance);

    //        float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
    //        Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
    //        bulletBody.AddForce(appliedHoverForce, ForceMode.Force);
    //    }
    //}

    void OnCollisionEnter(Collision collision)
    {
        rocketBounces++;

    }



}
