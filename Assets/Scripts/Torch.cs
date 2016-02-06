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
                go.SendMessage("Activate");
            }
        }
        else
        {
            foreach (GameObject go in Deactivates)
            {
                go.SendMessage("Activate");
            }
        }
    }

    void Light()
    {
        sr.sprite = lit;
    }

    void PutOut()
    {
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
                    go.SendMessage("Activate");
                }
            }
            if (Deactivates != null)
            {
                foreach (GameObject go in Deactivates)
                {
                    go.SendMessage("Deactivate");
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
                    go.SendMessage("Deactivate");
                }
            }
            if (Deactivates != null)
            {
                foreach (GameObject go in Deactivates)
                {
                    go.SendMessage("Activate");
                }
            }
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
