using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightRocket : MonoBehaviour
{
    [SerializeField]
    private float rocketSpeed = 1000;
    [SerializeField]
    private float rocketBounces, distanceAwayFromSurface, selfDestruct = 10;
    private Rigidbody bulletBody,carBody;

    Vector3 myTransform;

    // Use this for initialization
    void Start()
    {
        bulletBody = GetComponent<Rigidbody>();

        bulletBody.AddForce(this.transform.forward * rocketSpeed) ;

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

        Debug.DrawRay(transform.position, -Vector3.up, Color.green);

        if (Physics.Raycast(bulletBody.transform.position, -Vector3.up, out hit))
        {
            myTransform.x = transform.position.x;
            myTransform.z = transform.position.z;
            transform.position = hit.point + hit.normal * distanceAwayFromSurface;

        }

        selfDestruct -= Time.deltaTime;

        if (selfDestruct < 0)
        {
            Destroy(this.gameObject);
        }
        if (selfDestruct < 9.7)
        {
            Component[] shpheres;
            shpheres = GetComponents(typeof(SphereCollider));
            foreach (SphereCollider a in shpheres)
                a.enabled = true;
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "Kart")
        {
            Destroy(this.gameObject);
        }
        if (col.tag == "Banana" || col.tag == "FakeMysteryBox")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        rocketBounces++;

    }



}
