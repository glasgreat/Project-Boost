using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSrc : MonoBehaviour
{
    AudioSource audioSource;
  

    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;




    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(transform.gameObject);

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCrashSound()
    {
        audioSource.PlayOneShot(crash);
    }

    public void PlaySuccessSound()
    {
        audioSource.PlayOneShot(success);
    }

    

}
