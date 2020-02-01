using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunMachine : MonoBehaviour
{
    public bool isRunning;
    
    void Start()
    {
        isRunning = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRunning = !isRunning;
        }
    }

    public void SetEnabled(bool isEnabled)
    {
        if (isEnabled == false)
        { 
            isRunning = false;   
        }
        
        enabled = isEnabled;
    }
}
