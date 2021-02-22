using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Text ammoText, fragText, levelText, healthText;
    private static Text ammoTextStatic, fragTextStatic, levelTextStatic, healthTextStatic;


    private void Start()
    {
        ammoTextStatic = ammoText;
        fragTextStatic=fragText;
        levelTextStatic = levelText;
        healthTextStatic = healthText;
    }

    public static void AmmoChanged()
    {
        ammoTextStatic.GetComponent<Text>().text = "AMMO: " + FireScript.ammo.ToString();
    }

    public static void FragChanged()
    {
        fragTextStatic.GetComponent<Text>().text = "FRAGS: " + GrenadeLauncher.ammo.ToString();
    }

    public static void LevelChanged(int level)
    {
        levelTextStatic.GetComponent<Text>().text =level.ToString();
    }

    public static void HealthChanged(int health)
    {
        healthTextStatic.GetComponent<Text>().text = "HP: " + health.ToString();
    }
}
