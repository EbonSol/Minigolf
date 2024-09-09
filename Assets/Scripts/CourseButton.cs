using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CourseButton : MonoBehaviour
{
    public TMP_Text nameText;
    public CourseController course;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = course.courseName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCourse()
    {
        // remove the CourseController from the button parent when the game scene starts
        // for the CourseController to be brought across scenes (holes) throughout the course
        course.transform.SetParent(null); 

        // set the selected course's CourseController to be active
        course.gameObject.SetActive(true);

        // load the selected course
        SceneManager.LoadScene(course.firstScene);
    }
}
