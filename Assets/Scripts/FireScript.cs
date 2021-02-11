using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireScript : MonoBehaviour
{
    private float delay;
    public static int ammo;

    private int damage = 40;
    private float range = 100f;

    public Camera fireCam;
    public ParticleSystem fireEffect;
    public GameObject impactEffect;

    private AudioSource fireSound;

    private List<GameObject> impactS;

    public Text ammoText;

    private void Start()
    {
        delay = 0f;//a limit so that the player cannot fire the weapon repeatedly;
        ammo = 50;
        impactS = new List<GameObject>();
        fireSound = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && delay<Time.timeSinceLevelLoad && ammo!=0)
        {
            Shoot();
            UI.AmmoChanged();//changing ammo ui
            delay += 1f;
        }      
    }

    public void Shoot()
    {
        ammo--;

        RaycastHit hit;

        fireEffect.Play();
        fireSound.Play();

        ClearTheImpacts();//clearing the unnecessary impact effects

        if (Physics.Raycast(fireCam.transform.position, fireCam.transform.forward, out hit, range))
        {
            GameObject hedef;
            
            hedef = hit.transform.gameObject;
            
            if(hedef.tag=="zombie")//vurulan hedef zombi ise
            {
                hedef.GetComponent<ZombieAI>().health -= damage;
                impactS.Add(Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)));//impact effect
                //hit.transform.gameObject.GetComponent<AudioSource>().Play();
            }
            else
            {
                impactS.Add(Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)));//impact effect
                //hit.transform.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }

    void ClearTheImpacts()
    {
        foreach(GameObject o in impactS)
        {
            Destroy(o);
        }
    }
        
}
