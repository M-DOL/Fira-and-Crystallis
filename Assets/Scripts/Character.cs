using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    public GameObject blastPrefab;
    public float speed = 3f;
    public Rigidbody2D rb;
    public SpriteAnimator sa;
    public SpriteRenderer sr;
    public bool attacked = false;
    public GameObject puddle, puddleGO;
    public Dictionary<string, bool> Abilities = new Dictionary<string, bool>();


    public Vector2 respawnLocation;
    public float deathTime = 2f, deathStart;
    public bool dead = false;
    //For Player Respawn w/ Time Delay
    public void FixedUpdate()
    {
        if(dead && Time.time - deathStart > deathTime)
        {
            dead = false;
            this.transform.position = respawnLocation;
            Destroy(puddleGO);
        }
    }

    // Use this for initialization
    public void Move(float h, float v)
    {
        rb.velocity = new Vector2(h, v) * speed;
    }
    public void Attack()
    {
        attacked = true;
        GameObject blast = Instantiate(blastPrefab, transform.position, transform.rotation) as GameObject;
        blast.GetComponent<Blast>().dir = sa.lastDir;
    }
    public void Kill()
    {
        if(Ice.S != null)
        {
            Ice.S.rb.velocity = Vector2.zero;
        }
        if (Fire.S != null)
        {
            Fire.S.rb.velocity = Vector2.zero;
        }
        
        dead = true;
        deathStart = Time.time;
        puddleGO = Instantiate(puddle, transform.position, Quaternion.identity) as GameObject;
        this.transform.position = new Vector2(1000, 1000);
        //Level.S.stop = true;
        //Level.S.KillCharacter(gameObject);
    }
}
