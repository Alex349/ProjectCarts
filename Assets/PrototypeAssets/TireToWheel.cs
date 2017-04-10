using UnityEngine;
using System.Collections;

public class TireToWheel : MonoBehaviour {

	public WheelCollider wheelCollider;
    private ParticleSystem wheelParticleSystem;

	void Start()
    {
        wheelParticleSystem = wheelCollider.GetComponent<ParticleSystem>();
	}

	void FixedUpdate ()
    {
		UpdateWheelHeight(this.transform, wheelCollider);
	}


	void UpdateWheelHeight(Transform wheelTransform, WheelCollider collider)
    {
		
		Vector3 localPosition = wheelTransform.localPosition;
		
		WheelHit hit = new WheelHit();
		
		if (collider.GetGroundHit(out hit)) {

			float hitY = collider.transform.InverseTransformPoint(hit.point).y;

			localPosition.y = hitY;

			wheelParticleSystem.enableEmission = true;

			if(Mathf.Abs(hit.forwardSlip) >= wheelCollider.forwardFriction.extremumSlip || 
					Mathf.Abs(hit.sidewaysSlip) >= wheelCollider.sidewaysFriction.extremumSlip)
            {
				wheelParticleSystem.enableEmission = true;
			}
			else {
				wheelParticleSystem.enableEmission = false;
			}
		}
        else
        {			
			localPosition = Vector3.Lerp (localPosition, -Vector3.up * collider.suspensionDistance, .05f);
			wheelParticleSystem.enableEmission = false;
		}
		
		// actually update the position
		
		wheelTransform.localPosition = localPosition;

		wheelTransform.localRotation = Quaternion.Euler(collider.transform.rotation.x, collider.steerAngle, 0);
		
	}
}
