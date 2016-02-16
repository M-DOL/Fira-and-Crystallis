using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : MonoBehaviour {
    public Sprite on, off;
    SpriteRenderer sr;
    public List<GameObject> bridges;
	// Use this for initialization
	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Block" || col.gameObject.tag == "Player")
        {
            sr.sprite = on;
            foreach (GameObject go in bridges)
            {
                go.SendMessage("Activate");
            }
            Level.S.PlaySound("Button Push");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        sr.sprite = off;
        foreach (GameObject go in bridges)
        {
            go.SendMessage("Deactivate");
        }
    }

    /*void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Block" || col.gameObject.tag == "Player")
        {
            foreach (GameObject go in bridges)
            {
                go.SendMessage("Activate");
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        foreach (GameObject go in bridges)
        {
            go.SendMessage("Deactivate");
        }
    }*/
}
