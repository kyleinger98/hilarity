using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger2 : MonoBehaviour
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

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            audioclip.Stop();
        }
    }
}
