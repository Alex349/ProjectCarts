using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target, LeftDriftTarget, RightDriftTarget;
    public float distance;
    public float height;
    public float heightDamping;

    public float lookAtHeight;

    public float rotationSnapTime;

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
    private Camera thisCamera;

    void Start()
    {
        m_kart = FindObjectOfType<m_carController>();
        lookAtVector = new Vector3(0, lookAtHeight, 0);
        target = GameObject.Find("CameraTarget").transform;
        LeftDriftTarget = GameObject.Find("CameraTargetL").transform;
        RightDriftTarget = GameObject.Find("CameraTargetR").transform;
        thisCamera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        wantedHeight = target.position.y + height;
        currentHeight = transform.position.y;

        wantedRotationAngle = target.eulerAngles.y;
        currentRotationAngle = transform.eulerAngles.y;

        currentRotationAngle = Mathf.SmoothDampAngle(currentRotationAngle, wantedRotationAngle, ref yVelocity, rotationSnapTime);

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        wantedPosition = target.position;
        wantedPosition.y = currentHeight;

        usedDistance = Mathf.SmoothDampAngle(usedDistance, distance + (m_kart.currentSpeed * distanceMultiplier), ref zVelocity, distanceSnapTime);

        wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0) * new Vector3(0, 0, -usedDistance);

        transform.position = wantedPosition;

        transform.LookAt(target.position + lookAtVector);

        if (m_kart.currentSpeed <= 5f)
        {
            currentHeight = wantedHeight;
        }
        else
        {
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        }

        if (m_kart.leftDrift)
        {            
            transform.LookAt(Vector3.Lerp(target.position + lookAtVector, LeftDriftTarget.position + lookAtVector, 0.05f));
        }
        else if (m_kart.rightDrift)
        {
            transform.LookAt(Vector3.Lerp(target.position + lookAtVector, RightDriftTarget.position + lookAtVector, 0.05f));
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
