using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Rigidbody fizik;
    ParticleSystem explosionEffect;
    AudioSource explosionSound;

    float throwForce = 70f, friction = 64f, impactRadius = 7f; //explosionForce = 50f;
    int damage = 100; 
    bool collisionDetector = false, explodeOnce=true;


    void Start()
    {
        fizik = transform.GetComponent<Rigidbody>();
        explosionEffect = GetComponentInChildren<ParticleSystem>();
        explosionSound = GetComponent<AudioSource>();
    }


    void FixedUpdate()
    {
        if (!collisionDetector)
        {
            throwForce -= Time.deltaTime * friction;
            fizik.AddForce(((0.2f*transform.up)+(0.7f*transform.forward))*throwForce);
        }              
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionDetector = true;
        if(explodeOnce)//to prevent more than one explosion effect
        {
            StartCoroutine(Explosion());
            explodeOnce = false;
        }       
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2f);

        transform.GetComponent<MeshRenderer>().enabled=false;
        DestroyNearbyZombies();
        explosionEffect.Play();
        explosionSound.Play();

        yield return new WaitForSeconds(1f);//wait until explosion effect complete

        Destroy(transform.gameObject);
    }

    void DestroyNearbyZombies()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, impactRadius);

        foreach(Collider col in nearbyObjects)
        {
            if(col.gameObject.tag=="zombie")
            {
                col.gameObject.GetComponent<ZombieAI>().health -= damage;
                //col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, impactRadius);
            }           
        }
    }
}
