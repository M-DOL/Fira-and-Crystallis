using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour
{
    public GameObject Disables;
    public BoxCollider2D col;
    // Update is called once per frame
    public void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }
    public void Activate()
    {
        Disables.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Deactivate()
    {
        if(col.bounds.Contains(Fire.S.transform.position))
        {
            Fire.S.Kill();
        }
        if(col.bounds.Contains(Ice.S.transform.position))
        {
            Ice.S.Kill();
        }
        Disables.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
    }


}
