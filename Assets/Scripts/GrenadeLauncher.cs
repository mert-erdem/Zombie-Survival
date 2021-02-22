using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    public GameObject grenade, grenadeLocation;
    public static int ammo=4;
    AudioSource launchSound;

    void Start()
    {
        launchSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ammo!=0)
        {
            launchSound.Play();
            Instantiate(grenade, grenadeLocation.transform.position, grenadeLocation.transform.rotation);
            ammo--; UI.FragChanged();
        }
    }
}
