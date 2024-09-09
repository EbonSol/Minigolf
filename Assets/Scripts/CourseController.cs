using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseController : MonoBehaviour
{
    public static CourseController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    public string courseName;

    public HoleInfo[] holes; // array that stores hole info in the course

    [System.Serializable]
    // Stored values from levels completed
    public class HoleInfo
    {
        public string nextSceneName;
        public int par;
        public int score;
    }

    public int currentHole; // to track current hole

    public int totalScore; // to track current total score

    public string firstScene; // hole 1

    public void UpdateTotalScore()
    {
        totalScore = 0;

        for (int i = 0; i < currentHole; i++)
        {
            totalScore += holes[i].score - holes[i].par;
        }

        if (HoleController.instance != null)
        {
            if (HoleController.instance.ballInGoal)
            {
                totalScore = 0;

                for (int i = 0; i <= currentHole; i++)
                {
                    totalScore += holes[i].score - holes[i].par;
                }
            }
        }
    }

    // this function resets data within the scene (level)
    // so that the level is restarted fresh
    public void ResetCourse()
    {
        totalScore = 0;
        currentHole = 0;

        foreach (HoleInfo hole in holes)
        {
            hole.score = 0;
        }
    }

}



