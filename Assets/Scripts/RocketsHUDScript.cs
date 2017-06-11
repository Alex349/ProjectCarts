using Greyman;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketsHUDScript : MonoBehaviour
{

    public Sprite king, tracker, straight;
    public bool hoamingIsInside, straightIsInside, KingBallIsInside;
    private GameObject player;
    public GameObject offScreenLogicGO;
    private OffScreenIndicator offScreenArrow;
    private OffScreenIndicatorManagerCanvas offScreenIndicatorManagerCanvas;

    // Use this for initialization
    void Start()
    {

        offScreenArrow = offScreenLogicGO.GetComponent<OffScreenIndicator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
        this.transform.rotation = player.transform.rotation;

        if (hoamingIsInside == false)
        {

            //HoamingRocketImage.enabled = false;
        }

        if (straightIsInside == false)
        {
            // StraightRocketImage.enabled = false;
        }

        if (KingBallIsInside == false)
        {
            // KingBallImage.enabled = false;
        }

    }


    //void OnTriggerStay(Collider rocket)
    void OnTriggerEnter(Collider rocket)
    {
        if (rocket.GetComponent<StraightRocket>() != null && rocket.GetComponent<HoamingRocket>() == null)
        {
            straightIsInside = true;
        }

        if (rocket.GetComponent<StraightRocket>() != null && rocket.GetComponent<HoamingRocket>() != null)
        {
            hoamingIsInside = true;
        }

        if (rocket.GetComponent<ToFirstRocket>() != null)
        {
            Debug.Log("Detected");
            AddTarget(rocket.transform);
            KingBallIsInside = true;
        }

    }
    void OnTriggerExit(Collider rocket)
    {
        if (rocket.GetComponent<StraightRocket>() != null)
        {
        }
        if (rocket.GetComponent<StraightRocket>() != null && rocket.GetComponent<HoamingRocket>() != null)
        {
        }
        if (rocket.GetComponent<ToFirstRocket>() != null)
        {
        }
    }
    public void AddTarget(Transform rocketTrans)
    {
        Debug.Log("Added");

        //add the ArrowIndicatorStuff
        offScreenArrow.AddIndicator(rocketTrans, Random.Range(0, offScreenArrow.indicators.Length));
    }
    public void RemoveTarget()
    {
        GameObject cubeToRemove = GameObject.Find("AddedCube");
        if (cubeToRemove)
        {
            offScreenArrow.RemoveIndicator(cubeToRemove.transform);
            GameObject.Destroy(cubeToRemove);
        }
    }
}
