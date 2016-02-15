using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Lastlevel : MonoBehaviour {
    public float endTime = 30f;

	// Use this for initialization
	void Start () {
        StartCoroutine(animate());
	}

    IEnumerator animate()
    {
        yield return new WaitForSeconds(endTime);
        SceneManager.LoadScene(0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
