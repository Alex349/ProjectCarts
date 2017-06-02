using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wagonSpaner_Copy : MonoBehaviour
{
    private wagonScript m_barrel;
    public float currentTime = 0f;
    public float spawningTime = 5f;

    private GameObject instanceWagon;
    public Transform startPoint, endPoint;
    public float deltaUpdate = 5f;

    void Start()
    {
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > spawningTime)
        {
            Instantiate(Resources.Load("Hazards/Barrel_Def"), startPoint.position, new Quaternion(0, 0, 0, 0));
            currentTime = 0;
        }        
    }
}

