using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level S;
    public bool stop = false;
    public bool FireFin = false;
    public bool IceFin = false;
    //Ability Indices 
    const int FIRE_PROJ_BUILD_INDEX = 5, ICE_PROJ_BUILD_INDEX = 8;
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

        //Level End If Both Dead
        if (Ice.S.dead && Fire.S.dead && !stop)
        {
            stop = true;
            KillCharacter(Fire.S.gameObject);
        }
    }
    public void KillCharacter(GameObject toDie)
    {
        Destroy(Fire.S.gameObject);
        Destroy(Ice.S.gameObject);
        StartCoroutine(SomeoneDied());
    }
    public IEnumerator SomeoneDied()
    {
        sound.Stop();
        PlaySound("Death");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator Finish()
    {
        Level.S.stop = true;
        Destroy(Fire.S.boxCol);
        Destroy(Ice.S.boxCol);
        Destroy(Fire.S.rb);
        Destroy(Ice.S.rb);
        Fire.S.sa.finished = true;
        Ice.S.sa.finished = true;
        sound.Stop();
        PlaySound("Finish");
        yield return new WaitForSeconds(3f);
        Destroy(Fire.S.sa);
        Destroy(Ice.S.sa);
        Fire.S.sr.sprite = Sprite.Create(Resources.Load("Sprites/FireFlower") as Texture2D, new Rect(0, 0, 1213f, 1213f), new Vector2(0.5f, 0.5f));
        Fire.S.transform.localScale = new Vector3(0.0824f, 0.0824f, 0.0824f);
        Ice.S.sr.sprite = Sprite.Create(Resources.Load("Sprites/IceFlower") as Texture2D, new Rect(0, 0, 1213f, 1213f), new Vector2(0.5f, 0.5f));
        Ice.S.transform.localScale = new Vector3(0.0824f, 0.0824f, 0.0824f);
        Fire.S.isFlower = true;
        Ice.S.isFlower = true;
        PlaySound("Woosh");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void FireFinish()
    {
        FireFin = true;
        if (IceFin)
        {
            StartCoroutine(Finish());
        }
    }
    public void IceFinish()
    {
        IceFin = true;
        if (FireFin)
        {
            StartCoroutine(Finish());
        }
    }

    void SetAbilities()
    {
        //List all possible abilities here
        Fire.S.Abilities["FireProj"] = false;
        Ice.S.Abilities["IceProj"] = false;
        Fire.S.Abilities["FireHug"] = false;
        Ice.S.Abilities["IceHug"] = false;
        //Set abilities to true past the levels they were collected here
        if (SceneManager.GetActiveScene().buildIndex > FIRE_PROJ_BUILD_INDEX)
        {
            Fire.S.Abilities["FireProj"] = true;
        }
        if (SceneManager.GetActiveScene().buildIndex > ICE_PROJ_BUILD_INDEX)
        {
            Ice.S.Abilities["IceProj"] = true;
        }
    }
    public void PlaySound(string name)
    {
        sound.PlayOneShot(Resources.Load("Sounds/" + name) as AudioClip);
    }
}
