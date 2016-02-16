using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {


    public bool burnt, frozen, pushed;
    public Material fire, ice;
    Renderer rend;
    Rigidbody2D rb;
    public float burnTime = 2f, burnStart;
    public float block_move_speed = 2f;
    Vector3 last_position;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 1000000;
        pushed = false;
        last_position = this.transform.position;
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if (rb.velocity.magnitude <= 0f) {
            pushed = false;
        }
        if (!pushed && this.transform.position != last_position) {
            this.transform.position = last_position;        
        }
        if(burnt && Time.time - burnStart > burnTime) {
            Destroy(this.gameObject);
        }
        last_position = this.transform.position;
	}

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Fire") {
            burnBlock();
        } else if (col.gameObject.name == "Ice") {
            freezeBlock(getDirectionVector(this.transform, col.gameObject.transform));
            pushed = true;
        } else if (col.gameObject.tag == "Block") {
            rb.velocity = Vector2.zero;
            col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        } else if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Pit") {
            rb.velocity = Vector2.zero;
            rb.transform.position += getDirectionVector(this.transform, col.gameObject.transform) * 0.25f;
        }
    }

    void OnCollisionStay2D(Collision2D col) {
        if (col.gameObject.tag == "Block" && !pushed) {
            rb.velocity = Vector2.zero;
            if (!col.gameObject.GetComponent<Block>().pushed) {
                col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
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
        burnt = true;
        burnStart = Time.time;
        frozen = false;
        Level.S.PlaySound("Burning Block");
    }

    void freezeBlock(Vector3 direction) {
        rend.sharedMaterial = ice;
        rb.velocity = direction * block_move_speed;
        frozen = true;
        burnt = false;
        Level.S.PlaySound("blockScrape");
    }

    Vector3 getDirectionVector(Transform start, Transform end) {
        Vector3 direction = start.position - end.position;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            direction = new Vector3(direction.x, 0f, 0f);
        } else {
            direction = new Vector3(0f, direction.y, 0f);
        }
        return direction.normalized;
    }
}
