using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRigidbodyC : MonoBehaviour {

    public float spring = 50.0f;
    public float damper = 5.0f;
    public float drag = 10.0f;
    public float angularDrag = 5.0f;
    public float distance = 0.2f;
    public float throwForce = 5.5f;
    public bool attachToCenterOfMass = false;
    public bool isPressed = false;
    GameObject player;
    SpringJoint springJoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Make sure the user pressed the mouse down
        if (!Input.GetButtonDown("Fire1"))
            return;
        if (Input.GetButtonDown("Fire1"))
        {
            isPressed = true;
            Debug.Log("isPressed = true;");
        }
        
        var mainCamera = Camera.main;

        // We need to actually hit an object
        RaycastHit hit;
        if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            return;
        // We need to hit a rigidbody that is not kinematic
        if (!hit.rigidbody || hit.rigidbody.isKinematic)
            return;

        if (!springJoint)
        {
            GameObject go = new GameObject("Rigidbody dragger");
            Rigidbody m_rigidbody = go.AddComponent<Rigidbody>() as Rigidbody;
            springJoint = go.AddComponent<SpringJoint>();
            m_rigidbody.isKinematic = true;
        }

        springJoint.transform.position = hit.point;
        if (attachToCenterOfMass)
        {
            var anchor = transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
            anchor = springJoint.transform.InverseTransformPoint(anchor);
            springJoint.anchor = anchor;
        }
        else
        {
            springJoint.anchor = Vector3.zero;
        }

        springJoint.spring = spring;
        springJoint.damper = damper;
        springJoint.maxDistance = distance;
        springJoint.connectedBody = hit.rigidbody;

        StartCoroutine("DragObject", hit.distance);
    }

    IEnumerator DragObject(float distance)
    {
        var oldDrag = springJoint.connectedBody.drag;
        var oldAngularDrag = springJoint.connectedBody.angularDrag;
        springJoint.connectedBody.drag = drag;
        springJoint.connectedBody.angularDrag = angularDrag;
        var mainCamera = Camera.main;

        while (isPressed)
        {

            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            springJoint.transform.position = ray.GetPoint(distance);
            
            while (Input.GetButtonDown("Fire2"))
            {
                springJoint.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * throwForce);
                yield return new WaitForSeconds(0.01f);
                isPressed = false;
                Debug.Log("Thrown? DragRigidbody.js");
            }
        }


        if (springJoint.connectedBody)
        {
            springJoint.connectedBody.drag = oldDrag;
            springJoint.connectedBody.angularDrag = oldAngularDrag;
            springJoint.connectedBody = null;
        }
    }

    Camera FindCamera(Camera camera)
    {
        if (camera)
            return camera;
        else
            return Camera.main;
    }
}

