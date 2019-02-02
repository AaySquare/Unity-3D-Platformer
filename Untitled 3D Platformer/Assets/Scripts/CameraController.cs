using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    private Vector3 offset;

    public float rotateSpeed;

    public Transform pivot;

    void Start()
    {
        offset = transform.position - target.position;

        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;
        pivot.transform.parent = null;

        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        pivot.transform.position = target.transform.position;

        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        float desiredYAngle = pivot.eulerAngles.y;
        //float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
        transform.position = target.position + (rotation * offset);

        transform.LookAt(target.position);
    }
}
