using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingBallMesh : MonoBehaviour {

    public float deltaRotation;

    void Update()
    {
        transform.Rotate(new Vector3(0, deltaRotation * Time.deltaTime, 0));
    }
}
