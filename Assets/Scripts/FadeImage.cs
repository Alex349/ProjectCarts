using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour {

    private m_carHUD carHUD;

    public float FadeRate;
    private Image image;
    private float targetAlpha;

    // Use this for initialization
    void Start () {
		    carHUD = GameObject.Find("HUDManager").GetComponent<m_carHUD>();

        this.image = this.GetComponent<Image>();
        if (this.image == null)
        {
            Debug.LogError("Error: No image on " + this.name);
        }

        this.targetAlpha = 0;

    }
	
	// Update is called once per frame
	void Update () {

        if (carHUD.countDown < -0.8)
        {
        Color curColor = this.image.color;
        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.image.color = curColor;
        }
        }

    }

    public void FadeOut()
    {
        this.targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        this.targetAlpha = 1.0f;
    }
}
