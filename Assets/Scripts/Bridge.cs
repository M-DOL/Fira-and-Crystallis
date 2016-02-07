using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour
{
    public GameObject Disables;
    bool fireOn = false, iceOn = false;
    public void Activate()
    {
        Disables.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Deactivate()
    {
        if(fireOn)
        {
            Level.S.KillCharacter(Fire.S.gameObject);
        }
        if(iceOn)
        {
            Level.S.KillCharacter(Ice.S.gameObject);
        }
        Disables.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Fire")
        {
            fireOn = true;
        }
        if (collision.gameObject.tag == "Ice")
        {
            iceOn = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            fireOn = false;
        }
        if (collision.gameObject.tag == "Ice")
        {
            iceOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
