using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text AmmoDisplayer;
    public int Magazine;
    public int CurrentAmmo;

    [Header("Food & Thirst")]
    public int Hunger;
    public int Thirst;
    public GameObject LowHunger;
    public GameObject LowThirst;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;

        CurrentAmmo = PlayerPrefs.GetInt("CurrentAmmo");
        Magazine = PlayerPrefs.GetInt("Magazine");

        AmmoDisplayer.text = CurrentAmmo.ToString() + " | " + Magazine.ToString();

        //Food & Thirst
        Hunger = PlayerPrefs.GetInt("Hunger");
        Thirst = PlayerPrefs.GetInt("Thirst");

        if(Hunger < 10) 
        {
            LowHunger.SetActive(true);
        }

        else 
        {
            LowHunger.SetActive(false);
        }

        if(Thirst < 10) 
        {
            LowThirst.SetActive(true);
        }

        else 
        {
            LowThirst.SetActive(false);
        }
    }
}
