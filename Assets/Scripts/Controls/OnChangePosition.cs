using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class OnChangePosition : MonoBehaviour
{
    [SerializeField] GameObject BallStartPoint;
    [SerializeField] GameObject HoleStartPoint;
    [SerializeField] GameObject Ball;
    [SerializeField] PolygonCollider2D hole2dCollider;
    [SerializeField] PolygonCollider2D ground2dCollider;
    [SerializeField] MeshCollider GeneratedMeshColldier;
    [SerializeField] Collider GroundCollider;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject[] Levels;
    [Space]
    [SerializeField] float initialScale = 0.5f;
    [SerializeField] float moveSpeed = 15f;
    Mesh GeneratedMesh;
    float x, y;
    Vector3 touch, targetPos;

    //public void Move(BaseEventData myEvent)
    //{
    //    if (((PointerEventData)myEvent).pointerCurrentRaycast.isValid)
    //    {
    //        transform.position = ((PointerEventData)myEvent).pointerCurrentRaycast.worldPosition;
    //    }

    //}

    private void Start()
    {
        GameObject[] AllGameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (var gameobject in AllGameObjects)
        {
            if (gameobject.layer == LayerMask.NameToLayer("Obstacles"))
            {
                Physics.IgnoreCollision(gameobject.GetComponent<Collider>(), GeneratedMeshColldier, true);
            }
        }
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("WhichLevel") == null)
        {
            PlayerPrefs.SetInt("WhichLevel", 0);
        }
        LevelControl();
        Game.isGameover = false;
        Game.isMoving = false;
        Game.isStartLevel = false;
        Game.isLevelPassed = false;
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
        StartPanel.SetActive(true);
        Debug.Log(PlayerPrefs.GetInt("WhichLevel"));
    }

    private void LevelControl()
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            if(i==PlayerPrefs.GetInt("WhichLevel"))
            {
                Levels[i].gameObject.SetActive(true);
            }
            else
            {
                Levels[i].gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (transform.hasChanged == true)
        {
            transform.hasChanged = false;
            hole2dCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            hole2dCollider.transform.localScale = transform.localScale * initialScale;
            createHole2D();
            create3DMeshCollider();
        }
        
    }
    
    private void Update()
    {
        if(!Game.isGameover && Game.isStartLevel && !Game.isLevelPassed)
        {
            Game.isMoving = Input.GetMouseButton(0);
            if (!Game.isGameover && Game.isMoving)
            {
                MoveHole();

            }
        }
        GameOver();
        LevelPassed();
        
        
    }

    private void LevelPassed()
    {
        if (Game.isLevelPassed)
        {
            
            WinPanel.SetActive(true);
            LosePanel.SetActive(false);
            StartPanel.SetActive(false);
        }
       

    }

    private void GameOver()
    {
        if (Game.isGameover)
        {
            WinPanel.SetActive(false);
            LosePanel.SetActive(true);
            StartPanel.SetActive(false);
        }
    }

    private void MoveHole()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp(transform.position, 
            transform.position + new Vector3(x, 0f, y), moveSpeed * Time.deltaTime);

        transform.position = touch;

    }

    private void createHole2D()
    {
        Vector2[] PointPositions = hole2dCollider.GetPath(0);

        for(int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = hole2dCollider.transform.TransformPoint(PointPositions[i]);
        }
        ground2dCollider.pathCount = 2;
        ground2dCollider.SetPath(1, PointPositions);
    }

    private void create3DMeshCollider()
    {
        if (GeneratedMesh != null) { Destroy(GeneratedMesh); }
        GeneratedMesh = ground2dCollider.CreateMesh(true, true);
        GeneratedMeshColldier.sharedMesh = GeneratedMesh;
    }
    public void OnClickStart()
    {
        Game.isStartLevel = true;
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
        StartPanel.SetActive(false);

    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene("Game");
        //Ball.transform.position = BallStartPoint.transform.position;
        //transform.position = HoleStartPoint.transform.position;

        //WinPanel.SetActive(false);
        //LosePanel.SetActive(false);
        //StartPanel.SetActive(true);
    }
    public void OnClickContinue()
    {
        PlayerPrefs.SetInt("WhichLevel", PlayerPrefs.GetInt("WhichLevel") + 1);
        SceneManager.LoadScene("Game");
    }
    private void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(other,GroundCollider,true);
        Physics.IgnoreCollision(other, GeneratedMeshColldier, false);
    }
    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other, GroundCollider, false);
        Physics.IgnoreCollision(other, GeneratedMeshColldier, true);
    }
}
