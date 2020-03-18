using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillipeFood : MonoBehaviour
{
    private Rigidbody rb;
    public int upForce;
    public int forwardForce;

    public GameObject player;

    public int lifeTime;
    
    public void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Get direction towards player
        player = GameObject.FindWithTag("Player");
        Vector3 dirToPlayer = player.transform.position - this.transform.position;

        // Apply upwards force
        rb.AddForce(transform.up * upForce);
        // Apply forwards force
        rb.AddForce(dirToPlayer * forwardForce);

        // Apply lifetime 
        Destroy(gameObject, lifeTime);
    }


    public void OnCollisionEnter(Collision other)
    {
        // If hits player, damage them
        if (other.gameObject.tag == "Player")
        {
            player.transform.gameObject.SendMessage("playerTakeDamage", GameObject.Find("Fillipe").GetComponent<Fillipe>().AIThrowDamage);
        }
    }

}
