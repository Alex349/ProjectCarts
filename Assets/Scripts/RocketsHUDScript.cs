using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketsHUDScript : MonoBehaviour {

    public Image HoamingRocketImage, StraightRocketImage, KingBallImage;
    public bool hoamingIsInside, straightIsInside, KingBallIsInside;
    private GameObject player;
    // Use this for initialization
    void Start () {

        HoamingRocketImage = GameObject.Find("HoamingRocketHUD").GetComponent<Image>();
        StraightRocketImage = GameObject.Find("StraightRocketHUD").GetComponent<Image>();
        KingBallImage = GameObject.Find("KingBallHUD").GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = player.transform.position;
        this.transform.rotation = player.transform.rotation;

        if (hoamingIsInside == false)
        {
            HoamingRocketImage.enabled = false;
        }

        if (straightIsInside == false)
        {
            StraightRocketImage.enabled = false;
        }

        if (KingBallIsInside == false)
        {
            KingBallImage.enabled = false;
        }

    }


    void OnTriggerStay(Collider rocket)
    {
        if (rocket.GetComponent<StraightRocket>() != null && rocket.GetComponent<HoamingRocket>() == null)
        {
            StraightRocketImage.enabled = true;
            StraightRocketImage.enabled = true;
        }

        if (rocket.GetComponent<StraightRocket>() != null && rocket.GetComponent<HoamingRocket>() != null)
        {
            HoamingRocketImage.enabled = true;
            HoamingRocketImage.enabled = true;
        }
    
        if (rocket.GetComponent<ToFirstRocket>() != null)
        {
            KingBallImage.enabled = true;
            KingBallIsInside = true;
        }

    }
    void OnTriggerExit(Collider rocket)
    {
        if (rocket.GetComponent<StraightRocket>() != null)
        {
            StraightRocketImage.enabled = false;
        }
        if (rocket.GetComponent<StraightRocket>() != null && rocket.GetComponent<HoamingRocket>() != null)
        {
            HoamingRocketImage.enabled = false;
        }
        if (rocket.GetComponent<ToFirstRocket>() != null)
        {
            KingBallImage.enabled = false;
        }
    }
}
