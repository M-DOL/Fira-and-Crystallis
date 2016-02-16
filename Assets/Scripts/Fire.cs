using UnityEngine;
using System.Collections;

public class Fire : Character
{
    KeyCode FIRE_BUTTON = KeyCode.RightControl;
    // Update is called once per frame
    public static Fire S;

    void Awake()
    {
        S = this;
        respawnLocation = transform.position;
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sa = GetComponent<SpriteAnimator>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyFireProj")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "IceProj" || collision.gameObject.tag == "EnemyIceProj")
        {
            if (collision.gameObject.tag == "IceProj")
            {
                Ice.S.attacked = false;
            }
            Destroy(collision.gameObject);
            Kill();
        }
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
            if (!attacked && Input.GetKeyDown(FIRE_BUTTON) && Abilities["FireProj"])
            {
                Level.S.PlaySound("Fire Shot");
                Attack();
            }
        }
    }
}
