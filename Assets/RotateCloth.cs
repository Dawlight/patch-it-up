using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCloth : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private RunMachine runMachine; 
    
    void Start()
    {
    }

    void Update()
    {
        var currentSpeed = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentSpeed = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentSpeed = -1;
        }

        currentSpeed += Input.mouseScrollDelta.y - Input.mouseScrollDelta.x;
        currentSpeed *= speed;
        transform.RotateAround(runMachine.transform.position, Vector3.forward, currentSpeed * Time.deltaTime);
    }
}
