using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    Transform playerTransform;
    [SerializeField] float thrustForce = 10f;
    [SerializeField] float rotationForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessRotation();
        ProcessThrust();
    }


    void ProcessRotation() 
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
           ApplyRotation(rotationForce);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-rotationForce);
        }
        
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(new Vector3(0,thrustForce*Time.deltaTime,0));
        }
    }

    void ApplyRotation(float rotationForce)
    {
        rb.freezeRotation = true;
        playerTransform.Rotate(Vector3.forward*Time.deltaTime*rotationForce);
        rb.freezeRotation = false;
    }
}
