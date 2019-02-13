using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCollision : MonoBehaviour
{
    [Tooltip("Sets the minimum camera distance when there is an object between player and camera")]
    public float minDistane = 1.0f;
    [Tooltip("Sets the default/maximum camera distance from the player")]
    public float maxDistance = 4.0f;
    [Tooltip("Sets how smoothly the camera transitions between maximum distance and minimum distance")]
    public float smooth = 10.0f;

    Vector3 dollyDir;
    private float distance;

    // Start is called before the first frame update
    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredCamPosition = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCamPosition, out hit))
        {
            distance = Mathf.Clamp((hit.distance), minDistane, maxDistance);
        }
        else
            distance = maxDistance;

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
}
