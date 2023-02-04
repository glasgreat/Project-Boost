using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{


    [SerializeField] float levelLoadDelay = 2f;
    
    [SerializeField] ParticleSystem successPartical;
    [SerializeField] ParticleSystem crashPartical;

    AudioSrc audioSource;

    bool isLoadingNextLevel = false;
    bool isTransitioning;
    bool canDamage;

    private GameManager gManager;
    private UIManager myUIManager;
    private Movement move;


    public int currentHealth;
    public int damage;

    

    void Start() 
    {

       
        audioSource = GameObject.Find("AudioSource"). GetComponent<AudioSrc>();

        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        move = GetComponent<Movement>();

        myUIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();

        myUIManager.StartTimer(true);


    }

    private void Update()
    {
        
    }

    // OnTrigger passing through a game object !
    private void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.tag)
        {
            case "Gold":
                Debug.Log("This thing is Gold!");
                AddPoints();
                break;

            default:

                break;

        }


    }

    // On collison bumping into a game object!
    void OnCollisionEnter(Collision other) 
    {
        //if (isTransitioning) { return; }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                //Debug.Log("This thing is friendly");
                break;

            case "Finished":
                StartSuccessSequence();
                break;

            case "Winner":
                Debug.Log("==> OTHER: " + other);
                isLoadingNextLevel = true;
                StartSuccessSequence();
                RocketReachedWinPad();
                break;

            default:

                RocketTakeDamage(1);
                StartCrashSequence();
                
                break;
        }
    }

    void AddPoints()
    {
        myUIManager.AddCollectablePoints(1);

    }

    void StartCrashSequence()
    {

        audioSource.PlayCrashSound();

        crashPartical.Play();

        FreezePos();

        //Stop timeing fastest time!
        myUIManager.StopTimer(false);

    }

    void FreezePos()
    {
        move.FreezePosition();

        Invoke("SpawnPostition", levelLoadDelay);

    }



    void SpawnPostition()
    {
       // Back to launchPad on crash
       move.NewSpawnPosition();

    }

    void StartSuccessSequence()
    {

        audioSource.PlaySuccessSound();

        successPartical.Play();

       
        SpawnLandingPad();

        // <==
        if (!isLoadingNextLevel)
        {
            gManager.LoadNextLevel();
            isLoadingNextLevel = false;
        }

    }


    void SpawnLandingPad()
    {
        move.NewSpawnLandingPosition();

    }
   
    void RocketTakeDamage(int damage)
    {

            Debug.Log("Coll-Damage==> " + damage);

            currentHealth -= damage;

            myUIManager.UpdateRocketDamage(damage);
            
    }

    void RocketReachedWinPad()
    {
        Debug.Log("RocketReachedWinPad");

        myUIManager.WinGameUI();
        gManager.GameOver();
        myUIManager.StopTimer(true);
       
    }


}
