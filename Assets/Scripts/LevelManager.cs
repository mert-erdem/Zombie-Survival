using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject zombieGenerator;
    public static int level=1;


    private void Start()
    {
        LevelUI();
    }

    void Update()
    {
        if(zombieGenerator.GetComponent<ZombieGenerator>().enabled==false)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4);
        
        zombieGenerator.GetComponent<ZombieGenerator>().enabled = true;
    }

    public static void LevelUI()
    {      
        UI.LevelChanged(level);
    }
}
