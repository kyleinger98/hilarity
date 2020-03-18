using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject player;

    public void Start()
    {
        player.SetActive(false);
    }
    public void DeleteCamera()
    {
        Destroy(gameObject);
        player.SetActive(true);
    }

}
