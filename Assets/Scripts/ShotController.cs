using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShotController : MonoBehaviour
{
    public static ShotController instance;

    private void Awake()
    {
        instance = this;
    }

    public float maxShotPower;

    private bool canShoot; // ball state

    private BallController theBall; // ball object

    private float activeShotPower; // shot power
    public float powerBarSpeed;    // speed of power bar
    public bool powerChange;       // tracks whether power is increasing/decreasing (true/false)

    private bool inHole; // checks whether ball is in hole or not

    // Start is called before the first frame update
    void Start()
    {
        //canShoot = true;

        theBall = FindAnyObjectByType<BallController>();

        AllowShot();
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot) // if the ball can be shot
        {
            if (activeShotPower == maxShotPower) // when the shot power reaches max on the bar,
            {
                powerChange = false;            // make the shot power decrease.
            }
            else if (activeShotPower == 0f)      // when the shot power reaches 0,
            {
                powerChange = true;             // make the shot power increase.
            }


            if (powerChange) // when the shot power is in the increasing state, increase it
            {
                activeShotPower = Mathf.MoveTowards(activeShotPower, maxShotPower, powerBarSpeed * Time.deltaTime);
            }
            else            // else, reduce it
            {
                activeShotPower = Mathf.MoveTowards(activeShotPower, 0f, powerBarSpeed * Time.deltaTime);
            }

            UIController.instance.SetPowerBar(activeShotPower, maxShotPower);

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) // on left-click/spacebar
            {
                FireShot();
            }
        }
    }

    public void AllowShot()
    {
        if (!inHole) // as long as ball isn't in the hole, allow the shot
        {
            canShoot = true;

            UIController.instance.ShowPowerBar(); // show the power bar while making the shot

            // Make Power Bar 0f at the start of every shot
            activeShotPower = 0f;
            powerChange = true;

            UIController.instance.SetPowerBar(activeShotPower, maxShotPower);

            CameraController.instance.ShowIndicator(); // show the direction indicator
        }
    }

    void FireShot()
    {
        HoleController.instance.SetBallPosition(); // store ball position before the shot is fired

        theBall.ShootBall(activeShotPower); // fires shot with the current shot power
        
        PreventShot();

        HoleController.instance.AddShot(); // Adds +1 to the number of shots taken

        AudioController.instance.PlaySFX(1); // plays ball hit sound effect
    }

    // Set the ball state to be in the hole
    public void SetInHole()
    {
        inHole = true;

        PreventShot();
    }

    // Prevent shots after ball is in the hole/while ball is moving
    public void PreventShot()
    {
        canShoot = false;

        UIController.instance.HidePowerBar(); // hides power bar

        CameraController.instance.HideIndicator(); // hides direction indicator
    }

}
