using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string sceneToLoad; // string for scene name

    public GameObject settings;

    private void Start()
    {
        AudioController.instance.PlayMainMenuTrack();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settings.gameObject.activeSelf)
            {
                ToggleSettings();
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Quit()
    {
        Application.Quit();

        Debug.Log("Quit");
    }

    public void ToggleSettings()
    {
        settings.gameObject.SetActive(!settings.activeSelf);
    }
}
