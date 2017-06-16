using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    private float delay = 2;
    private GameObject m_kart;

    void Start()
    {
        
    }
    void Update()
    {
        m_kart = GameObject.FindGameObjectWithTag("Player");

        //if (m_kart != null)
        //{           
        //    if (m_kart.GetComponent<m_carItem>().ItemSystems[1].isPlaying && m_kart != null)
        //    {
        //        delay -= Time.deltaTime;
        //
        //        if (delay <= 0)
        //        {
        //            m_kart.transform.GetChild(7).GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
        //        }
        //    }
        //}

        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && col.GetComponent<m_carItem>().money < 10)
        {
            col.GetComponent<m_carItem>().money++;
            col.transform.GetChild(7).GetChild(1).gameObject.SetActive(true);
            col.transform.GetChild(7).GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            //col.GetComponent<m_carItem>().ItemSystems[1].Play();

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
