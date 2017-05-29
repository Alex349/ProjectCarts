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
        GameObject[] IA = GameObject.FindGameObjectsWithTag("Kart");

        foreach (GameObject racer in IA)
        {
            racersGO.Add(racer);
        }

        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject racer in Player)
        {
            racersGO.Add(racer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        racersGO.Sort((IComparer<GameObject>)new SortLap());
        //racersGO.Sort((IComparer<GameObject>)new SortDestPoint());
        racersGO.Reverse();
        //racersGO.Sort((IComparer<GameObject>)new SortDistToPoint());


    }

    private class SortLap : IComparer<GameObject>
    {
        int IComparer<GameObject>.Compare(GameObject _objAL, GameObject _objBL)
        {
            int t1 = _objAL.GetComponent<CarCheckPoints>().currentLap;
            int t2 = _objBL.GetComponent<CarCheckPoints>().currentLap;
            if (t1.CompareTo(t2) != 0)
            {
                return t1.CompareTo(t2);
            }
            else
            {
                int t3 = _objAL.GetComponent<CarCheckPoints>().currentCheckpoint;
                int t4 = _objBL.GetComponent<CarCheckPoints>().currentCheckpoint;

                return t3.CompareTo(t4);
            }
        }
    }

    private class SortCheckpoint: IComparer<GameObject>
    {
        int IComparer<GameObject>.Compare(GameObject _objAL, GameObject _objBL)
        {
            int t1 = _objAL.GetComponent<CarCheckPoints>().currentCheckpoint;
            int t2 = _objBL.GetComponent<CarCheckPoints>().currentCheckpoint;
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

}
