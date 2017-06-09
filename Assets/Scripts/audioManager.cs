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
        m_audios = GetComponentsInChildren<AudioSource>();
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
    public void MotorFront(float pitch)
    {     
        if (!m_audios[2].isPlaying)
        {
            m_audios[2].Play();
            m_audios[2].pitch = pitch;
            Debug.Log(pitch);
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
    }
    public void Music1stLapIntro()
    {
        if (!m_audios[6].isPlaying)
        {
            m_audios[6].Play();
        }
    }
    public void Music1stLap()
    {
        if (!m_audios[7].isPlaying)
        {
            m_audios[7].Play();
        }
    }
    public void MansionMusic()
    {
        if (!m_audios[8].isPlaying)
        {
            m_audios[8].Play();
        }
    }
    public void CrashCar()
    {
        if (!m_audios[9].isPlaying)
        {
            m_audios[9].Play();
        }
    }
    public void CarHorn()
    {
        if (!m_audios[10].isPlaying)
        {
            m_audios[10].Play();
        }
    }
    public void countDownSound()
    {
        if (!m_audios[11].isPlaying)
        {
            m_audios[11].PlayOneShot(m_audios[11].clip, 1f);
        }
    }
    public void StopDrift()
    {
        for (int i = 0; i < m_audios.Length; i++)
        {
            m_audios[4].Stop();
        }
    }
}
