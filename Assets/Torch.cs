using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour
{
    public GameObject Activates;
    public GameObject Deactivates;
    public Sprite unlit, lit;
    // Use this for initialization
    void Start()
    {
        Deactivates.SendMessage("Activate");
    }

    void Light()
    {
        GetComponent<SpriteRenderer>().sprite = lit;
    }

    void PutOut()
    {
        GetComponent<SpriteRenderer>().sprite = unlit;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Fire")
        {
            Light();
            if (Activates != null)
            {
                Activates.SendMessage("Activate");
            }
            if (Deactivates != null)
            {
                Deactivates.SendMessage("Deactivate");
            }
        }
        if(collision.gameObject.name == "Ice")
        {
            PutOut();
            if (Activates != null)
            {
                Activates.SendMessage("Deactivate");
            }
            if(Deactivates != null)
            {
                Deactivates.SendMessage("Activate");
            }
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
