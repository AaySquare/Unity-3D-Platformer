using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    [Tooltip("Sets the speed at which the camera follows the player")]
    public float CameraFollowSpeed = 100.0f;
    [Tooltip("Clamps the camera's vertical rotation between specfied angle")]
    public float clampAngle = 30.0f;
    [Tooltip("Sets the sensitivity of the camera movement")]
    public float inputSensitivity = 150.0f;
    [Tooltip("Sets the rotation smoothing speed")]
    public float lerpSpeed = 20f;
    [Tooltip("Use rotation smoothing?")]
    public bool lerpRotation = false;

    float mouseX;
    float mouseY;
    float rotationY = 0.0f;
    float rotationX = 0.0f;

    

    // Use this for initialization
    void Start()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotationY = rotation.y;
        rotationX = rotation.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        UpdateCamera();
    }

    void UpdateCamera()
    {
        Quaternion oldRot = transform.rotation;

        mouseX = Input.GetAxis("Mouse X"); 
        mouseY = Input.GetAxis("Mouse Y");

        rotationY += mouseX * inputSensitivity * Time.deltaTime;
        rotationX += mouseY * inputSensitivity * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -clampAngle, clampAngle);


        if(lerpRotation)
            transform.rotation = Quaternion.Lerp(oldRot, Quaternion.Euler(rotationX, rotationY, 0.0f), Time.deltaTime * lerpSpeed);
        if(!lerpRotation)
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0.0f);

        Transform target = player.transform;
        float move = CameraFollowSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.position, move);
    }
}
