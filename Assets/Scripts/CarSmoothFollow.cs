using UnityEngine;
using System.Collections;

public class CarSmoothFollow : MonoBehaviour
{

    public Transform target;
    public float distance = 20.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;

    public float lookAtHeight = 0.0f;

    public Rigidbody parentRigidbody;

    public float rotationSnapTime = 0.3F;

    public float distanceSnapTime;
    public float distanceMultiplier;

    private Vector3 lookAtVector;

    private float usedDistance;

    float wantedRotationAngle;
    float wantedHeight;

    float currentRotationAngle;
    float currentHeight;

    Quaternion currentRotation;
    Vector3 wantedPosition;

    private float yVelocity = 0.0F;
    private float zVelocity = 0.0F;

    private m_carController m_kart;

    void Start()
    {
        lookAtVector = new Vector3(0, lookAtHeight, 0);
        m_kart = FindObjectOfType<m_carController>();
    }

    void FixedUpdate()
    {
        wantedHeight = target.position.y + height;
        currentHeight = transform.position.y;

        wantedRotationAngle = target.eulerAngles.y;
        currentRotationAngle = transform.eulerAngles.y;

        currentRotationAngle = Mathf.SmoothDampAngle(currentRotationAngle, wantedRotationAngle, ref yVelocity, rotationSnapTime);

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        wantedPosition = target.position;
        wantedPosition.y = currentHeight;

        usedDistance = Mathf.SmoothDampAngle(usedDistance, distance + (parentRigidbody.velocity.magnitude * distanceMultiplier), ref zVelocity, distanceSnapTime);

        wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0) * new Vector3(0, 0, -usedDistance);

        transform.position = wantedPosition;

        transform.LookAt(target.position + lookAtVector);

        if (m_kart.Drifting == true)
        {
            rotationSnapTime = 0.25f;
        }
        else
        {
            rotationSnapTime = 0.125f;
        }
        if (Input.GetKeyDown("z"))
        {
            distance = -distance;
            //wantedPosition = new Vector3(-target.position.x, currentHeight, -target.position.z);
            //lookAtVector = -lookAtVector;
            //wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0) * new Vector3(0, 0, usedDistance);
            //transform.LookAt(new Vector3 (-target.position.x, currentHeight, -target.position.z) + lookAtVector);
        }
        else if (Input.GetKeyUp("z"))
        {
            transform.position = wantedPosition;
        }        
    }

}