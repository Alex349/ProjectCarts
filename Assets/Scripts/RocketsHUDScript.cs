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

    public void AddTarget()
    {

        GameObject cubeToInstantiate = GameObject.Find("RocketToFirst");
        GameObject newCube = GameObject.Instantiate(cubeToInstantiate);
        newCube.name = "RocketToFirstAdded";
        //position it at random place
        newCube.transform.localPosition = new Vector3(Random.Range(-50, 50), 1, Random.Range(-50, 50));
        //add the ArrowIndicatorStuff
        offScreenArrow.AddIndicator(newCube.transform, Random.Range(0, offScreenArrow.indicators.Length));
    }

    public void RemoveTarget()
    {
        GameObject cubeToRemove = GameObject.Find("RocketToFirstAdded");
        if (cubeToRemove)
        {
            offScreenArrow.RemoveIndicator(cubeToRemove.transform);
            GameObject.Destroy(cubeToRemove);
        }
    }

    void Update()
    {
        this.transform.position = player.transform.position;
        this.transform.rotation = player.transform.rotation;
    }

    void OnTriggerStay(Collider rocket)
    //void OnTriggerEnter(Collider rocket)
    {
        if (rocket.GetComponent<StraightRocket>() != null && rocket.GetComponent<HoamingRocket>() == null)
        {
            offScreenArrow.AddIndicator(rocket.transform, 2);
            straightIsInside = true;
        }

        if (rocket.GetComponent<StraightRocket>() != null && rocket.GetComponent<HoamingRocket>() != null)
        {
            hoamingIsInside = true;
            offScreenArrow.AddIndicator(rocket.transform, 1);
        }

        if (rocket.GetComponent<ToFirstRocket>() != null)
        {
            offScreenArrow.AddIndicator(rocket.transform, 3);
            KingBallIsInside = true;
        }

    }
    void OnTriggerExit(Collider rocket)
    {
        if (rocket.gameObject.tag == "Rocket")
        {
            offScreenArrow.RemoveIndicator(rocket.transform);
        }
    }
}
