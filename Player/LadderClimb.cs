using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public Transform Player;
    public bool inside = false;
    public float heightFactor = 3.2f;

  //  private int FPSInput = PlayerMove;

    // Start is called before the first frame update
    void Start()
    {
   //     FPSInput = GetComponents<CharacterController>;
    }

    public void OnTriggerEnter(Collider other)
    {
    //    if (Collider.gameObject.tag == "Ladder")
      //  {
     //       FPSInput.enabled = false;
      //      inside = !inside;
     //   }
    }

    public void OnTriggerExit(Collider other)
    {
       // if (Collider.gameObject.tag == "Ladder")
       // {
       //     FPSInput.enabled = true;
       //     inside = !inside;
      //  }
    }

    // Update is called once per frame
    void Update()
    {
        if(inside == true && Input.GetKey("w"))
        {
            Player.transform.position += Vector3.up / heightFactor;
        }
    }
}
