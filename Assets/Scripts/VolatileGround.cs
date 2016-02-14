using UnityEngine;
using System.Collections;

public class VolatileGround : MonoBehaviour
{
    public Sprite Neut, Fir, Ic;
    public enum STATE { Neutral, Fire, Ice };

    public float ConductivityTime = 1f;
    public bool Ends = true;

    public STATE state = STATE.Neutral;

    SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Neut = sr.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == STATE.Neutral)
        {
            sr.sprite = Neut;
        }
        else if (state == STATE.Fire)
        {
            sr.sprite = Fir;
        }
        else
        {
            sr.sprite = Ic;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.name == "Fire")
            {
                if(state == STATE.Ice)
                {
                    Fire.S.Kill();
                }
                else
                {
                    state = STATE.Fire;
                }
            }
            else
            {
                if(state == STATE.Fire)
                {
                    Ice.S.Kill();
                }
                else
                {
                    state = STATE.Ice;
                }      
            }
        }
    }
}
