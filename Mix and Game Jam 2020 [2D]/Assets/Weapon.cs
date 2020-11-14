using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform Player;
    public string EquippedWeapon;
    public float Offset;
    public Transform ShotPoint;
    public GameObject Projectile;
    private float TimeBTWShots;
    public float StartTimeBTWShots;

    [Header("Ammo")]
    public int Magazine;
    public int CurrentAmmo;
    public float ReloadDelay;

    void Start() 
    {
        EquippedWeapon = "";

        CurrentAmmo = Magazine;
        StartCoroutine("Reloading");
    }

    // Update is called once per frame
    private void Update()
    {
        if(EquippedWeapon != "") 
        {
            Vector3 Difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float RotZ = Mathf.Atan2(Difference.y, Difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, RotZ + Offset);

            if(TimeBTWShots <= 0) 
            {
                if(CurrentAmmo > 0)  
                {
                    if(Input.GetMouseButton(0)) 
                    {
                        Instantiate(Projectile, ShotPoint.position, transform.rotation);
                        CurrentAmmo--;
                        TimeBTWShots = StartTimeBTWShots;
                    }
                }
            }

            else 
            {
                TimeBTWShots -= Time.deltaTime;
            }

            this.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y);
        }

        else 
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        EquippedWeapon = PlayerPrefs.GetString("EquippedWeapon");

        if(CurrentAmmo > Magazine) 
        {
            CurrentAmmo = Magazine;
        }
        
        if(CurrentAmmo < 0) 
        {
            CurrentAmmo = 0;
        }

        PlayerPrefs.SetInt("CurrentAmmo", CurrentAmmo);
        PlayerPrefs.SetInt("Magazine", Magazine);
    }

    IEnumerator Reloading() 
    {
        while(true) 
        {
            if(EquippedWeapon != "")
            {
                if(Input.GetKeyDown("r")) 
                {
                    yield return new WaitForSeconds(ReloadDelay);
                    CurrentAmmo = Magazine;
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
