using UnityEngine;
using System.Collections;

public class TraversableTile : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Fire.S.colliding_tiles[GetInstanceID()] = false;
        Ice.S.colliding_tiles[GetInstanceID()] = false;
	}

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player") {
            coll.gameObject.GetComponent<Character>().colliding_tiles[GetInstanceID()] = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player") {
            coll.gameObject.GetComponent<Character>().colliding_tiles[GetInstanceID()] = false;
        }
    }
}
