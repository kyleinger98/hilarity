using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioclip;
    public GameObject trigger;

    public GameObject animatingObject;
    public Animator anim;

    public GameObject walkiepop3;

    public void Start()
    {
        trigger.SetActive(true);
        anim.enabled = false;
    }
    void OnTriggerEnter (Collider collider)
    {
        if (collider.tag == "Player")
        {
            trigger.SetActive(true);
            audioclip.Play();
            anim.enabled = true;
            walkiepop3.SetActive(true);
        }
    }

    void OnTriggerExit (Collider collider)
    {
        if (collider.tag == "Player")
        {
            trigger.SetActive(false);
        }
    }
}
