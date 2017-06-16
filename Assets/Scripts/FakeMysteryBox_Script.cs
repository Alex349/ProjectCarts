using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMysteryBox_Script : MonoBehaviour {

    public float deltaRotation = 50;

    Vector3 myTransform;

    void Start()
    {
        myTransform = this.transform.position;

    }

        void Update () {

        transform.Rotate(new Vector3(0, 1, 0), deltaRotation * Time.deltaTime);

        RaycastHit hit = new RaycastHit();

        Debug.DrawRay(transform.position, -Vector3.up, Color.green);

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            transform.position = hit.point + hit.normal * 0.5f;
        }
    }
}
