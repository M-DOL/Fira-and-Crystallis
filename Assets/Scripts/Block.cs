using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {


    public bool burnt, frozen;
    public Material fire, ice;
    Renderer rend;
    Rigidbody2D rb;
    public float burnTime = 2f, burnStart;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if(burnt && Time.time - burnStart > burnTime) {
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Fire") {
            burnBlock();
        } else if (col.gameObject.name == "Ice") {
            freezeBlock();
        } else if (col.gameObject.tag == "Block") {
            rb.velocity = Vector2.zero;
            col.gameObject.GetComponent<Rigidbody2D>().mass = 100000;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "FireProj") {
            burnBlock();
            Destroy(col.gameObject);
        }
    }

    void burnBlock() {
        rend.sharedMaterial = fire;
        rb.velocity = Vector2.zero;
        rb.mass = 100000;
        burnt = true;
        burnStart = Time.time;
        frozen = false;
        Level.S.PlaySound("Burning Block");
    }

    void freezeBlock() {
        rend.sharedMaterial = ice;
        rb.mass = 10;
        frozen = true;
        burnt = false;
        Level.S.PlaySound("blockScrape");
    }
}
