using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Torch : MonoBehaviour
{
    public List<GameObject> Activates;
    public List<GameObject> Deactivates;
    public Sprite unlit, lit;
    public SpriteRenderer sr;
    public bool isLit = false;
    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (isLit)
        {
            sr.sprite = lit;
            foreach (GameObject go in Activates)
            {
                if (go.gameObject.tag == "Bridge")
                {
                    go.SendMessage("Activate");
                }
                else
                {
                    go.SetActive(true);
                }
            }
        }
        else
        {
            foreach (GameObject go in Deactivates)
            {
                if (go.gameObject.tag == "Bridge")
                {
                    go.SendMessage("Activate");
                }
                else
                {
                    go.SetActive(true);
                }
            }
        }
    }

    void Light()
    {
        Level.S.PlaySound("Torch Light");
        sr.sprite = lit;
    }

    void PutOut()
    {
        Level.S.PlaySound("Torch Out");
        sr.sprite = unlit;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Fire" || collision.gameObject.tag == "FireProj")
        {
            Light();
            if (Activates != null)
            {
                foreach (GameObject go in Activates)
                {
                    if (go.gameObject.tag == "Bridge")
                    {
                        go.SendMessage("Activate");
                    }
                    else
                    {
                        go.SetActive(true);
                    }
                }
            }
            if (Deactivates != null)
            {
                foreach (GameObject go in Deactivates)
                {
                    if (go.gameObject.tag == "Bridge")
                    {
                        go.SendMessage("Deactivate");
                    }
                    else
                    {
                        go.SetActive(false);
                    }
                }
            }
        }
        if (collision.gameObject.name == "Ice" || collision.gameObject.tag == "IceProj")
        {
            PutOut();
            if (Activates != null)
            {
                foreach (GameObject go in Activates)
                {
                    if (go.gameObject.tag == "Bridge")
                    {
                        go.SendMessage("Deactivate");
                    }
                    else
                    {
                        go.SetActive(false);
                    }
                }
            }
            if (Deactivates != null)
            {
                foreach (GameObject go in Deactivates)
                {
                    if (go.gameObject.tag == "Bridge")
                    {
                        go.SendMessage("Activate");
                    }
                    else
                    {
                        go.SetActive(true);
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
