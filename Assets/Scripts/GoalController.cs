using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{

    private bool ballInGoal;

    public GameObject goalEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Detect when the ball enters the hole
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (!ballInGoal) // check that the ball is not already in the hole
            {
                ballInGoal = true; 

                Debug.Log("Ball in Hole");

                ShotController.instance.SetInHole(); // prevents more shots from firing

                HoleController.instance.BallInGoal(); 

                goalEffect.SetActive(true); // set off celebratory particle effects

                AudioController.instance.PlaySFX(2); // play 'in hole' sound effect
            }
        }
    }

}
