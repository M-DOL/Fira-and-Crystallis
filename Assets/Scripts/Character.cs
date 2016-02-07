﻿using UnityEngine;
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
    public GameObject puddle;
    public Dictionary<string, bool> Abilities = new Dictionary<string, bool>();

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
        rb.velocity = Vector2.zero;

        Level.S.stop = true;
        Destroy(sr);
        Destroy(GetComponent<SpriteAnimator>());
        Instantiate(puddle, transform.position, Quaternion.identity);
        StartCoroutine(Level.S.SomeoneDied());
    }
}
