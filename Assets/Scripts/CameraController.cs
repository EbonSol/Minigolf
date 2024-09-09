using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private void Awake()
    {
        instance = this;
    }

    public Transform target; // to target the golf ball

    public float moveSpeed;

    private float rotation;

    private float verticalRotation;
    public Transform verticalPoint;

    public GameObject directionIndicator; // adds an aiming indicator for the golf ball

    private bool canMove;

    // public bool useMouseRotation;

    // Start is called before the first frame update
    void Start()
    {
        verticalRotation = verticalPoint.localRotation.eulerAngles.x;

        Cursor.lockState = CursorLockMode.Confined; // ensures cursor stays within the game window

        canMove = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (canMove) // allows player to move the camera
        {
            transform.position = target.position;

            // rotation of camera based on mouseX/Y inputs
            rotation += Mathf.Clamp(Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X"), -1f, 1f) * moveSpeed * Time.deltaTime;
            verticalRotation += Mathf.Clamp(Input.GetAxis("Vertical") + Input.GetAxis("Mouse Y"), -1f, 1f) * moveSpeed * Time.deltaTime;

            verticalRotation = Mathf.Clamp(verticalRotation, 0f, 75f); // fixes camera height to a range

            transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            verticalPoint.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }

    public void ShowIndicator()
    {
        directionIndicator.SetActive(true);
    }

    public void HideIndicator()
    {
        directionIndicator.SetActive(false);   
    }

    public void StopCameraMovement()
    {
        canMove = false;
    }
}
