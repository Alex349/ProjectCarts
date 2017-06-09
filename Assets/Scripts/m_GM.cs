using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_GM : MonoBehaviour {

    public static m_GM gameManager;
    public static GameObject[] mainPlayer;
    public static GameObject[] IAPlayers;
    public static Transform[] initSpawnPoints;
    public static GameObject CanvasAmazing;
    private static float counter = 8;
    private static CarSmoothFollow m_cameraScript;
    private static GameObject[] cameraPoints;
    private static float cameraSpeed = 5;
    public GameObject centricPoint;
    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameManager);
        }

        CanvasAmazing.SetActive(false);
        m_cameraScript = Camera.main.GetComponent<CarSmoothFollow>();

        cameraPoints = new GameObject[8];

        for (int i = 0; i < cameraPoints.Length; i++)
        {
            cameraPoints = GameObject.FindGameObjectsWithTag("CameraPoint");

            if (cameraPoints[i] != null)
            {
                cameraPoints[i] = GameObject.FindGameObjectWithTag("CameraPoint");
            }
            else
            {
                cameraPoints[i+1] = GameObject.FindGameObjectWithTag("CameraPoint");
            }
        }
    }
	void Start ()
    {        

		
    }
	
	void Update ()
    {
        counter -= Time.deltaTime;

		if (counter >= 0)
        {
            CameraTravel();
        }
        else if (mainPlayer == null)
        {
            if (MenuScript.SelectionIndex == 0)
            {
                Instantiate(mainPlayer[0], initSpawnPoints[11].transform.position, Quaternion.identity);
            }
            else if (MenuScript.SelectionIndex == 1)
            {
                Instantiate(mainPlayer[1], initSpawnPoints[11].transform.position, Quaternion.identity);
            }
            else if (MenuScript.SelectionIndex == 2)
            {
                Instantiate(mainPlayer[2], initSpawnPoints[11].transform.position, Quaternion.identity);
            }
            else if (MenuScript.SelectionIndex == 3)
            {
                Instantiate(mainPlayer[3], initSpawnPoints[11].transform.position, Quaternion.identity);
            }
            else if (MenuScript.SelectionIndex != 0 && MenuScript.SelectionIndex != 1 && MenuScript.SelectionIndex != 2 && MenuScript.SelectionIndex != 3)
            {
                Instantiate(mainPlayer[3], initSpawnPoints[11].transform.position, Quaternion.identity);
            }
            for (int i = 0; i < IAPlayers.Length; i++)
            {
                Instantiate(IAPlayers[i], initSpawnPoints[i].transform.position, Quaternion.identity);
            }
        }
	}
    public static bool CameraTravel()
    {
        if (counter <= 8 && counter > 7)
        {
            m_cameraScript.target.position = Vector3.Lerp(m_cameraScript.target.position, cameraPoints[1].transform.position, cameraSpeed * Time.deltaTime);
            m_cameraScript.transform.rotation = Quaternion.FromToRotation(cameraPoints[0].transform.position, cameraPoints[1].transform.position);
            return false;
        }
        else if (counter <= 7 && counter > 6)
        {
            m_cameraScript.target.position = Vector3.Lerp(m_cameraScript.target.position, cameraPoints[2].transform.position, cameraSpeed * Time.deltaTime);
            m_cameraScript.transform.rotation = Quaternion.FromToRotation(cameraPoints[1].transform.position, cameraPoints[2].transform.position);
            return false;
        }
        else if (counter <= 6 && counter > 5)
        {
            m_cameraScript.target.position = Vector3.Lerp(m_cameraScript.target.position, cameraPoints[2].transform.position, cameraSpeed * Time.deltaTime);
            m_cameraScript.transform.rotation = Quaternion.FromToRotation(cameraPoints[2].transform.position, cameraPoints[3].transform.position);
            return false;
        }
        else if (counter <= 5 && counter > 4)
        {
            m_cameraScript.target.position = Vector3.Lerp(m_cameraScript.target.position, cameraPoints[3].transform.position, cameraSpeed * Time.deltaTime);
            m_cameraScript.transform.rotation = Quaternion.FromToRotation(cameraPoints[3].transform.position, cameraPoints[4].transform.position);
            return false;
        }
        else if (counter <= 4 && counter > 3)
        {
            m_cameraScript.target.position = Vector3.Lerp(m_cameraScript.target.position, cameraPoints[4].transform.position, cameraSpeed * Time.deltaTime);
            m_cameraScript.transform.rotation = Quaternion.FromToRotation(cameraPoints[4].transform.position, cameraPoints[5].transform.position);
            return false;
        }
        else if (counter <= 3 && counter > 2)
        {
            m_cameraScript.target.position = Vector3.Lerp(m_cameraScript.target.position, cameraPoints[5].transform.position, cameraSpeed * Time.deltaTime);
            m_cameraScript.transform.rotation = Quaternion.FromToRotation(cameraPoints[5].transform.position, cameraPoints[6].transform.position);
            return false;
        }
        else if (counter <= 2 && counter > 1)
        {
            m_cameraScript.target.position = Vector3.Lerp(m_cameraScript.target.position, cameraPoints[6].transform.position, cameraSpeed * Time.deltaTime);
            m_cameraScript.transform.rotation = Quaternion.FromToRotation(cameraPoints[6].transform.position, cameraPoints[7].transform.position);
            return false;
        }
        else if (counter <= 1 && counter > 0)
        {
            m_cameraScript.target.position = Vector3.Lerp(m_cameraScript.target.position, cameraPoints[7].transform.position, cameraSpeed * Time.deltaTime);
            m_cameraScript.transform.rotation = Quaternion.FromToRotation(cameraPoints[7].transform.position, cameraPoints[8].transform.position);
            return false;
        }
        else
        {
            return true;
        }
    }
}
