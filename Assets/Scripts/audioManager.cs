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
        else if (m_audios[15].isPlaying)
        {
            m_audios[15].Stop();
        }
    }
    public void Turbo()
    {
        if (!m_audios[5].isPlaying)
        {
            m_audios[5].Play();
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
    public void ButtonMenuOK()
    {
        if (!m_audios[12].isPlaying)
        {
            m_audios[12].Play();
        }
    }
    public void ButtonMenuBack()
    {
        if (!m_audios[13].isPlaying)
        {
            m_audios[13].Play();
        }
    }
    public void ButtonMenuNext()
    {
        m_audios[14].PlayOneShot(m_audios[14].clip, 1f);
      
    }
    public void StopDrift()
    {
        m_audios[4].Stop();
        m_audios[15].Stop();
    }
    public void Contravolant()
    {
        if (!m_audios[15].isPlaying)
        {
            m_audios[15].Play();
        }
        else if (m_audios[4].isPlaying)
        {
            m_audios[4].Stop();
        }
    }
    public void CrashCar2()
    {
        if (!m_audios[16].isPlaying)
        {
            m_audios[16].Play();
        }
    }
    public void NoPJ()
    {
        if (!m_audios[17].isPlaying)
        {
            m_audios[17].Play();
        }
    }

    public void LauhgPJ()
    {
        if (!m_audios[18].isPlaying)
        {
            m_audios[18].Play();
        }
    }
    public void YeahPJ()
    {
        if (!m_audios[19].isPlaying)
        {
            m_audios[19].Play();
        }
    }
    public void OopsPJ()
    {
        if (!m_audios[20].isPlaying)
        {
            m_audios[20].Play();
        }
    }

}
