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
        platform.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("Fire1"))
        {
            //Toggle the Collider on and off when pressing the L key
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

        if(!transform.GetChild(0).gameObject.activeInHierarchy)
        {
            platform.GetComponent<MeshRenderer>().enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hidden Platform")
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
