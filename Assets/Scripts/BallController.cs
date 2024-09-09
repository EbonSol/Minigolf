using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController instance;

    private void Awake()
    {
        instance = this;
    }

    public Rigidbody ballRB;

    public float hitPower;

    public float stopCutoff;
    public float stopSpeed = 0.95f;

    private CameraController ballCam;

    private float noMoveCounter;
    public float waitToEndShot = 0.5f;

    private bool isOutOfBounds;

    private Vector3 lastVelocity;
    public float bounceSoundThreshold = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        ballCam = FindFirstObjectByType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        /* if(Input.GetKey(KeyCode.F)) // Pressing F moves the ball
        {
            //ballRB.velocity = Vector3.forward * hitPower;

            ballRB.velocity = ballCam.transform.forward * hitPower;

            ballCam.HideIndicator();
        } */

        float speed = ballRB.velocity.magnitude;

        // Debug.Log(ballRB.velocity.magnitude);

        if (ballRB.velocity.y > -0.01f) // Keeps the ball moving when it has -y-axis velocity (gravity)
        {

            if (speed < stopCutoff)
            {
                ballRB.velocity = ballRB.velocity * stopSpeed; // adds friction to slow the ball down

                if (speed < 0.01f) // ensures the ball comes to a full stop
                {
                    ballRB.velocity = Vector3.zero; 
                    ballRB.angularVelocity = Vector3.zero;

                    //ballCam.ShowIndicator();
                    //ShotController.instance.AllowShot();
                }
            }
        }


        if (speed > 0.01f) // If ball is still moving, the noMoveCounter will always = 0.5f
        {
            noMoveCounter = waitToEndShot;
        }
        else
        {
            if (noMoveCounter > 0f) // When ball stops moving,
            {
                noMoveCounter -= Time.deltaTime; // noMoveCounter will drop
                if (noMoveCounter <= 0f) // Once noMoveCounter is <= 0f, allow the shot to fire
                {
                    // ballCam.ShowIndicator(); 
                    
                    if (isOutOfBounds == false) // only when the ball is within bounds
                    {
                        ShotController.instance.AllowShot();
                    }
                }
            }
        }

        if (Vector3.Magnitude(ballRB.velocity - lastVelocity) > bounceSoundThreshold && lastVelocity != Vector3.zero)
        {
            AudioController.instance.PlaySFX(0);
        }

        lastVelocity = ballRB.velocity;
    }

    public void ShootBall(float shotPower)
    {
        ballRB.velocity = ballCam.transform.forward * shotPower;

        // ballCam.HideIndicator();

        noMoveCounter = waitToEndShot; // to ensure that low-powered shots do not bug the game 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Out Of Bounds") && isOutOfBounds == false) // checks if ball is OOB
        {
            isOutOfBounds = true;

            HoleController.instance.OutOfBounds();
        }
    }

    public void WithinBounds()
    {
        isOutOfBounds = false;
    }
}
