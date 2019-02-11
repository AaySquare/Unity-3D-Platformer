using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    bool onGround = false;
    bool doubleJumpOK = false;

    public float jumpStrength;
    public float moveSpeed;
    public float turnSpeed;

    Collider coll;
    Rigidbody rb;

    Transform camTransform;

    private float horiInput;
    private float vertInput;

    private float crntHoriM;
    private float crntVertM;

    public float interpolation;

    Vector3 direction;
    float directionLength;

    Vector3 crntDirection;

    public GameObject rotator;


    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;
        coll = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        #region flat Movement
        vertInput = Input.GetAxis("Vertical");
        horiInput = Input.GetAxis("Horizontal");

        crntVertM = Mathf.Lerp(crntVertM, vertInput, Time.deltaTime * interpolation);
        crntHoriM = Mathf.Lerp(crntHoriM, horiInput, Time.deltaTime * interpolation);


        direction = camTransform.forward * crntVertM + camTransform.right * crntHoriM;
        directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        Vector3 localRot = new Vector3(rotator.transform.position.x, this.transform.position.y, rotator.transform.position.z);
        rb.transform.LookAt(localRot);

        if (direction != Vector3.zero) {

            crntDirection = Vector3.Slerp(crntDirection, direction, Time.deltaTime * interpolation);
            crntDirection.y = 0;
            transform.position += crntDirection * moveSpeed * Time.deltaTime;


            //rb.transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
            
            
        }

        #endregion
        


        #region Jumps
        //checkGrounding();
        if (Input.GetKeyDown(KeyCode.Space)){
            
            if (onGround == true) {
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
                doubleJumpOK = true;
            }
            if (onGround == false && doubleJumpOK == true) {
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
                doubleJumpOK = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            rb.AddForce(new Vector3(0,0, transform.localPosition.z)  * jumpStrength, ForceMode.Impulse);
        }
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint conP = collision.GetContact(0);
        if (Vector3.Dot(conP.normal, Vector3.up) > 0.5f) {
            onGround = true;
            doubleJumpOK = false;
            Debug.Log("grounded");
        }
    }
}
