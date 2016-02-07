﻿using UnityEngine;
using System.Collections;

public class IceBlast : Blast
{
    void Awake()
    {
        isIce = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Fire")
        {
            Destroy(gameObject);
            Fire.S.Kill();
        }
        if (col.gameObject.layer == wallLayer)
        {
            Destroy(gameObject);
            Ice.S.attacked = false;
        }
        //Set target to burn
    }
}
