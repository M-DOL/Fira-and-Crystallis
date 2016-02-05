using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public float Speed = 3f;


    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}


    public void Move(float h, float v)
    {
        rb.velocity = new Vector2(h, v) * Speed;
    }

	
	// Update is called once per frame
	void Update () {
	}
}
