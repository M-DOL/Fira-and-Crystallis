using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Title : MonoBehaviour {
    public Vector3[] selectLocs = new Vector3[3];
    public int selectionNum = 0;
    public RectTransform selector;
    Image img;
    public Sprite ice, fire;
	// Use this for initialization
	void Start ()
    {
        selector = transform.FindChild("Selector") as RectTransform;
        img = selector.gameObject.GetComponent<Image>();
        selectLocs[0] = new Vector3(-86, -38, 0);
        selectLocs[1] = new Vector3(-86, -98, 0);
        selectLocs[2] = new Vector3(-86, -158, 0);
    }
    void FixedUpdate()
    {
        selector.Rotate(Vector3.forward, 5f);
    }
    // Update is called once per frame
    void Update()
{
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            img.sprite = fire;
            --selectionNum;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            img.sprite = fire;
            ++selectionNum;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            img.sprite = ice;
            --selectionNum;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            img.sprite = ice;
            ++selectionNum;
        }
        if (selectionNum == -1)
        {
            selectionNum = 2;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            switch(selectionNum % 3)
            {
                case 0:
                    SceneManager.LoadScene("Level1-1");
                    break;
                case 1:
                    SceneManager.LoadScene("Level_Select");
                    break;
                case 2:
                    SceneManager.LoadScene("Controls");
                    break;
            }
        }
        selector.localPosition = selectLocs[selectionNum % 3];
    }
}
