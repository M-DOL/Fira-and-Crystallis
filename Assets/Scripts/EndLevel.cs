using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour
{
    public bool isFire = false;
    public ParticleSystem ps;
    public Sprite fire, ice, finish;
    public Color fireStart, fireEnd, iceStart, iceEnd, finishStart, finishEnd;
    private ParticleSystem.ColorOverLifetimeModule col;
    private Gradient fireGrad, iceGrad, finishGrad;
    // Use this for initialization
    void Start()
    {
        if(isFire)
        {
            Fire.S.finLoc = transform.position;
        }
        else
        {
            Ice.S.finLoc = transform.position;
        }
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps = GetComponent<ParticleSystem>();
        col = ps.colorOverLifetime;
        fireGrad = new Gradient();
        fireGrad.SetKeys(new GradientColorKey[] { new GradientColorKey(fireStart, 0.0f), new GradientColorKey(fireEnd, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        iceGrad = new Gradient();
        iceGrad.SetKeys(new GradientColorKey[] { new GradientColorKey(iceStart, 0.0f), new GradientColorKey(iceEnd, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        finishGrad = new Gradient();
        finishGrad.SetKeys(new GradientColorKey[] { new GradientColorKey(finishStart, 0.0f), new GradientColorKey(finishEnd, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.name == "Ice" && !isFire)
            {
                GetComponent<SpriteRenderer>().sprite = finish;
                col.color = new ParticleSystem.MinMaxGradient(finishGrad);
                Level.S.IceFinish();
            }
            if (collision.gameObject.name == "Fire" && isFire)
            {
                GetComponent<SpriteRenderer>().sprite = finish;
                col.color = new ParticleSystem.MinMaxGradient(finishGrad);
                Level.S.FireFinish();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.name == "Ice" && !isFire)
            {
                GetComponent<SpriteRenderer>().sprite = ice;
                col.color = new ParticleSystem.MinMaxGradient(iceGrad);
                Level.S.IceFin = false;
            }
            if (collision.gameObject.name == "Fire" && isFire)
            {
                GetComponent<SpriteRenderer>().sprite = fire;
                col.color = new ParticleSystem.MinMaxGradient(fireGrad);
                Level.S.FireFin = false;
            }
        }
    }
}
