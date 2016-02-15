using UnityEngine;
using System.Collections;

public class PowerupGET : MonoBehaviour
{
    public bool isFire = true;
    public string PowerUpName = "FireProj";
    public ParticleSystem p;
    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate() {
        transform.Rotate(Vector3.up, 5f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Character>().Abilities[PowerUpName] = true;
        StartCoroutine(Pickup());
    }

    IEnumerator Pickup()
    {
        Level.S.stop = true;
        Level.S.sound.Stop();
        Level.S.PlaySound("New Ability");
        yield return new WaitForSeconds(3f);
        Level.S.sound.Play();
        Level.S.stop = false;
        Destroy(p);
        Destroy(gameObject);
    }
}
