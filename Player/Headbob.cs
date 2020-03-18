using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    public CharacterController characterController;
    public Animation anim;
    private bool isMoving;

    private bool left;
    private bool right;

    void CameraAnimations()
    {
        if (characterController.isGrounded == true)
        {
            if(isMoving == true)
            {
                if(left == true)
                {
                    if (!anim.isPlaying)
                    {
                        anim.Play("walkLeft");
                        left = false;
                        right = true;
                    }
                }
                if (right == true)
                {
                    if (!anim.isPlaying)
                    {
                        anim.Play("walkRight");
                        left = true;
                        right = false;
                    }
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        left = true;
        right = true;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if(inputX != 0 || inputY != 0)
        {
            isMoving = true;
        }else if(inputX == 0 && inputY == 0)
        {
            isMoving = false;
        }

        CameraAnimations();
    }
}
