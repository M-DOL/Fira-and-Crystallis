using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
    public static Level S;
    public bool stop = false;
    public bool FireFin = false;
    public bool IceFin = false;
    //Ability Indices 
    const int FIRE_PROJ_BUILD_INDEX = 3, ICE_PROJ_BUILD_INDEX = 6;
    public AudioSource sound;
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
    public void KillCharacter(GameObject toDie)
    {
        Destroy(toDie);
        StartCoroutine(SomeoneDied());
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
        Ice.S.Abilities["IceBlock"] = false;
        //Set abilities to true past the levels they were collected here
        if (SceneManager.GetActiveScene().buildIndex > FIRE_PROJ_BUILD_INDEX)
        {
            Fire.S.Abilities["FireProj"] = true;
        }
        if (SceneManager.GetActiveScene().buildIndex > ICE_PROJ_BUILD_INDEX)
        {
            Ice.S.Abilities["IceProj"] = true;
        }
        /*if (SceneManager.GetActiveScene().buildIndex > SceneManager.GetSceneByName("Level2-1").buildIndex)
        {
            Ice.S.Abilities["IceBlock"] = true;
        }*/
    }
    public void PlaySound(string name)
    {
        sound.PlayOneShot(Resources.Load("Sounds/" + name) as AudioClip);
    }
}
