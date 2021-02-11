using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Text ammoText; private static Text ammoTextStatic;
    public Text levelText; private static Text levelTextStatic;
    public Text healthText; private static Text healthTextStatic;


    private void Start()
    {
        ammoTextStatic = ammoText;
        levelTextStatic = levelText;
        healthTextStatic = healthText;
    }

    public static void AmmoChanged()
    {
        ammoTextStatic.GetComponent<Text>().text = "AMMO: " + FireScript.ammo.ToString();
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
