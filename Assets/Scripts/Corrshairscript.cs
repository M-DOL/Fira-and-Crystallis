using UnityEngine;
using System.Collections;

public class Corrshairscript : MonoBehaviour {
    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(animate());
	}

    IEnumerator animate()
    {
        for(int c = 0; c < 3; ++c)
        {
            yield return new WaitForSeconds(0.1f);
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
        }
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
