using UnityEngine;
using System.Collections;

public class Ice : Character
{
    public static Ice S;
    void Awake()
    {
        S = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sa = GetComponent<SpriteAnimator>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fire")
        {
            Kill();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Level.S.stop)
        {
            return;
        }
        Move(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
        if (!attacked && Input.GetKeyDown(KeyCode.LeftControl) && Abilities["IceProj"])
        {
            Level.S.PlaySound("Ice Shot");
            Attack();
        }
    }
}
