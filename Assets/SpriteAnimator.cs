using UnityEngine;
using System.Collections;

public class SpriteAnimator : MonoBehaviour {
    public Sprite[] LeftWalk;
    public Sprite[] DownWalk;
    public Sprite[] UpWalk;
    public Sprite[] RightWalk;
    int c = 0;
    string LASTDIR = "Down";
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
        vel = rb.velocity;
        if (vel.magnitude < 0.01)
        {
            if(LASTDIR == "Down")
            {
                sr.sprite = DownWalk[0];
            }
            else if (LASTDIR == "Up")
            {
                sr.sprite = UpWalk[0];
            }
            else if (LASTDIR == "Left")
            {
                sr.sprite = LeftWalk[0];
            }
            else if (LASTDIR == "Right")
            {
                sr.sprite = RightWalk[0];
            }
        }
        else
        {
            if (vel.x < 0 && -vel.x > Mathf.Abs(vel.y))
            {
                sr.sprite = LeftWalk[c];
                LASTDIR = "Left";
            } else if(vel.x > 0 && vel.x > Mathf.Abs(vel.y))
            {
                sr.sprite = RightWalk[c];
                LASTDIR = "Right";
            } else if (vel.y > 0 && vel.y > Mathf.Abs(vel.x))
            {
                sr.sprite = UpWalk[c];
                LASTDIR = "Up";
            } else if (vel.y < 0 && -vel.y > Mathf.Abs(vel.x))
            {
                sr.sprite = DownWalk[c];
                LASTDIR = "Down";
            }
        }
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
