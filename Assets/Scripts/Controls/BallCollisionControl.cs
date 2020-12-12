using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionControl : MonoBehaviour
{
    private AudioSource _audio;
    private CameraMovement _cam;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _cam = FindObjectOfType<CameraMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle")
        {
            if (!Game.isGameover)
            {
                _audio.Play();
                _cam.ShakeIt();
            }

            Game.isGameover = true;
            Game.isStartLevel = false;
            Debug.Log("CARPTI");
        }
        else if (other.transform.tag == "FinishLineTag")
        {
            Game.isLevelPassed = true;          
        }
    }
}
