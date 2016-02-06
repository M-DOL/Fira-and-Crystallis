using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
    public static Level S;

    public bool FireFin = false;
    public bool IceFin = false;
    AudioSource sound;
    void Awake()
    {
        S = this;
    }

    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        SetAbilities();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public IEnumerator SomeoneDied()
    {
        sound.Stop();
        PlaySound("Death");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FireFinish()
    {
        FireFin = true;
        if (IceFin)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void IceFinish()
    {
        IceFin = true;
        if (FireFin)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void SetAbilities()
    {
        //List all possible abilities here
        Fire.S.Abilities["FireProj"] = false;
        Ice.S.Abilities["IceProj"] = false;
        //Set abilities to true past the levels they were collected here
        if (SceneManager.GetActiveScene().buildIndex > SceneManager.GetSceneByName("Level1-4").buildIndex)
        {
            Fire.S.Abilities["FireProj"] = true;
        }
    }
    void PlaySound(string name)
    {
        sound.PlayOneShot(Resources.Load("Sounds/" + name) as AudioClip);
    }
}
