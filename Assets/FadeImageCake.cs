using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImageCake : MonoBehaviour
{

    private m_carItem carItem;

    public float FadeRate;
    private Image image;
    private float targetAlpha;
    public Color curColor;

    // Use this for initialization
    void Start()
    {
        carItem = GameObject.FindGameObjectWithTag("Player").GetComponent<m_carItem>();

        this.image = this.GetComponent<Image>();
        curColor = this.image.color; ;

        if (this.image == null)
        {
            Debug.LogError("Error: No image on " + this.name);
        }

        this.targetAlpha = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (carItem.bananaEffect >= 6.9)
        {
            curColor.a = 100;
            this.image.color = curColor;
        }

        if (carItem.bananaEffect < 4) // && carItem.bananaEffect > -5
        {

            float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
            if (alphaDiff > 0.0001f)
            {
                curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
                this.image.color = curColor;
            }
        }

        if (carItem.bananaEffect < -6)
        {
            Color curColor = this.image.color;
            curColor.a = 100;
            this.image.color = curColor;
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
