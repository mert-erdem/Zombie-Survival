using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    private float delay=2;
    private float delayDecreaser = 0;

    public GameObject zombie;

    private Vector3 newPosition;

    private int hordeSize;
    private int currentHordeSize;
    public static int aliveZombies;


    private void Start()
    {
        hordeSize = 5;
        currentHordeSize = 0;
        aliveZombies = hordeSize;
    }
    void Update()
    {      
        if(currentHordeSize!=hordeSize)
        {
            if (delay < Time.timeSinceLevelLoad)
            {
                newPosition = new Vector3(-15.03345f, 2.287f, Random.Range(-5f, 8f));
                Instantiate(zombie, newPosition,
                    Quaternion.Euler(zombie.transform.rotation.x, zombie.transform.rotation.y, zombie.transform.rotation.z));

                currentHordeSize++;

                delay += (3 - delayDecreaser);
            }
        }
        
        if (aliveZombies==0)
        {
            currentHordeSize = 0;
            //New Level is preparing;
            hordeSize *=3;
            aliveZombies = hordeSize;
            delayDecreaser += 0.01f;

            LevelManager.level++;
            LevelManager.LevelUI();//UI's update with new level

            this.enabled=false;
        }
    }

    
}
