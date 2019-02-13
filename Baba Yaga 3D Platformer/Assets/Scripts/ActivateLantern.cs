using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLantern : MonoBehaviour
{
    Collider m_Collider;
    GameObject platform;

    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<BoxCollider>();
        platform = GameObject.FindWithTag("Hidden Platform");
        platform.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("Fire3"))
        {
            //Toggle the lantern on and off when pressing the L key
            m_Collider.enabled = !m_Collider.enabled;

            if (m_Collider.enabled)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (!m_Collider.enabled)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        //If lantern is off, deactivate platform
        if(!transform.GetChild(0).gameObject.activeInHierarchy)
        {
            platform.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    //If lantern's collider collides with platform's collider, activate platform
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hidden Platform")
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
