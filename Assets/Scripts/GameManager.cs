using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private UIManager myUIManager;

    // By Default unspecified bool value is false!
    bool isGameOver;
    


    // Start is called before the first frame update
    void Start()
    {

        myUIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();

        DontDestroyOnLoad(this);

    }

    // Update is called once per frame
    void Update()
    {

        StartNewGame();
        QuitGamePlay();
       
        CheatSkipToNextLevel();
        SuperCheat();
    }

    public void GameOver()
    {
        //myUIManager.StopTimer(false);
        isGameOver = true;
        Time.timeScale = 0;

    }

    void StartNewGame()
    {
        bool isKeyR = Input.GetKey(KeyCode.R);

        if (isKeyR && isGameOver == true) 
        {
            Debug.Log("===> R Pressed");
     
            Time.timeScale = 1;

            myUIManager.GameOverUI(true);
            myUIManager.UpdateRocketDamage(0);
            myUIManager.ResetTimeUI();

            ReloadLevel();
      
        }   
    }

    void QuitGamePlay()
    {

        bool isEscapeKey = Input.GetKey(KeyCode.Escape);

        if (isEscapeKey)
        {
            Application.Quit();
            Debug.Log("Escape Key Pressed");

        }

    }

    public void ReloadLevel()
    {
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currentSceneIndex);

        myUIManager.ResetGameText();
       
        SceneManager.LoadScene(0);

    }


    public void LoadNextLevel()
    {
       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);

    }

    void CheatSkipToNextLevel()
    {

        bool isKeyL = Input.GetKey(KeyCode.L);

        if (isKeyL)
        {
            Debug.Log("User pressed L key");
            LoadNextLevel();

        }

    }


    void SuperCheat()
    {
        /*
        bool isKeyC = Input.GetKey(KeyCode.C);

        if (isKeyC)
        {
            Debug.Log("Is Key C");

            //isCheatOn = true;
            // Toggle isCheatOn ON AND OFF
            isCheatOn = !isCheatOn;
        }
        */

    }




}
