using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    bool onGround = false;
    bool doubleJumpOK = false;
    bool dashOK = true;

    public float jumpStrength;
    public float dashStrength;
    public float moveSpeed;
    public float turnSpeed;
    public float interpolation;

    Collider coll;
    Rigidbody rb;

    Transform camTransform;

    private float horiInput;
    private float vertInput;

    private float crntHoriM;
    private float crntVertM;

    Vector3 direction;
    float directionLength;

    Vector3 crntDirection;



    // Start is called before the first frame update
    void Start()
    {
        //Grab the components we need
        camTransform = Camera.main.transform;
        coll = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        #region flat Movement
        vertInput = Input.GetAxis("Vertical"); //Save our Inputs
        horiInput = Input.GetAxis("Horizontal");

        crntVertM = Mathf.Lerp(crntVertM, vertInput, Time.deltaTime * interpolation); //technically we're lerping twice unless we use GetAxisRaw, but this kinda feels good
        crntHoriM = Mathf.Lerp(crntHoriM, horiInput, Time.deltaTime * interpolation);


        direction = camTransform.forward * crntVertM + camTransform.right * crntHoriM; //set direction based on camera as well
        directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;
        Quaternion oldRot = transform.rotation;

        Debug.DrawRay(transform.position, transform.localPosition * 3, Color.green); //Current direction
        if (direction != Vector3.zero) {

            crntDirection = Vector3.Slerp(crntDirection, direction, Time.deltaTime * interpolation);
            crntDirection.y = 0;
            transform.position += crntDirection * moveSpeed * Time.deltaTime; //the actual moving
            transform.rotation = Quaternion.Lerp(oldRot, Quaternion.LookRotation(direction), Time.deltaTime * turnSpeed); //Rotating character towards direction of travel. Lerp stops it snapping
           
        }

        #endregion
        


        #region Jumps
        if (Input.GetButtonDown("Jump")){


            if (onGround == false && doubleJumpOK == true)
            {
                Vector3 crntVelo = rb.velocity;
                crntVelo.y = 0;
                rb.velocity = Vector3.zero; //Important to zero out the velocity first. Addforce will otherwise be deducted from the downwards velocity
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.VelocityChange);
                doubleJumpOK = false;
            }
            if (onGround == true) {
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.VelocityChange);
                doubleJumpOK = true;
                onGround = false;
            }
            
        }
        if (Input.GetButtonDown("Fire1")) {
            if (onGround == false && dashOK == true)
            {
                rb.AddRelativeForce(new Vector3(0, 0.25f, 1) * dashStrength, ForceMode.VelocityChange);
                dashOK = false;
            }
        }
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint conP = collision.GetContact(0);
        if (Vector3.Dot(conP.normal, Vector3.up) > 0.5f)
        {
            onGround = true;
            doubleJumpOK = false;
            dashOK = true;
        }
        else {
            onGround = false;
            doubleJumpOK = true;
        }
    }
}
