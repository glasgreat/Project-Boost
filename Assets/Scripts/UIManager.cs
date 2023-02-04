using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text gameOver;
    public TMP_Text newGame;
    public TMP_Text winGame;
    public TMP_Text timeText;
    public TMP_Text points;

    int maxHealth = 5;
    int currentHealth;
    int point;

    private HealthBarScript healthBar;
    private CollisionHandler rocket;
    private GameManager gManager;

    private float timeTaken;
    private float fastestTime = 120.0f;
   
    private string mins;
    private string secs;

    private bool stopTimer = true;
    private bool timerActive = false;

    bool isTextGUIEnabled;


    // Start is called before the first frame update
    void Start()
    {

        scoreText.color = Color.green;
        currentHealth = maxHealth;

        healthBar = GameObject.Find("AtomRocket").GetComponent<HealthBarScript>();

        healthBar.SetMaxHealth(maxHealth);
        
        scoreText.text = "Health: " + maxHealth.ToString();

 
        rocket = GameObject.Find("AtomRocket").GetComponent<CollisionHandler>();

        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

        StartTimer(timerActive);
        
        if (Input.GetKey(KeyCode.C))
        {
            Debug.Log("Key C pressed ");
           
            PlayerPrefs.DeleteKey("FastestTime");
        }

    }

    public void UpdateRocketDamage(int damage)
    {
        //currentHealth = 5;

        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        scoreText.text = "Health: " + currentHealth.ToString();

        Debug.Log("Damage is: " + currentHealth);


        if (currentHealth == 0)
        {

            Debug.Log("=== Current Health ====> " + currentHealth);
     
            maxHealth = 5;
            currentHealth = maxHealth;
            scoreText.text = " ";

            GameOverUI(false);
            StopTimer(false);
            gManager.GameOver();

        }
    }

    public void AddCollectablePoints(int aPoint)
    {

        points.color = Color.yellow;

        point += aPoint;

        points.text = "Collected: " + point.ToString();

    }

    public void GameOverUI(bool isTextGUIEnabled)
    {

        if (!isTextGUIEnabled)
        {

            Debug.Log("!isGUIENabled :: Called");
            gameOver.enabled = true;
            newGame.enabled = true;

        }
        if(isTextGUIEnabled)
        {
            
            ResetGameText();

            isTextGUIEnabled = false;
        }
           
      
    }

    public void ResetGameText()
    {
        
        gameOver.enabled = false;
        newGame.enabled = false;
        winGame.enabled = false;

        // RESET SCORES

        maxHealth = 5;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    public void WinGameUI()
    {
        Debug.Log("Called ==> WinGame");
        winGame.enabled = true;
        newGame.enabled = true;

    }
    
    public void StartTimer(bool timerAct)
    {
       
        timerActive = timerAct;

        if (timerActive)
        {

            timeTaken += Time.deltaTime;
       
            mins = ((int)timeTaken / 60).ToString();
            secs = (timeTaken % 60).ToString("f2");

            fastestTime = PlayerPrefs.GetFloat("FastestTime");

            timeText.color = Color.yellow;

            timeText.text = "Time:  " + mins + ":" + secs + "   Fastest Time: " + fastestTime.ToString("f2");
        }

    }

    public void StopTimer(bool stpTimer)
    {

        //StartTimer(false);
        Debug.Log("StopTimer :: Called: " + timerActive );

        stopTimer = stpTimer;

        //On first Run fastestTime is 0, so timeTaken < fastest time will be false
        //and loop will not run, example fastestTime=5 < timeTaken0
        // On playing game first time, there is no recorded fastestTime
        // hence the loop would not run, so we add 520f seconds as the first fastestTime record.

        if (stopTimer == true)
        {

            if (timeTaken < (fastestTime + 544f))
            {

                fastestTime = timeTaken;

                Debug.Log("Time taken ==>" + timeTaken);

                // Save
                Debug.Log("FastestTime:: ==>" + fastestTime);

                PlayerPrefs.SetFloat("FastestTime", fastestTime);
             
            }

        }
       

    }

    public void ResetTimeUI()
    {
        timeTaken = 0.0f;
    
    }

}
