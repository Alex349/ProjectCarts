using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMysteryBox_Script : MonoBehaviour {

    public float deltaRotation = 50, deltaUp = 0.004f;

	void Update () {
        transform.Rotate(new Vector3(0, 1, 0), deltaRotation * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.time) * deltaUp), transform.position.z);
    }
}
