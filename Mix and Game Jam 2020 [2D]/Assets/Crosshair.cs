using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    public int RotZ;
    public int Multiplier;
    public float MoveSpeed;
    private Rigidbody2D RG2D;

    void Awake() 
    {
        RG2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Rotation");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 Direction = (mousePosition - transform.position).normalized;
        RG2D.velocity = new Vector2(Direction.x * MoveSpeed, Direction.y * MoveSpeed);

        if(Input.GetMouseButton(0)) transform.localScale = new Vector2(1f, 1f);
        else transform.localScale = new Vector2(1.5f, 1.5f);
    }

    IEnumerator Rotation() 
    {
        while(true) 
        {
            if(!Input.GetMouseButton(0)) 
            {
                RotZ++;
                transform.rotation = Quaternion.Euler(0f, 0f, RotZ * Multiplier);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
