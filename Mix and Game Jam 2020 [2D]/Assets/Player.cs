using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [Header("General")]
    public int Health;
    public int Hunger;
    public int Thirst;
    public float HungerDelay;
    public float ThirstDelay;
    public float FoodHealthDelay;
    public float WaterHeathDelay;
    public Slider HealthBar;
    public Slider HungerBar;
    public Slider ThirstBar;

    [Header("Movement")]
    public float MoveSpeed;
    public float JumpForce;
    private bool Jumping;
    private Rigidbody2D RG2D;
    
    [Header("Weapons")]
    public string EquippedWeapon;
    
    [Header("Raycast")]
    public float Distance;
    public LayerMask WhatIsWeapon;

    // Start is called before the first frame update
    void Start()
    {
        //General
        Health = 100;
        Hunger = 100;
        Thirst = 100;

        //Components
        RG2D = GetComponent<Rigidbody2D>();        

        //Coroutines
        StartCoroutine("HungerDecrease");
        StartCoroutine("ThirstDecrease");
        StartCoroutine("HungerEffect");
        StartCoroutine("ThirstEffect");

        //Weapon
        EquippedWeapon = "";
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float MovX = Input.GetAxisRaw("Horizontal");
        if(MovX != 0)
        {
            RG2D.velocity = new Vector2(MoveSpeed * MovX, RG2D.velocity.y);
        }
        if(MovX == 0) 
        {
            RG2D.velocity = new Vector2(0f, RG2D.velocity.y);
        }


        if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w")) && !Jumping) 
        {
            RG2D.velocity = new Vector2(RG2D.velocity.x, JumpForce);
            Jumping = true;
        }

        //Health
        if(Health <= 0) 
        {
            Destroy(gameObject);
        }

        //UI
        HealthBar.value = Health;
        HungerBar.value = Hunger;
        ThirstBar.value = Thirst;

        PlayerPrefs.SetInt("Hunger", Hunger);
        PlayerPrefs.SetInt("Thirst", Thirst);

        if(Hunger <= 0) 
        {
            Hunger = 0;
        }

        if(Thirst <= 0) 
        {
            Thirst = 0;
        }

        //Weapon
        PlayerPrefs.SetString("EquippedWeapon", EquippedWeapon);

        //Raycast
        RaycastHit2D WeaponInfo = Physics2D.Raycast(transform.position, Vector2.down, Distance, WhatIsWeapon);
        if(WeaponInfo.collider != null) 
        {
            if(WeaponInfo.collider.CompareTag("AK47"))
            {
                if(Input.GetKey("e")) 
                {
                    EquippedWeapon = "AK47";
                }
            }
        }

        if(Input.GetKey("q")) 
        {
            EquippedWeapon = "";
        }
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        Jumping = false;
    }

    IEnumerator HungerDecrease() 
    {
        while(true) 
        {
            if(Hunger > 0) 
            {
                Hunger--;
                yield return new WaitForSeconds(HungerDelay);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ThirstDecrease() 
    {
        while(true) 
        {
            if(Thirst > 0) 
            {
                Thirst--;
                yield return new WaitForSeconds(ThirstDelay);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator HungerEffect() 
    {
        while(true) 
        {
            if(Hunger <= 0) 
            {
                Health--;
                yield return new WaitForSeconds(FoodHealthDelay);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    IEnumerator ThirstEffect() 
    {
        while(true) 
        {
            if(Thirst <= 0) 
            {
                Health--;
                yield return new WaitForSeconds(WaterHeathDelay);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
