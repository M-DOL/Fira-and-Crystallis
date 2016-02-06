using UnityEngine;
using System.Collections;

public class FireBlast : Blast
{
    void Awake()
    {
        isIce = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Ice")
        {
            StartCoroutine(Level.S.SomeoneDied());
        }
        if (col.gameObject.layer == wallLayer)
        {
            Destroy(gameObject);
            Fire.S.attacked = false;
        }
        //Set target to burn
    }
}
