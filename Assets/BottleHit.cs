using UnityEngine;
using System.Collections;

public class BottleHit : MonoBehaviour {

    public Rigidbody bottle;

    // Grenade explodes after a time delay.
    public float fuseTime;

    // Possible projectile script.
    public GameObject explosionPrefab;

    // Use this for initialization
    void Start () {
        // Invoke("Explode", fuseTime);
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.duration);
    }

    void Explode()
    {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.duration);
    }

    // Grenade explodes on impact.
    void OnCollisionEnter(Collision coll)
    {
        Explode();
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Instantiate(explosionPrefab, hit.point, Quaternion.identity);
        }
    }
}
