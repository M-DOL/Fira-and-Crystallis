using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

    public GameObject[] activates;
    public GameObject FireProj, IceProj, LASER;
    public Sprite spriteN, spriteF, spriteI;
    //public bool TargetIsFire = false;
    public float fireTime = 3f;
    public float speed = 3f;
    GameObject fire, ice;
    public int MaxHealth = 20;
    public int CurrentHealth = 20;
    Vector3 dest = Vector3.zero;
    public SpriteRenderer sr;
    // Use this for initialization
    void Start()
    { 
        fire = Fire.S.gameObject;
        ice = Ice.S.gameObject;
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(animate());
        StartCoroutine(chooseDir());
        StartCoroutine(shoot());
        //StartCoroutine(Shoot());
    }
    bool inv = false;
    IEnumerator getHit()
    {
        --CurrentHealth;
        inv = true;
        for (int c = 0; c < 2; ++c)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        inv = false;
        PhaseChangeCheck();
        if (CurrentHealth < 0)
        {
            Die();
        }
    }

    bool PHASE2 = false;

    void PhaseChangeCheck()
    {
        if(CurrentHealth < 15)
        {
            isNeutral = false;
            isFire = true;
        }
        if(CurrentHealth < 10)
        {
            isNeutral = false;
            isFire = false;
        } 
        if(CurrentHealth < 5)
        {
            PHASE2 = true;
            isNeutral = true;
            isFire = true;
        }
        if (isNeutral)
        {
            sr.sprite = spriteN;
        } else
        {
            if (isFire)
            {
                sr.sprite = spriteF;
            } else
            {
                sr.sprite = spriteI;
            }
        }
    }
    void Die()
    {
        for (int c = 0; c < activates.Length; ++c)
        {
            activates[c].SetActive(true);
        }
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "IceProj" || collision.tag == "FireProj")
        {
            if (!inv)
            {
                if (isNeutral)
                {
                    StartCoroutine(getHit());
                } else
                {
                    if (isFire)
                    {
                        if(collision.tag == "FireProj")
                        {

                            StartCoroutine(getHit());
                        }
                    }
                    if (!isFire)
                    {
                        if(collision.tag == "IceProj")
                        {

                            StartCoroutine(getHit());
                        }
                    }
                }
            }
        }
        if (collision.tag == "Player")
        {
            if(collision.name == "Ice")
            {
                Ice.S.Kill();
            }
            if(collision.name == "Fire")
            {
                Fire.S.Kill();
            }
            //Change to respawn stuff
            //Level.S.KillCharacter(collision.gameObject);
        }
    }

    bool isNeutral = true;
    bool isFire = false;
    public GameObject crosshair, crosshair2;
    IEnumerator chooseDir()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            float x = Random.Range(-4f, 4f);
            float y = Random.Range(-4f, 4f);
            if (PHASE2)
            {

                Instantiate(crosshair2, new Vector3(x, y, 0), transform.rotation);
            }
            else {
                Instantiate(crosshair, new Vector3(x, y, 0), transform.rotation);
            }
            yield return new WaitForSeconds(1f);
            // fire lasers
            if (PHASE2)
            {
                Quaternion q = new Quaternion();
                q = Quaternion.Euler(new Vector3(0, 0, -90));
                Instantiate(LASER, new Vector3(x, y, 0), transform.rotation);
                Instantiate(LASER, new Vector3(x, y, 0), q);
            }
            dest = new Vector3(x, y, 0);
        }
    }


    public void ShootAtFire()
    {

        Vector3 dir = Vector3.up;
        if (fire != null)
        {
            dir = fire.transform.position - transform.position;
        }
        dir = dir.normalized;
        GameObject g = Instantiate(IceProj, transform.position, transform.rotation) as GameObject;
        g.GetComponent<Rigidbody2D>().velocity = dir;
    }

    public void ShootAtIce()
    {
        Vector3 dir = Vector3.up;
        if (ice != null)
        {
            dir = ice.transform.position - transform.position;
        }
        dir = dir.normalized;
        GameObject g = Instantiate(FireProj, transform.position, transform.rotation) as GameObject;
        g.GetComponent<Rigidbody2D>().velocity = dir;
    }

    public void TakeDamage()
    {

    }

    IEnumerator shoot()
    {

        while (true)
        {
            if (isNeutral)
            {
                ShootAtFire();
                yield return new WaitForSeconds(0.6f);
                ShootAtIce();
                yield return new WaitForSeconds(0.6f);
                ShootAtFire();
                yield return new WaitForSeconds(0.6f);
                ShootAtIce();
                yield return new WaitForSeconds(0.6f);
            } else
            {
                if (isFire)
                {
                    ShootAtIce();
                    yield return new WaitForSeconds(0.1f);
                } else
                {
                    ShootAtFire();
                    yield return new WaitForSeconds(0.1f);
                }
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

    // Update is called once per frame
    void Update () {
        if (dest != Vector3.zero)
        {
            transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime * speed);
        }
    }
}
