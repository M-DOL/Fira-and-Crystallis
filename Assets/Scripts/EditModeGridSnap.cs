using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EditModeGridSnap : MonoBehaviour
{
    public float snapValue = 1;
    public float depth = 0;
    private SpriteRenderer sr;
    private BoxCollider2D boxCol;
    private LayerMask pitLayer;
    public Sprite alert;
    public Sprite[] grounds;
    private int pitL, groundL;
    public bool left, top, right, bottom;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        boxCol = gameObject.GetComponent<BoxCollider2D>();
        pitLayer = LayerMask.GetMask("Pit");
        pitL = LayerMask.NameToLayer("Pit");
        groundL = LayerMask.NameToLayer("Ground");
    }
    void Update()
    {
        if (!Application.isPlaying)
        {
            float snapInverse = 1 / snapValue;

            float x, y, z;

            // if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
            // so 1.45 to nearest .5 is 1.5
            x = Mathf.Round(transform.position.x * snapInverse) / snapInverse;
            y = Mathf.Round(transform.position.y * snapInverse) / snapInverse;
            z = depth;  // depth from camera

            transform.position = new Vector3(x, y, z);
            if (gameObject.layer == groundL)
            {
                foreach (var hit in Physics2D.RaycastAll(transform.position, Vector2.up, .1f))
                {
                    if (hit.transform.gameObject.layer == pitL
                        || (hit.transform.gameObject.layer == groundL && transform != hit.transform))
                    {
                        sr.sprite = alert;
                        sr.sortingOrder = 0;
                        return;
                    }
                    sr.sortingOrder = -5;
                }
                float offset = .3f;
                left = Physics2D.Raycast(new Vector2(boxCol.bounds.min.x - offset, transform.position.y), Vector2.left, offset, pitLayer);
                right = Physics2D.Raycast(new Vector2(boxCol.bounds.max.x + offset, transform.position.y), Vector2.right, offset, pitLayer);
                top = Physics2D.Raycast(new Vector2(transform.position.x, boxCol.bounds.max.y + offset), Vector2.up, offset, pitLayer);
                bottom = Physics2D.Raycast(new Vector2(transform.position.x, boxCol.bounds.min.y - offset), Vector2.down, offset, pitLayer);
                int i = 6;
                if(gameObject.tag == "NeutralGround")
                {
                    i -= 6;
                }
                if (left && top && bottom && right)
                {
                    i += 5;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (left && top && right)
                {
                    i += 4;
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (left && top && bottom)
                {
                    i += 4;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (left && bottom && right)
                {
                    i += 4;
                    transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (bottom && top && right)
                {
                    i += 4;
                    transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (left && top)
                {
                    i += 3;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (top && right)
                {
                    i += 3;
                    transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (left && bottom)
                {
                    i += 3;
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (bottom && right)
                {
                    i += 3;
                    transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (left && right)
                {
                    i += 2;
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (top && bottom)
                {
                    i += 2;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (top)
                {
                    i += 1;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (left)
                {
                    i += 1;
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (right)
                {
                    i += 1;
                    transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (bottom)
                {
                    i += 1;
                    transform.eulerAngles = new Vector3(0, 0, 180);
                }
                sr.sprite = grounds[i];
            }
        }
    }
}
