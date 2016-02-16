using UnityEngine;
using System.Collections;

public class laser : MonoBehaviour
{
    bool lethal = true;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!lethal)
        {
            return;
        }
         if(collision.tag == "Player")
        {
            Level.S.KillCharacter(collision.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(animate());
    }

    IEnumerator animate()
    {
        yield return new WaitForSeconds(0.3f);
        lethal = false;
        yield return new WaitForSeconds(3f);
        //Destroy(gameObject.transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
