using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour
{
    public GameObject Disables;

        public void Activate()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        Disables.SetActive(false);
    }

    public void Deactivate()
    {
        Disables.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
