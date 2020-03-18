using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnstyleMove : MonoBehaviour
{
    public Camera camera;
    public float ray_Range = 2f;
    public KeyCode interact;

    public GameObject staticTurnstyle;
    public GameObject animatedTurnstyle;
    public GameObject leaveArea;

    public GameObject turnstyleTimeline;
    public GameObject turnstyleCamera;
    public GameObject levelFade;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        staticTurnstyle.SetActive(false);
        animatedTurnstyle.SetActive(false);
        leaveArea.SetActive(false);

        turnstyleCamera.SetActive(false);
        turnstyleTimeline.SetActive(false);
        levelFade.SetActive(false);

        player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray_Cast = camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit ray_Hit;

        if (Physics.Raycast(ray_Cast, out ray_Hit, ray_Range))
        {
            if (ray_Hit.collider.tag == "Gate")
            {

                leaveArea.SetActive(true);

                if (Input.GetKeyDown(interact))
                {
                    staticTurnstyle.SetActive(false);
                    animatedTurnstyle.SetActive(true);

                    turnstyleCamera.SetActive(true);
                    turnstyleTimeline.SetActive(true);
                    levelFade.SetActive(true);

                    player.SetActive(false);

                }
            }

        }
        else
        {
            leaveArea.SetActive(false);
        }

    }

}


