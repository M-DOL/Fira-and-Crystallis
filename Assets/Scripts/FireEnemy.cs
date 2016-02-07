using UnityEngine;
using System.Collections;

public class FireEnemy : MonoBehaviour
{

    public GameObject Projectile;
    public bool TargetIsFire = false;
    public float fireTime = 3f;

    GameObject fire, ice;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(animate());
        StartCoroutine(Shoot());
        fire = Fire.S.gameObject;
        ice = Ice.S.gameObject;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Fire")
        {
            if (!TargetIsFire)
            {
                Destroy(gameObject);
            }
            else
            {

            }
        }

        if(collision.gameObject.name == "Ice")
        {
            if (TargetIsFire)
            {
                Destroy(gameObject);
            }
            else
            {

            }
        }
        if (TargetIsFire)
        {
            // this is ice man
            if(collision.gameObject.tag == "FireProj")
            {
                Destroy(gameObject);
            }
        } else
        {
            if(collision.gameObject.tag == "IceProj")
            {
                Destroy(gameObject);
            }
        }
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

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireTime);
            if (TargetIsFire)
            {
                Vector3 dir = Vector3.up;
                if (fire != null)
                {
                    dir = fire.transform.position - transform.position;
                }
                dir = dir.normalized;
                GameObject g = Instantiate(Projectile, transform.position, transform.rotation) as GameObject;
                g.GetComponent<Rigidbody2D>().velocity = dir;
            }
            else
            {
                Vector3 dir = Vector3.up;
                if (ice != null)
                {
                    dir = ice.transform.position - transform.position;
                }
                dir = dir.normalized;
                GameObject g = Instantiate(Projectile, transform.position, transform.rotation) as GameObject;
                g.GetComponent<Rigidbody2D>().velocity = dir;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
