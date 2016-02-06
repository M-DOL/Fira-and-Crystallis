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
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.name == "Fire")
            {
                if(state == STATE.Ice)
                {
                    Level.S.SomeoneDied();
                }
                state = STATE.Fire;
                if (stopping != null)
                {
                    StopCoroutine(stopping);
                }
            }
            else
            {
                if(state == STATE.Fire)
                {

                    Level.S.SomeoneDied();
                }
                
                state = STATE.Ice;
                if (stopping != null)
                {
                    StopCoroutine(stopping);
                }
            }
        }
    }

    Coroutine stopping;

    IEnumerator cooldown(float f)
    {
        yield return new WaitForSeconds(f);
        
        state = STATE.Neutral;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!Ends)
        {
            return;
        }
        stopping = StartCoroutine(cooldown(ConductivityTime));
    }




    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == STATE.Neutral)
        {
            sr.sprite = Neut;
        } else if (state == STATE.Fire)
        {
            sr.sprite = Fir;
        } else
        {
            sr.sprite = Ic;
        }
    }
}
