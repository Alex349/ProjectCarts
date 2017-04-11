using UnityEngine;
using System.Collections;

public class TireToWheel : MonoBehaviour {

	public WheelCollider wheelCollider;
    public ParticleSystem wheelParticleSystem;
    private float timeCounter;
    private Vector3 wheelPosition;

    void Start()
    {
        wheelPosition = this.transform.localPosition;
	}

	void FixedUpdate ()
    {
        UpdateWheelHeight(this.transform, wheelCollider);        
    }

	void UpdateWheelHeight(Transform wheelTransform, WheelCollider collider)
    {
        var emission = wheelParticleSystem.emission;
        Vector3 localPosition = wheelTransform.localPosition;
		
		WheelHit hit = new WheelHit();
		
		if (collider.GetGroundHit(out hit))
        {

			float hitY = collider.transform.InverseTransformPoint(hit.point).y;

            localPosition.y = hitY;

            emission.enabled = true;

			if(Mathf.Abs(hit.forwardSlip) >= wheelCollider.forwardFriction.extremumSlip || 
			   Mathf.Abs(hit.sidewaysSlip) >= wheelCollider.sidewaysFriction.extremumSlip)
            {
				emission.enabled = true;
			}
			else
            {
                emission.enabled = false;
			}
		}
        else
        {
            timeCounter += Time.deltaTime;		
			localPosition = Vector3.Lerp (localPosition, -Vector3.up * collider.suspensionDistance, .05f);

            emission.enabled = false;

            if (timeCounter >= 2)
            {
                localPosition.y = wheelPosition.y;
                timeCounter = 0;
            }
		}		
		wheelTransform.localPosition = localPosition;

		wheelTransform.localRotation = Quaternion.Euler(0, collider.steerAngle, 0);        
	}
}
