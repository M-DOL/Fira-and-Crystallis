using UnityEngine;
using System.Collections;

public class Fire : Character
{
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attacking = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ice")
        {
            Level.s.SomeoneDied();
        }
    }




    // Update is called once per frame
    public GameObject fireBlast, fireBlastGO;
    public float blastTime = 3f;
    public bool attacking = false;
    float blastStart;
    void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Start Attack
        if (Input.GetKey(KeyCode.Space) && !attacking) {
            Attack();
            blastStart = Time.time;
            attacking = true;
        }

        //End Attack
        if (attacking && (Time.time - blastStart > blastTime)) {
            Destroy(fireBlastGO);
            attacking = false;
        }
    }


    public override void Attack() {
        //See what direction it is facing
        //Create fireBlast in that direction
        //      Make fireBlast script that reacts if in contact w/ something
        Vector3 pos = transform.position;
        pos.y++;
        print(transform.forward);
        fireBlastGO = Instantiate(fireBlast, pos, Quaternion.identity) as GameObject;

    }
}
