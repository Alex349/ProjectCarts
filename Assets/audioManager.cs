using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager audioInstance;
    private AudioSource[] m_audios;
    private m_carController m_kart;
    //private AudioSource motorStopped, motorAcceleration, motor

    void Awake()
    {
        if (audioInstance == null)
        {
            audioInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
	void Start ()
    {
        m_audios = GetComponents<AudioSource>();
        m_kart = FindObjectOfType<m_carController>();
	}
	
	void Update ()
    {
		
	}
    public void MotorStopped()
    {
        if (!m_audios[0].isPlaying)
        {
            m_audios[0].Play();
        }
    }
    public void MotorAcceleration()
    {
        if (!m_audios[1].isPlaying)
        {
            m_audios[1].Play();
        }
    }
    public void MotorFront()
    {     
        if (!m_audios[2].isPlaying)
        {
            m_audios[2].Play();
        }
    }
    public void MotorRear()
    {
        if (!m_audios[3].isPlaying)
        {
            m_audios[3].Play();
        }
    }
    public void MotorDrift()
    {
        if (!m_audios[4].isPlaying)
        {
            m_audios[4].Play();
            m_audios[2].Play();
        }      
    }
    public void Turbo()
    {
        if (!m_audios[5].isPlaying)
        {
            m_audios[5].Play();
            Debug.Log("playing turbo sound");
        }
        else
        {
            m_audios[5].Play();
        }
    }
    public void Stop()
    {
        for (int i = 0; i < m_audios.Length; i++)
        {
            m_audios[i].Stop();
        }
    }
}
