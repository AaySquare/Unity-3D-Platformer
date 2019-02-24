using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLantern : MonoBehaviour
{
    Collider lanternCollider;
    GameObject platform;
    Renderer platformRenderer;
    Collider platformCollider;

    // Start is called before the first frame update
    void Start()
    {
        lanternCollider = GetComponent<BoxCollider>();
        platform = GameObject.FindWithTag("Hidden Platform");
        platformRenderer = platform.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        platformCollider = platform.transform.GetChild(0).gameObject.GetComponent<Collider>();
        platformRenderer.enabled = false;
        platformCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("Fire3"))
        {
            //Toggle the lantern on and off when pressing the L key
            lanternCollider.enabled = !lanternCollider.enabled;

            if (lanternCollider.enabled)
            {
                transform.GetChild(0).gameObject.GetComponent<Light>().enabled = true;
            }
            else if (!lanternCollider.enabled)
            {
                transform.GetChild(0).gameObject.GetComponent<Light>().enabled = false;
            }
        }

        //If lantern is off, deactivate platform
        if (!transform.GetChild(0).gameObject.GetComponent<Light>().enabled)
        {
            platformRenderer.enabled = false;
            platformCollider.enabled = false;
        }

    }

    //If lantern's collider collides with platform's collider, activate platform
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hidden Platform")
        {
            platformRenderer.enabled = true;
            platformCollider.enabled = true;
        }
    }
}
