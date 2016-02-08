using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : MonoBehaviour {

    public List<GameObject> bridges;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Block" || col.gameObject.tag == "Player")
        {
            foreach (GameObject go in bridges)
            {
                go.SendMessage("Activate");
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        foreach (GameObject go in bridges)
        {
            go.SendMessage("Deactivate");
        }
    }
}
