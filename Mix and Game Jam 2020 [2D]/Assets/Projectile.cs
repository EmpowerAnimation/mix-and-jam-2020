using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float Speed;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);

        if(this.transform.position.x > 1000 || this.transform.position.x < -1000 || this.transform.position.y > 1000 || this.transform.position.y < -1000) 
        {
            Destroy(gameObject);
        }
    }
}
