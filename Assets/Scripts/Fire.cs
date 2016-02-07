using UnityEngine;
using System.Collections;

public class Fire : Character
{
    // Update is called once per frame
    public static Fire S;

    void Awake()
    {
        S = this;
    }
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sa = GetComponent<SpriteAnimator>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ice")
        {
            Kill();
        }
    }
    void Update()
    {
        if (!Level.S.stop)
        {
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (!attacked && Input.GetKeyDown(KeyCode.Space) && Abilities["FireProj"])
            {
                Level.S.PlaySound("Fire Shot");
                Attack();
            }
        }
    }
}
