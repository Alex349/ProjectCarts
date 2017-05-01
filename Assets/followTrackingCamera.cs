using UnityEngine;

public class followTrackingCamera : MonoBehaviour
{
    public Transform target;
    public Transform car;

    public float height = 20f;
    public float distance = 20f;

    public float rotateSpeed = 1f;

    public bool doRotate;

    private float heightWanted;
    private float distanceWanted;

    private Vector3 zoomResult;
    private Quaternion rotationResult;
    private Vector3 targetAdjustedPosition;

    void Start()
    {
        heightWanted = height;
        distanceWanted = distance;
    }

    void FixedUpdate()
    {
        if (!target)
        {
            Debug.LogError("This camera has no target, you need to assign a target in the inspector.");
            return;
        }
        if (doRotate)
        {
            Vector3 currentRotationAngle = transform.eulerAngles;
            Vector3 wantedRotationAngle = target.eulerAngles;

            currentRotationAngle = Vector3.Lerp(currentRotationAngle, wantedRotationAngle, rotateSpeed * Time.deltaTime);

            rotationResult = Quaternion.Euler(currentRotationAngle.x, currentRotationAngle.y, currentRotationAngle.z);
        }

        targetAdjustedPosition = rotationResult * zoomResult;
        transform.position = target.position + targetAdjustedPosition;

        transform.LookAt(car);
    }
}
