using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarEngine : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    public float maxMotorTorque = 80f;
    public float maxBrakeTorque = 150f;
    public float wheelTorque = 150f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;
    public bool isBraking = false;

    //[Header("Sensors")]
    //public float sensorLength = 3f;
    //public Vector3 frontSensorPosition = new Vector3(0f, 0.2f, 0.5f);
    //public float frontSideSensorPosition = 0.2f;
    //public float frontSensorAngle = 30f;

    private List<Transform> nodes;
    private int currectNode = 0;
    
    private void Start () {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;

        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++) {
            if (pathTransforms[i] != path.transform) {
                nodes.Add(pathTransforms[i]);
            }
        }
    }
	
	private void FixedUpdate () {
       // Sensors();
        ApplySteer();
        Drive();
        CheckWaypointDistance();
        Braking();
	}

    //private void Sensors() {
    //    RaycastHit hit;
    //    Vector3 sensorStartPos = transform.position + frontSensorPosition;

    //    //front center sensor
    //    if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength)) {
    //        Debug.DrawLine(sensorStartPos, hit.point);
    //    }
        
    //    //front right sensor
    //    sensorStartPos.x += frontSideSensorPosition;
    //    if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength)) {
    //        Debug.DrawLine(sensorStartPos, hit.point);
    //    }
        
    //    //front right angle sensor
    //    if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength)) {
    //        Debug.DrawLine(sensorStartPos, hit.point);
    //    }
        
    //    //front left sensor
    //    sensorStartPos.x -= 2 * frontSideSensorPosition;
    //    if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength)) {
    //        Debug.DrawLine(sensorStartPos, hit.point);
    //    }
        
    //    //front left angle sensor
    //    if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength)) {
    //        Debug.DrawLine(sensorStartPos, hit.point);
    //    }
        
    //}

    private void ApplySteer() {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive() {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if (currentSpeed < maxSpeed && !isBraking) {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
            wheelRL.motorTorque = maxMotorTorque;
            wheelRR.motorTorque = maxMotorTorque;
            Debug.Log("brake1");
        } else {
            wheelFL.motorTorque = wheelTorque;
            wheelFR.motorTorque = wheelTorque;
            wheelRL.motorTorque = wheelTorque;
            wheelRR.motorTorque = wheelTorque;
        }
    }

    private void CheckWaypointDistance() {
        if(Vector3.Distance(transform.position, nodes[currectNode].position) < 5f) {
            if(currectNode == nodes.Count - 1) {
                currectNode = 0;
            } else {
                currectNode++;
            }
        }
    }

    private void Braking() {
        if (isBraking) {
            wheelFL.brakeTorque = maxBrakeTorque;
            wheelFR.brakeTorque = maxBrakeTorque;
            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
        } else {
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;

        }
    }
}
