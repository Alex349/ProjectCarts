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
        if (gameObject.CompareTag("BackWheel"))
        {
            wheelTransform.localPosition = new Vector3(localPosition.x, 0.1f, localPosition.z);
        }
        else if (gameObject.CompareTag("FrontWheel"))
        {
            wheelTransform.localPosition = new Vector3(localPosition.x, localPosition.y, localPosition.z);
        }

        if (collider.rpm < 1500)
        {
            wheelTransform.localRotation = Quaternion.Euler(collider.rpm, collider.steerAngle, transform.rotation.z);
        }
        else if (collider.rpm >= 1500)
        {
            wheelTransform.localRotation = Quaternion.Euler(1600, collider.steerAngle, transform.rotation.z);
        }
    }
}
