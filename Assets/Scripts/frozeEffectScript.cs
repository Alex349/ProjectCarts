using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frozeEffectScript : MonoBehaviour {

    private GameObject m_particleSystem;
    private float delayParticles = 5;

	void Start ()
    {
       //m_particleSystem = transform.GetChild(0).gameObject;
       //m_particleSystem.SetActive(false);
	}
	
	
	void Update ()
    {
		//if (gameObject.activeInHierarchy)
        //{
        //    delayParticles -= Time.deltaTime;
        //
        //    if (delayParticles <= 0)
        //    {
        //        m_particleSystem.SetActive(true);
        //        Debug.Log("playing froze effect");
        //    }
        //}
	}
    void OnDestroy()
    {
        
    }
}
