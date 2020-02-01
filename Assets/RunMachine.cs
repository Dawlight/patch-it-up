using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunMachine : MonoBehaviour
{
    public bool isRunning;
    public float lerpTime;
    public bool hasCompletedCycle;
    private float _timeToReverse;
    private float _currentNeedleTime;
    
    void Start()
    {
        isRunning = false;
        _timeToReverse = 0.5f;
        _currentNeedleTime = 0;
        lerpTime = 0;
        hasCompletedCycle = true;
    }

    void Update()
    {
        isRunning = Input.GetKey(KeyCode.Space) || hasCompletedCycle == false;

        if (isRunning)
        {
            hasCompletedCycle = false;
            
            _currentNeedleTime += Time.deltaTime;
            lerpTime = _currentNeedleTime / _timeToReverse;

            if (_currentNeedleTime >= _timeToReverse)
            {
                _currentNeedleTime = 0;
                lerpTime = 0;
                hasCompletedCycle = true;
            }
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
