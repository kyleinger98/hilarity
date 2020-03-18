using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger3 : MonoBehaviour
{
    public AudioSource audioclip;
    public GameObject trigger;


    public void Start()
    {
        trigger.SetActive(true);
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            trigger.SetActive(true);
            audioclip.Play();
        }
    }
}
