using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloth : MonoBehaviour
{
    [SerializeField] private RunMachine runMachine;
    [SerializeField] private float speed;
    
    void Start()
    {
    }

    void Update()
    {
        if (runMachine.isRunning)
        {
            transform.position += Vector3.up * (speed * Time.deltaTime);
        }
    }
}
