using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Item : MonoBehaviour {

    public string currentIAItem = "none";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        //Is it the IA who enters the collider?
        if (other.tag == "MysteryBox")
        {
            Destroy(other.gameObject);

            if (currentIAItem == "none")
            {
                GetRandomItem();

            }
        }

    }

    void GetRandomItem()
    {
        float rnd = (Random.Range(0f, 1f));


        if (rnd < 0.45)
        {
            currentIAItem = "banana";
        }
        else if (rnd < 0.7)
        {
            currentIAItem = "turbo";
        }
        else if (rnd < 1)
        {
            currentIAItem = "rocket";
        }
        else
        {

        }
    }

}
