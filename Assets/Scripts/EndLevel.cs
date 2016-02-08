using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour
{
    public bool isFire = false;

    public Sprite FirenoStand, FireStand, IcenoStand, IceStand;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.name == "Ice" && !isFire)
            {
                GetComponent<SpriteRenderer>().sprite = IceStand;
                Level.S.IceFinish();
            }
            if (collision.gameObject.name == "Fire" && isFire)
            {

                GetComponent<SpriteRenderer>().sprite = FireStand;
                Level.S.FireFinish();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.name == "Ice" && !isFire)
            {
                GetComponent<SpriteRenderer>().sprite = IcenoStand;
                Level.S.IceFin = false;
            }
            if (collision.gameObject.name == "Fire" && isFire)
            {

                GetComponent<SpriteRenderer>().sprite = FirenoStand;
                Level.S.FireFin = false;
            }
        }
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
