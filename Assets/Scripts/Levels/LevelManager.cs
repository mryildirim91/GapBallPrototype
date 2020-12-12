using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    BallMovement ballMovScript;
    public Slider skorSlider;

    [SerializeField]
    private Text leftLevelText, rightLevelText;

    public GameObject startPos, finishPos;

    private float awakeDist, updateDist;
    private int levelNumber;
    private void Awake()
    {
        ballMovScript = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMovement>();
        awakeDist = Vector3.Distance(startPos.transform.position, finishPos.transform.position);
    }

    public void Update()
    {
        updateDist = Vector3.Distance(ballMovScript.transform.position, finishPos.transform.position);
        Debug.Log(awakeDist - updateDist);
        skorSlider.value = awakeDist-updateDist;

        levelNumber= PlayerPrefs.GetInt("WhichLevel");
        leftLevelText.text =""+ (levelNumber+1);
        rightLevelText.text = "" + (levelNumber + 2);

    }

}
