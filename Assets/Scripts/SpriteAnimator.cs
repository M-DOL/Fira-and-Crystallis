using UnityEngine;
using System.Collections;

public class SpriteAnimator : MonoBehaviour {
    public Sprite[] LeftWalk;
    public Sprite[] DownWalk;
    public Sprite[] UpWalk;
    public Sprite[] RightWalk;
    public bool finished = false;
    int c = 0;
    public string lastDir = "Down";
    SpriteRenderer sr;
    Vector2 vel;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(changer());
	}
    bool reversing = false;
    IEnumerator changer()
    {
        while (true)
        {
            if (vel.magnitude >= 0.01)
            {
                if (!reversing)
                {
                    ++c;
                } else
                {
                    --c;
                }
                if (c >= LeftWalk.Length)
                {
                    reversing = true;
                    --c;
                    --c;
                }
                if ( c < 0)
                {
                    reversing = false;
                    ++c;
                    ++c;
                }
                yield return new WaitForSeconds(0.2f);
            } else
            {
                c = 0;
                yield return new WaitForFixedUpdate();
            }
        }
    }

    void FixedUpdate()
    {
        if (finished)
        {
            sr.sprite = DownWalk[1];
            Ice.S.transform.position = Ice.S.finLoc;
            Fire.S.transform.position = Fire.S.finLoc;
            return;
        }
        vel = rb.velocity;
        if (vel.magnitude < 0.01)
        {
            if(lastDir == "Down")
            {
                sr.sprite = DownWalk[0];
            }
            else if (lastDir == "Up")
            {
                sr.sprite = UpWalk[0];
            }
            else if (lastDir == "Left")
            {
                sr.sprite = LeftWalk[0];
            }
            else if (lastDir == "Right")
            {
                sr.sprite = RightWalk[0];
            }
        }
        else
        {
            if (vel.x < 0 && -vel.x > Mathf.Abs(vel.y))
            {
                sr.sprite = LeftWalk[c];
                lastDir = "Left";
            } else if(vel.x > 0 && vel.x > Mathf.Abs(vel.y))
            {
                sr.sprite = RightWalk[c];
                lastDir = "Right";
            } else if (vel.y > 0 && vel.y > Mathf.Abs(vel.x))
            {
                sr.sprite = UpWalk[c];
                lastDir = "Up";
            } else if (vel.y < 0 && -vel.y > Mathf.Abs(vel.x))
            {
                sr.sprite = DownWalk[c];
                lastDir = "Down";
            }
        }
    }
}
