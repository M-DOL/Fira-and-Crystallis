using UnityEngine;
using System.Collections;

public class PowerupGET : MonoBehaviour
{


    public bool isFire = true;
    public string PowerUpName = "FireProj";

    // Use this for initialization
    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(isFire &&collision.gameObject.name == "Fire")
        {
            collision.GetComponent<Character>().Abilities[PowerUpName] = true;
            Destroy(gameObject);
        }
        if (!isFire && collision.gameObject.name == "Ice")
        {
            collision.GetComponent<Character>().Abilities[PowerUpName] = true;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
