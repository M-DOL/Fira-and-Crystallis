using UnityEngine;
using System.Collections;

public class NeutralEnemy : MonoBehaviour
{
    // picks random player and follows them
    GameObject dest;
    public float speed = 2f;
    public int MaxHealth = 5;
    public int CurrentHealth;
    SpriteRenderer sr;
    public GameObject[] activates;
    // Use this for initialization
    void Start()
    {
        CurrentHealth = MaxHealth;
        StartCoroutine(animate());
        GameObject[] ps = GameObject.FindGameObjectsWithTag("Player");
        int i = Mathf.CeilToInt(Random.Range(-0.9f, 1f));
        dest = ps[i];
        sr = GetComponent<SpriteRenderer>();
    }
    bool inv = false;
    IEnumerator getHit()
    {
        inv = true;
        for (int c = 0; c < 2; ++c)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        inv = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dest != null)
        {
            transform.position = Vector3.Lerp(transform.position, dest.transform.position, Time.deltaTime * speed);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "IceProj" || collision.tag == "FireProj")
        {
            if (!inv)
            {
                StartCoroutine(getHit());
                --CurrentHealth;
                if(CurrentHealth < 0)
                {
                    Die();
                }
            }
        }
        if(collision.tag == "Player")
        {
            Level.S.KillCharacter(collision.gameObject);
        }
    }    

    void Die()
    {
        for(int c = 0; c < activates.Length; ++c)
        {

            activates[c].SendMessage("Activate");
        }
        Destroy(gameObject);
    }

    IEnumerator animate()
    {
        while (true)
        {
            for (int c = 1; c < 10; ++c)
            {
                transform.position += Vector3.up * Time.deltaTime / c;
                yield return new WaitForFixedUpdate();
            }
            for (int c = 1; c < 10; ++c)
            {
                transform.position -= Vector3.up * Time.deltaTime / c;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
