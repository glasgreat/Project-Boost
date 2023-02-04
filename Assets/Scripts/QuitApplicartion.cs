using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplicartion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        QuitGamePlay();
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


}
