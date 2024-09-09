using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CourseSelectController : MonoBehaviour
{
    public string mainMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        if (CourseController.instance != null)
        {
            Destroy(CourseController.instance.gameObject);
            CourseController.instance = null;
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
}
