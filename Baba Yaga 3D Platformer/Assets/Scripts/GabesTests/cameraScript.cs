using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject player;

    Quaternion targetRot;

    Vector3 offset;

    void Start() {
        //offset = transform.position - player.transform.position;
        offset = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 7.0f);
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        /*
        //transform.localPosition = player.transform.localPosition + offset;
        this.transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X"));

        //transform.localPosition = player.transform.localPosition + offset;*/

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X"), Vector3.up) * offset;
        transform.position = player.transform.position + offset;
        Vector3 rotTarget = player.transform.position;
        transform.LookAt(new Vector3(rotTarget.x, transform.position.y, rotTarget.z));

    }
    
}
