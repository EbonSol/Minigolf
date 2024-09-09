using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    public ScorecardController scorecard;

    public TMP_Text finalScoreText;

    public string mainMenuScene;
    public string courseMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        scorecard.UpdateScore();

        int finalScore = CourseController.instance.totalScore;

        switch(finalScore)
        {
            case 0:
                finalScoreText.text = "Par";
                break;

            case > 0:
                finalScoreText.text = "+" + finalScore;
                break;

            case < 0:
                finalScoreText.text = finalScore.ToString();
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void RestartCourse()
    {
        CourseController.instance.ResetCourse(); // resets the played data

        SceneManager.LoadScene(CourseController.instance.firstScene);
    }

    public void CourseMenu()
    {
        SceneManager.LoadScene(courseMenuScene);
    }
}
