using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
    public static Level s;

    public bool FireFin = false;

    public bool IceFin = false;


    public void SomeoneDied()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void FireFinish()
    {
        FireFin = true;
        if (IceFin)
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
    }
    public void IceFinish()
    {
        IceFin = true;
        if (FireFin)
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
    }

    void Awake()
    {
        s = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}
}
