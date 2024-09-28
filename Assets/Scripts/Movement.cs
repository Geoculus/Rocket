using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    Transform playerTransform;
    AudioSource mainEngineAudioSouce;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] AudioClip maineEngineSound;
    [SerializeField] float thrustForce = 10f;
    [SerializeField] float rotationForce = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        mainEngineAudioSouce = audioSources[0];

    }
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
            
            if(!thrustParticles.isPlaying)
                thrustParticles.Play();
                
            if(!mainEngineAudioSouce.isPlaying)
            {
               mainEngineAudioSouce.Play();
            }
            
        }
        else
        {
            mainEngineAudioSouce.Stop();
            thrustParticles.Stop();
        }
    }

    void ApplyRotation(float rotationForce)
    {
        rb.freezeRotation = true;
        playerTransform.Rotate(Vector3.forward*Time.deltaTime*rotationForce);
        rb.freezeRotation = false;
    }
}
