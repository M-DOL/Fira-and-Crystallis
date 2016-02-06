using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    public GameObject blastPrefab;
    public float speed = 3f;
    public Rigidbody2D rb;
    public SpriteAnimator sa;
    public bool attacked = false;
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
}
