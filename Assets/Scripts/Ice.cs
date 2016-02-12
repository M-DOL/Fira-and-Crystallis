using UnityEngine;
using System.Collections;

public class Ice : Character
{
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
        if (!attacked && Input.GetKeyDown(KeyCode.LeftShift) && Abilities["IceBlock"])
        {
            Level.S.PlaySound("Ice Shot");
            string dir = sa.lastDir;
            Vector3 direction = Vector3.zero;
            switch (dir)
            {
                case "Down":
                    direction = Vector3.down;
                    break;
                case "Up":
                    direction = Vector3.up;
                    break;
                case "Left":
                    direction = Vector3.left;
                    break;
                case "Right":
                    direction = Vector3.right;
                    break;
            }
            Instantiate(iceBlock, transform.position + direction, Quaternion.Euler(Vector3.zero));
        }
    }
}
