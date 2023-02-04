using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem rocketPowerFlame;
    [SerializeField] ParticleSystem rocketThrust;
    [SerializeField] ParticleSystem rocketThrust1;
    [SerializeField] ParticleSystem rocketThrust2;

    Rigidbody rb;
    AudioSource audioSource;

    Vector3 startPosition;
    Quaternion startRotation;



    // Start is called before the first frame update
    void Start()
    {


        startPosition = transform.position;
        startRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

       
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        
    }

    void ProcessThrust()
    {

        bool isSpace = Input.GetKey(KeyCode.Space);

        rocketPowerFlame.Play();

        if (isSpace) 
        {

            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

        //bool isSpaceUp = Input.GetKeyUp(KeyCode.Space);

    }

    void StartThrusting()
    {

        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!rocketThrust.isPlaying || !rocketThrust1.isPlaying || !rocketThrust2.isPlaying)
        {
            // Play Partical Effects
            rocketThrust.Play();
            rocketThrust1.Play();
            rocketThrust2.Play();

        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        rocketThrust.Stop();
        rocketThrust1.Stop();
        rocketThrust2.Stop();
    }

    void ProcessRotation()
    {
        bool isKeyA = Input.GetKey(KeyCode.A);
        bool iskeyD = Input.GetKey(KeyCode.D);

        if (isKeyA)
        {
            RotateLeft();
        }
        else if (iskeyD)
        {
            RotateRight();
        }
    }


    void RotateLeft()
    {
        ApplyRotation(rotationThrust);

    }


    void RotateRight()
    {
        ApplyRotation(-rotationThrust);

    }


    void ApplyRotation(float rotationThisFrame)
    {

        rb.freezeRotation = true;  // freezing rotation so we can manually rotate

        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        rb.freezeRotation = false;  // unfreezing rotation so the physics system can take over
    }


    public void NewSpawnPosition()
    {

        // Stop Rocket from bouncing off other objects by setting velocity to zero
        rb.velocity = Vector3.zero;
        transform.position = startPosition;
        transform.rotation = startRotation;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    public void NewSpawnLandingPosition()
    {
        rb.velocity = Vector3.zero;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;


    }

    public void FreezePosition()
    {
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        
    }


}