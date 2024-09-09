using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorecardController : MonoBehaviour
{
    public TMP_Text totalScore;

    public TMP_Text[] parTexts;
    public TMP_Text[] shotsTexts;

    public void UpdateScore()
    {

        CourseController.instance.UpdateTotalScore();

        totalScore.text = "Total: " + CourseController.instance.totalScore;

        for (int i = 0; i < parTexts.Length; i++)
        {
            if (i < CourseController.instance.holes.Length)
            {
                parTexts[i].text = CourseController.instance.holes[i].par.ToString();


                if(CourseController.instance.currentHole > i || (CourseController.instance.currentHole == i && HoleController.instance.ballInGoal))   
                {
                    // only shows the shots taken after the hole is played
                    // OR during the current hole
                    // otherwise it shows '0 shots'
                    shotsTexts[i].text = CourseController.instance.holes[i].score.ToString();
                }
                else // shows - for unplayed holes
                {
                    shotsTexts[i].text = "-";
                }
            }
            else
            {
                parTexts[i].text = "";
                shotsTexts[i].text = "";
            }
        }
    }
}

