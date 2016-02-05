using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour
{
    public bool isFire = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.name == "Ice" && !isFire)
            {
                Level.s.IceFinish();
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.name == "Fire" && isFire)
            {
                Level.s.FireFinish();

                Destroy(collision.gameObject);
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
