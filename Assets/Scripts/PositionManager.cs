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
        racersGO.Reverse();
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

                if(t3 == 74 || t4 == 74)
                {
                    return 1;
                }
                else
                {
                    return t3.CompareTo(t4);

                }
            }
        }
    }

}
