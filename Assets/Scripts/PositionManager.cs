using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{

    public List<GameObject> racersGO;

    // Use this for initialization
    void Start()
    {
        racersGO = new List<GameObject>();

        AddAllRacers();

    }

    void AddAllRacers()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Kart");

        foreach (GameObject racer in go)
        {
            racersGO.Add(racer);
        }
    }

    // Update is called once per frame
    void Update()
    {
       // racersGO.Sort((IComparer<GameObject>)new SortLap());
        racersGO.Sort((IComparer<GameObject>)new SortDestPoint());
        racersGO.Reverse();
        //racersGO.Sort((IComparer<GameObject>)new SortDistToPoint());


    }

    private class SortLap : IComparer<GameObject>
    {
        int IComparer<GameObject>.Compare(GameObject _objAL, GameObject _objBL)
        {
            int t1 = _objAL.GetComponent<CarCheckPoints>().currentLap;
            int t2 = _objBL.GetComponent<CarCheckPoints>().currentLap;
            return t1.CompareTo(t2);
        }
    }

    private class SortDestPoint : IComparer<GameObject>
    {

        int IComparer<GameObject>.Compare(GameObject _objA, GameObject _objB)
        {
            int t1 = _objA.GetComponent<NavMeshAI>().destPoint;
            int t2 = _objB.GetComponent<NavMeshAI>().destPoint;
            return t1.CompareTo(t2);
        }
    }

    private class SortDistToPoint : IComparer<GameObject>
    {

        int IComparer<GameObject>.Compare(GameObject _objAD, GameObject _objBD)
        {
            float t1 = _objAD.GetComponent<NavMeshAI>().distanceToNextPoint;
            float t2 = _objBD.GetComponent<NavMeshAI>().distanceToNextPoint;
            return t1.CompareTo(t2);
        }
    }

}
