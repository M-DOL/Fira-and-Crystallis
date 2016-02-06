using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
    public ParticleSystem p;
	void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 5f);  
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        Level.S.PlaySound("New Ability");
        Destroy(p);
        Destroy(gameObject);
        Fire.S.Abilities["FireProj"] = true;
    }
}
