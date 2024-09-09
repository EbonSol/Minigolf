using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    public bool rotateX;
    public bool rotateY;
    public bool rotateZ;

    public float rotateSpeed;
    private float currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation += rotateSpeed * Time.deltaTime;

        if (rotateX)
        {
            transform.localRotation = Quaternion.Euler(currentRotation, 0f, 0f);
        }
        
        if (rotateY)
        {
            transform.localRotation = Quaternion.Euler(0f, currentRotation, 0f);
        }
        
        if (rotateZ)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
        }
    }
}
