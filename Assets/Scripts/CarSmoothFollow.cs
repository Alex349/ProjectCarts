using UnityEngine;
using System.Collections;

public class CarSmoothFollow : MonoBehaviour
{

    public Transform target, LeftDriftTarget, RightDriftTarget;
    public float distance = 20.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;

    public float lookAtHeight = 0.0f;

    public Transform parentObject;

    public float rotationSnapTime = 0.3F;

    public float distanceSnapTime;
    public float distanceMultiplier;

    private Vector3 lookAtVector;
    private Vector3 distanceToKart;

    private float usedDistance;

    float wantedRotationAngle;
    float wantedHeight;

    float currentRotationAngle;
    float currentHeight;

    Quaternion currentRotation;
    Vector3 wantedPosition;

    private float yVelocity = 0.0F;
    private float zVelocity = 0.0F;

    private Camera thisCamera;
    private m_carController m_kart;

    void Start()
    {
        lookAtVector = new Vector3(0, lookAtHeight, 0);

        m_kart = FindObjectOfType<m_carController>();
                
        thisCamera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        distanceToKart = parentObject.transform.position - transform.position;

        rotationSnapTime = 0.125f;
        wantedHeight = target.position.y + height;
        currentHeight = transform.position.y;

        wantedRotationAngle = target.eulerAngles.y;
        currentRotationAngle = transform.eulerAngles.y;
        
        currentRotationAngle = Mathf.SmoothDampAngle(currentRotationAngle, wantedRotationAngle, ref yVelocity, rotationSnapTime);

        if (m_kart.currentSpeed <= 0.1f)
        {
            currentHeight = wantedHeight;
        }
        else
        {
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        }
        

        wantedPosition = target.position;
        wantedPosition.y = currentHeight;

        usedDistance = Mathf.SmoothDampAngle(usedDistance, distance + (distanceToKart.magnitude * distanceMultiplier), ref zVelocity, distanceSnapTime);

        wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0) * new Vector3(0, 0, -usedDistance);

        transform.position = wantedPosition;

        transform.LookAt(target.position + lookAtVector);

        if (m_kart.GetComponent<m_carController>().leftDrift)
        {
            rotationSnapTime = 0.5f;            
            transform.LookAt(Vector3.Lerp(target.position + lookAtVector, LeftDriftTarget.position + lookAtVector, 0.05f));
            
        }
        else if (m_kart.rightDrift)
        {
            rotationSnapTime = 0.5f;
            transform.LookAt(Vector3.Lerp(target.position + lookAtVector, RightDriftTarget.position + lookAtVector, 0.05f));
            //transform.LookAt(RightDriftTarget.position + lookAtVector);
        }
        if (m_kart.currentSpeed > m_kart.frontMaxSpeed - 1)
        {
            thisCamera.fieldOfView = Mathf.Lerp(thisCamera.fieldOfView, 55, 1f * Time.deltaTime);
        }
        else if (m_kart.currentSpeed >= 20)
        {
            thisCamera.fieldOfView = Mathf.Lerp(thisCamera.fieldOfView, 70, 1f * Time.deltaTime);
        }
        else if (thisCamera.fieldOfView > 45 && m_kart.currentSpeed < m_kart.frontMaxSpeed - 1)
        {
            thisCamera.fieldOfView = Mathf.Lerp(thisCamera.fieldOfView, 45, 1f * Time.deltaTime);
        }
                 
    }

}