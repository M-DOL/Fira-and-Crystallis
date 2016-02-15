using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour
{
    public float dur = .8f, start;
    public float speed = 3f;
    public string dir;
    protected Vector3 direction;
    public bool isIce;
    public int wallLayer;
    public Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        start = Time.time;
        dirTranslate();
        wallLayer = LayerMask.NameToLayer("Wall");
    }

    // Update is called once per frame

    void Update()
    {
        rigid.velocity = direction * speed;
        if (Time.time - start > dur)
        {
            Destroy(gameObject);
            if (isIce)
            {
                Ice.S.attacked = false;
            }
            else
            {
                Fire.S.attacked = false;
            }
        }
    }
    void dirTranslate()
    {
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
    }
}
