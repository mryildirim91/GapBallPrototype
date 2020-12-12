using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMoveForward : MonoBehaviour
{
    Rigidbody rbHole;
    [SerializeField] float m_Speed = 1.5f;
    void Start()
    {
        rbHole = transform.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!Game.isGameover && Game.isStartLevel && !Game.isLevelPassed)
        {
            MoveForward();
        }
        else
        {
            rbHole.velocity = new Vector3(0, 0, 0);
        }

    }

    private void MoveForward()
    {
        rbHole.velocity = transform.forward * m_Speed;
    }
}
