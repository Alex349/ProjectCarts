using UnityEngine;
using System.Collections;

public class TireToWheel : MonoBehaviour {

	public WheelCollider wheelCollider;
    //private ParticleSystem wheelParticleSystem;

    void Start()
    {
        if (GetComponentInParent<ParticleSystem>()!= null)
        {
            //wheelParticleSystem = GetComponentInParent<ParticleSystem>();
        }
	}

	void FixedUpdate ()
    {
        UpdateWheelHeight(this.transform, wheelCollider);        
    }

	void UpdateWheelHeight(Transform wheelTransform, WheelCollider collider)
    {
       // var emission = wheelParticleSystem.emission;
        Vector3 localPosition = wheelTransform.localPosition;
		
		WheelHit hit = new WheelHit();
		
		if (collider.GetGroundHit(out hit))
        {
			float hitY = collider.transform.InverseTransformPoint(hit.point).y;

            localPosition.y = hitY;

          // wheelParticleSystem.Play();
          // emission.enabled = true;

			if(Mathf.Abs(hit.forwardSlip) >= wheelCollider.forwardFriction.extremumSlip || 
			   Mathf.Abs(hit.sidewaysSlip) >= wheelCollider.sidewaysFriction.extremumSlip)
            {
             //   wheelParticleSystem.Play();
             //   emission.enabled = true;
			}
			else
            {
             //  wheelParticleSystem.Stop();
             //  emission.enabled = false;
			}
		}
        else
        {           	
			localPosition = Vector3.Lerp (localPosition, -Vector3.up * collider.suspensionDistance, .05f);
         //  wheelParticleSystem.Stop();
         //  emission.enabled = false;
		}		
		wheelTransform.localPosition = localPosition;

        //wheelTransform.localRotation = Quaternion.Euler(collider.rpm, collider.steerAngle, 0);
        //this is the good one, we can add the correct rotation when the centers (assets) changed
        wheelTransform.localRotation = Quaternion.Euler(0, collider.steerAngle, 0);
    }
}
