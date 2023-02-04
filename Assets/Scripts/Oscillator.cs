using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;

    // [Range(0,1)] creates a slider for values 0 to 1 in the inspector!
    [SerializeField] [Range(0,1)] float movementFactor;

    [SerializeField] float period = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

        startingPosition = transform.position;
        Debug.Log("Starting Position: " + startingPosition);

    }

    // Update is called once per frame
    void Update()
    {
        // period cant be NaN ( divided by 0 )
        // Cycles growing over time ( not wise to use == to compare float value )
        // so instead of if(period <=0) we are using if(period <= Mathf.Epsilon)

        if(period <= Mathf.Epsilon) { return; } 

        // Cycles growing over time
        float cycles = Time.time / period;

        // Tau is a measure of circle radius placed on a circle's circumferance!
        // so there is 6.28 Tau in the circumference of a circle.

        const float tau = Mathf.PI * 2;

        // going from -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau);
        //Debug.Log("RawSinWave =" + rawSinWave);

        //Recalculated to go from 0 - 1
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        
        transform.position = startingPosition + offset;

        transform.Translate(offset * rawSinWave);

       

    }
}
