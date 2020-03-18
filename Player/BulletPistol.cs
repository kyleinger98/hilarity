using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPistol : MonoBehaviour
{
    // Variables 
    private Rigidbody rb;
    private PlayerMove playerMove;
    private WeaponWheel weaponWheel;
    public int bulletDamage;
    private Vector3 direction;
    private bool firstTime = false;
    public float bulletForce;
    
    

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerMove>();
        weaponWheel = GetComponent<WeaponWheel>();
    }




    //public void Update()
    //{
    //    // Apply force
    //    //BulletForce();  
    //}


    public void SetDirection(Vector3 dir)
    {
        direction = dir;
        firstTime = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        // If bullet hits anything
        // Do not collide with other bullets
        if (other.tag != ("AI") && other.tag != ("Bullet"))
        {
            // Delete bullet
            Destroy(gameObject);
            Debug.Log("Not hit AI");
        }

        // Damage enemies
        if (other.tag == ("AI"))
        {
            other.gameObject.SendMessage("TakeDamage", bulletDamage);
            Destroy(gameObject);
            Debug.Log("Hit AI");
            //Debug.Log("TakingDamage");
        }   
    }

    void FixedUpdate()
    {
        if (firstTime)
        {
            rb.AddForce(direction * bulletForce);
            firstTime = false;
        }    
    }

}
