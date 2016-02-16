using UnityEngine;
using System.Collections;

public class Ice : Character
{
    KeyCode FIRE_BUTTON = KeyCode.LeftControl;

    public static Ice S;
    public GameObject iceBlock;

    void Awake()
    {
        S = this;
        respawnLocation = transform.position;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sa = GetComponent<SpriteAnimator>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (Abilities["IceHug"])
        {
            return;
        }
        if (collision.gameObject.name == "Fire")
        {
            Kill();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyIceProj")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "FireProj" || collision.gameObject.tag == "EnemyFireProj")
        {
            if (collision.gameObject.tag == "FireProj")
            {
                Fire.S.attacked = false;
            }
            Destroy(collision.gameObject);
            Kill();
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Level.S.stop)
        {
            return;
        }
        Move(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
        if (!attacked && Input.GetKeyDown(FIRE_BUTTON) && Abilities["IceProj"])
        {
            Level.S.PlaySound("Ice Shot");
            Attack();
        }
    }
}
