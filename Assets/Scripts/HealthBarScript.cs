using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarScript : MonoBehaviour
{

    public Slider slider;
    public Gradient grad;
    public Image fill;

   

    private void Start()
    {
        //DontDestroyOnLoad(this);
    }

    public void SetMaxHealth(int health)
    {
        
        slider.maxValue = health;

        slider.value = health;
        
        
        fill.color = grad.Evaluate(1f);
       
    }
    
    
    
    public void SetHealth(int health)
    {
        
        slider.value = health;

        fill.color = grad.Evaluate(slider.normalizedValue);

    }



}
