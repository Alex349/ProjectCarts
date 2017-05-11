using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearScript : MonoBehaviour {

    public GameObject spear1, spear2, spear3, spear4;
    public float displacement = 3f;
    public float displacementTime = 10f;

	void Start ()
    {
		
	}

	void Update ()
    {
        displacementTime -= Time.deltaTime;

        if (displacementTime > 9f && displacementTime < 10f)
        {
            spear1.transform.position = Vector3.MoveTowards(spear1.transform.position, new Vector3(spear1.transform.position.x + displacement,
                                    spear1.transform.position.y, spear1.transform.position.z), displacement);
        }
        else if (displacementTime > 8f && displacementTime < 9f)
        {
            spear2.transform.position = Vector3.MoveTowards(spear2.transform.position, new Vector3(spear2.transform.position.x + displacement,
                                    spear2.transform.position.y, spear2.transform.position.z), displacement);
        }
        else if (displacementTime > 7f && displacementTime < 8f)
        {
            spear3.transform.position = Vector3.MoveTowards(spear3.transform.position, new Vector3(spear3.transform.position.x - displacement,
                                    spear3.transform.position.y, spear3.transform.position.z), displacement);
        }
        else if (displacementTime > 6f && displacementTime < 7f)
        {
            spear4.transform.position = Vector3.MoveTowards(spear4.transform.position, new Vector3(spear4.transform.position.x - displacement,
                                    spear4.transform.position.y, spear4.transform.position.z), displacement);
        }

        else if (displacementTime > 5f && displacementTime < 6f)
        {
            spear1.transform.position = Vector3.MoveTowards(spear1.transform.position, new Vector3(spear1.transform.position.x - displacement,
                                    spear1.transform.position.y, spear1.transform.position.z), displacement);
        }
        else if (displacementTime > 4f && displacementTime < 5f)
        {
            spear2.transform.position = Vector3.MoveTowards(spear2.transform.position, new Vector3(spear2.transform.position.x - displacement,
                                    spear2.transform.position.y, spear2.transform.position.z), displacement);
        }
        else if (displacementTime > 3f && displacementTime < 4f)
        {
            spear3.transform.position = Vector3.MoveTowards(spear3.transform.position, new Vector3(spear3.transform.position.x + displacement,
                                    spear3.transform.position.y, spear3.transform.position.z), displacement);
        }
        else if (displacementTime > 2f && displacementTime < 3f)
        {
            spear4.transform.position = Vector3.MoveTowards(spear4.transform.position, new Vector3(spear4.transform.position.x + displacement,
                                    spear4.transform.position.y, spear4.transform.position.z), displacement);            
        }
        if (displacementTime <= 0)
        {
            displacementTime = 10f;
        }
    }
}
