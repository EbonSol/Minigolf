using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    public Slider powerBar;

    public GameObject inHoleText;

    public TMP_Text shotText;
    public TMP_Text parText;

    public GameObject endScreen;
    public TMP_Text endScreenScoreText;

    public GameObject OutOfBoundsText;

    public string mainMenu;

    public ScorecardController scoreCard;

    public GameObject pauseScreen;

    public string courseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleScorecard();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseScreen();
        }
    }

    public void ShowPowerBar()
    {
        powerBar.gameObject.SetActive(true);
    }

    public void SetPowerBar(float power, float maxPower)
    {
        powerBar.maxValue = maxPower;
        powerBar.value = power;
    }

    public void HidePowerBar()
    {
        powerBar.gameObject.SetActive(false);
    }

    public void ShowInHole()
    {
        inHoleText.SetActive(true);
    }

    // function updates the shot text
    public void UpdateShotText(int currentShot)
    {
        shotText.text = "Shots: " + currentShot;
    }

    public void SetParText(int currentPar)
    {
        parText.text = "Par: " + currentPar;
    }

    public void ShowEndScreen(string scoreResult)
    {
        endScreenScoreText.text = scoreResult;

        endScreen.gameObject.SetActive(true);
    }

    public void ShowOutOfBounds()
    {
        OutOfBoundsText.SetActive(true);
    }

    public void HideOutOfBounds()
    {
        OutOfBoundsText.SetActive(false);
    }

    // function to manage the Play Again Button
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void NextLevel()
    {
        HoleController.instance.LoadNextLevel();
    }

    // shows and hides the scorecard
    public void ToggleScorecard()
    {
        scoreCard.gameObject.SetActive(!scoreCard.gameObject.activeSelf);

        if(scoreCard.gameObject.activeSelf == true)
        {
            scoreCard.UpdateScore();
        }
    }

    public void TogglePauseScreen()
    {
        pauseScreen.gameObject.SetActive(!pauseScreen.activeSelf);
    }

    public void BackToCourseScreen()
    {
        SceneManager.LoadScene(courseScreen);
    }

}
