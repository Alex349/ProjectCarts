using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player" && col.GetComponent<m_carItem>().money < 10)
        {
            col.GetComponent<m_carItem>().money++;
            audioManager.audioInstance.CoinSound();
        }
        else if (col.tag == "IA" && col.GetComponent<IA_Item>().money < 10)
        {
            col.GetComponent<IA_Item>().money++;
            Debug.Log("Coin");
        }

        Destroy(this.gameObject);

    }
}
