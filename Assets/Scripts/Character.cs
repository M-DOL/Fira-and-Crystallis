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
    public bool allowDiagonalMovement = true;

    public Dictionary<int, bool> colliding_tiles = new Dictionary<int, bool>();
    Vector2 last_frame_velocity = Vector2.zero;

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
        if (!allowDiagonalMovement && h != 0 && v != 0) {
            return;
        }

        int total_colliding_tiles = 0;
        foreach (bool is_colliding in colliding_tiles.Values) {
            if (is_colliding) {
                ++total_colliding_tiles;
            }
        }

        Vector2 new_velocity = new Vector2(h, v) * speed;
        if (total_colliding_tiles <= 1 || !changedMovementAxis(new_velocity)) {
            rb.velocity = new_velocity;
            last_frame_velocity = new_velocity;
        }
    }

    bool changedMovementAxis(Vector2 new_velocity) {
        if (last_frame_velocity == new_velocity || last_frame_velocity == (new_velocity * -1)) {
            return false;
        }
        return true;
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
