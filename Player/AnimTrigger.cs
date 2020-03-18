using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    public GameObject animatingObject;
    public Animator anim;
    public bool enter;
    public GameObject walkiePickup3;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            enter = true;
            anim.enabled = true;
        }
    }

    void Start()
    {
        enter = false;
        anim.enabled = false;

    }

    public void deleteTrigger()
    {
        walkiePickup3.SetActive(false);
    }
}
