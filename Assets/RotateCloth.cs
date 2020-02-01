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
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(runMachine.transform.position, Vector3.back, speed * Time.deltaTime);    
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(runMachine.transform.position, Vector3.forward, speed * Time.deltaTime);
        }
    }
}
