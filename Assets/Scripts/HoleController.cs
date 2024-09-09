using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleController : MonoBehaviour
{
    public static HoleController instance;

    private void Awake()
    {
        instance = this;
    }

    public int shotsTaken; // count for number of shots taken

    public int par;

    public float delayBallInGoal;
    // adds a delay after the ball is in the hole
    // (so that the level completion looks more natural)

    private Vector3 previousBallPosition;

    public float delayOutOfBounds = 2;

    public string nextHoleScene;

    public bool ballInGoal;

    // Start is called before the first frame update
    void Start()
    {
        par = CourseController.instance.holes[CourseController.instance.currentHole].par; // setting the par for the level
        UIController.instance.SetParText(par); // Par text on UI

        SetBallPosition();

        nextHoleScene = CourseController.instance.holes[CourseController.instance.currentHole].nextSceneName;

        AudioController.instance.PlayCourseTrack(); // play BGM music
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // function that adds the number of shots
    public void AddShot()
    {
        shotsTaken++;

        UIController.instance.UpdateShotText(shotsTaken); // updates the shot number when a shot is taken
    }

    public void BallInGoal()
    {
        ballInGoal = true;

        UIController.instance.ShowInHole();

        int finalScore = shotsTaken - par;

        string finalResult = "";

        switch(finalScore) // golf terms for the score achieved
        {
            case -4:
                finalResult = "Condor!!! (-4)";
                break;

            case -3:
                finalResult = "Albatross!!! (-3)";
                break;

            case -2:
                finalResult = "Eagle!! (-2)";
                break;

            case -1:
                finalResult = "Birdie! (-1)";
                break;

            case 0:
                finalResult = "Par!";
                break;

            case 1:
                finalResult = "Bogey (+1)";
                break;

            case 2:
                finalResult = "Double Bogey (+2)";
                break;

            case 3:
                finalResult = "Triple Bogey (+3)";
                break;

            // default case if the score doesn't have a name
            default:

                if (finalScore > 0)
                {
                    finalResult = "+" + finalScore;
                }
                else
                {
                    finalResult = finalScore.ToString();
                }
                break;
        }

        // special case for hole-in-1
        if(shotsTaken == 1)
        {
            finalResult = "Hole-in-One!!!!!";
        }

        //UIController.instance.ShowEndScreen(finalScore.ToString()); //shows the end screen (immediately)

        StartCoroutine(EndHoleCoroutine(finalResult)); // shows end screen after a delay

        CourseController.instance.holes[CourseController.instance.currentHole].score = shotsTaken; // saves the results of that hole
    }

    // function that delays the end result screen from showing up 
    private IEnumerator EndHoleCoroutine(string scoreResult)
    {
        yield return new WaitForSeconds(delayBallInGoal);

        UIController.instance.ShowEndScreen(scoreResult);

        CameraController.instance.StopCameraMovement();

    }

    public void SetBallPosition()
    {
        previousBallPosition = BallController.instance.transform.position;
    }

    public void OutOfBounds()
    {
        StartCoroutine(OutOfBoundsCoroutine());
    }

    // function that delays the resuming of the ball position after an OOB shot
    IEnumerator OutOfBoundsCoroutine()
    {
        AddShot(); // +1 shot penalty for OOB

        UIController.instance.ShowOutOfBounds(); // show OOB text

        yield return new WaitForSeconds(delayOutOfBounds);

        // bring the ball back to the position before the shot was taken
        BallController.instance.ballRB.Move(previousBallPosition, BallController.instance.transform.rotation); 

        BallController.instance.ballRB.velocity = Vector3.zero;        // these two ensure that the ball does not
        BallController.instance.ballRB.angularVelocity = Vector3.zero; // retain its momentum after the position reset

        ShotController.instance.AllowShot();

        BallController.instance.WithinBounds();

        UIController.instance.HideOutOfBounds();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextHoleScene);

        CourseController.instance.currentHole++;
    }
}
