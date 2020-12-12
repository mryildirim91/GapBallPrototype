using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    Rigidbody rbBall;
    [SerializeField] float m_Speed = 1.5f;
    Vector3 m_EulerAngleVelocity;
    void Start()
    {
        rbBall = transform.gameObject.GetComponent<Rigidbody>();
        m_EulerAngleVelocity = new Vector3(100, 0, 0);
    }

    void Update()
    {
        if (!Game.isGameover && Game.isStartLevel && !Game.isLevelPassed)
        {
            MoveForward();
            Roll();
        }
        else
        {
            rbBall.velocity = new Vector3(0, 0, 0);
        }
        
                
    }

    

    private void Roll()
    {
        //transform.Rotate(Vector3.right * 40*Time.deltaTime);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        rbBall.MoveRotation(rbBall.rotation * deltaRotation);
    }

    private void MoveForward()
    {
        rbBall.velocity = new Vector3(0,0,1) * m_Speed;
    }
}
