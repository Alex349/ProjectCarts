using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoamingRocketMesh : MonoBehaviour {

    public float deltaRotation;

    void Update () {
        transform.Rotate(new Vector3(deltaRotation * Time.deltaTime, 0, 0));
    }
}
