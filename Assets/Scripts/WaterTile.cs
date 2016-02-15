using UnityEngine;
using System.Collections;

public class WaterTile : MonoBehaviour
{
    public Color neutral, water;
    public enum WaterType { Hot, Cold };

    public float water_removed_time = 1f;
    public int total_fill_in_stages;
    public bool water_removed;
    public WaterType water_type;
    GameObject cover_block;

    SpriteRenderer sr;
    BoxCollider2D coll;
    Coroutine stopping;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (((collision.gameObject.name == "Fire" || collision.gameObject.tag == "FireProj") && water_type == WaterType.Hot) ||
        ((collision.gameObject.name == "Ice" || collision.gameObject.tag == "IceProj") && water_type == WaterType.Cold))
        {
            water_removed = true;
            cover_block.SetActive(true);
            cover_block.transform.localScale = this.transform.localScale;
            if (stopping != null)
            {
                StopCoroutine(stopping);
            }
        }

        if (!water_removed)
        {
            if (water_type == WaterType.Hot)
            {
                if (collision.gameObject.name == "Ice")
                {
                    Ice.S.Kill();
                }
                if (collision.gameObject.tag == "IceProj")
                {
                    Destroy(collision.gameObject);
                    Ice.S.attacked = false;
                    Level.S.PlaySound("Torch Light");
                }
            }
            else if (water_type == WaterType.Cold)
            {
                if(collision.gameObject.name == "Fire")
                {
                    Fire.S.Kill();
                }
                if(collision.gameObject.tag == "FireProj")
                {
                    Destroy(collision.gameObject);
                    Fire.S.attacked = false;
                    Level.S.PlaySound("Torch Out");
                }
            }
        }
    }

    IEnumerator cooldown(int stage)
    {
        yield return new WaitForSeconds(water_removed_time / total_fill_in_stages);
        if (stage < total_fill_in_stages ) {
            cover_block.transform.localScale = this.transform.localScale * (1 - ((float) stage / total_fill_in_stages));
            stopping = StartCoroutine(cooldown(stage + 1));
        } else {
            cover_block.SetActive(false);
            water_removed = false;
            if (water_type == WaterType.Cold && Fire.S != null && coll.bounds.Contains(Fire.S.transform.position)) {
                Fire.S.Kill();
            } else if (water_type == WaterType.Hot && Ice.S != null && coll.bounds.Contains(Ice.S.transform.position)) {
                Ice.S.Kill();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        startCooldown();
    }

    public void startCooldown() {
        stopping = StartCoroutine(cooldown(1));
    }

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        cover_block = Instantiate(Resources.Load("NeutralUntraversable")) as GameObject;
        cover_block.transform.position = this.transform.position;
        cover_block.SetActive(false);
        if (this.gameObject.tag == "HotWater")
        {
            water_type = WaterType.Hot;
        }
        else if (this.gameObject.tag == "ColdWater")
        {
            water_type = WaterType.Cold;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
