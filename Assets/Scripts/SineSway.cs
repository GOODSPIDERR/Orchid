using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineSway : MonoBehaviour
{
    float index;
    public PlayerMovementScript playerMovementScript;
    private float currentSpeed;
    Vector3 originalPosition;
    Quaternion originalRotation;
    public float speedMultiplier = 1f, distanceMultiplier = 1f;
    Vector3 newPositionPlus, newPositionMinus;
    Quaternion newRotationPlus, newRotationMinus;
    [Header("Position Offsets")]
    public Vector3 positionOffsetPlus = new Vector3(0.1f, 0.05f, 0.05f);
    public Vector3 positionOffsetMinus = new Vector3(-0.1f, 0.05f, 0.05f);
    [Header("Rotation Offsets")]
    public Quaternion rotationOffsetPlus;
    public Quaternion rotationOffsetMinus;
    void Start()
    {
        //Multiplies the current offset in case I want to change values quickly
        positionOffsetPlus *= distanceMultiplier;
        positionOffsetMinus *= distanceMultiplier;

        //Finds the initial transforms
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;

        //It DOESN'T have to calculate the new final position every frame
        newPositionPlus = new Vector3(originalPosition.x + positionOffsetPlus.x, originalPosition.y + positionOffsetPlus.y, originalPosition.z + positionOffsetPlus.z);
        newPositionMinus = new Vector3(originalPosition.x + positionOffsetMinus.x, originalPosition.y + positionOffsetMinus.y, originalPosition.z + positionOffsetMinus.z);

        //Calculating the new rotation
        newRotationPlus = new Quaternion(originalRotation.x + rotationOffsetPlus.x, originalRotation.y + rotationOffsetPlus.y, originalRotation.z + rotationOffsetPlus.z, originalRotation.w + rotationOffsetPlus.w);
        newRotationMinus = new Quaternion(originalRotation.x + rotationOffsetMinus.x, originalRotation.y + rotationOffsetMinus.y, originalRotation.z + rotationOffsetMinus.z, originalRotation.w + rotationOffsetMinus.w);
    }

    void Update()
    {
        //Finds the current movement speed to control when the weapon sways
        currentSpeed = Mathf.Clamp(Mathf.Max(Mathf.Abs(playerMovementScript.move.x), Mathf.Abs(playerMovementScript.move.z)), 0f, 1f);

        //Debug.Log(Mathf.Clamp(Mathf.Max(Mathf.Abs(playerMovementScript.move.x), Mathf.Abs(playerMovementScript.move.z)), 0f, 1f));

        //Index calculator for the Sine function
        index = (index + Time.deltaTime * speedMultiplier * currentSpeed) % (Mathf.PI * 2f);

        //Just so I don't have to type Mathf.Sin(index) every time
        float Sinq()
        {
            return Mathf.Sin(index);
        }

        //Return to default position when you stop moving
        if (currentSpeed <= 0.25f)
        {
            float i = 0f;
            if (index < Mathf.PI)
            {
                index = Mathf.Lerp(index, 0f, i + Time.deltaTime * speedMultiplier);
            }
            else
            {
                index = Mathf.Lerp(index, Mathf.PI, i + Time.deltaTime * speedMultiplier);
            }
        }

        //First lerp for the negative transformations
        if (Sinq() <= 0f)
        {
            //Position transforms
            float newPositionX = Mathf.Lerp(originalPosition.x, newPositionMinus.x, Mathf.Sqrt(Mathf.Abs(Sinq())));
            float newPositionY = Mathf.Lerp(originalPosition.y, newPositionMinus.y, Mathf.Sqrt(Mathf.Abs(Sinq())));
            float newPositionZ = Mathf.Lerp(originalPosition.z, newPositionMinus.z, Mathf.Sqrt(Mathf.Abs(Sinq())));

            transform.localPosition = new Vector3(newPositionX, newPositionY, newPositionZ);

            //Rotation transforms
            float newRotationX = Mathf.Lerp(originalRotation.x, newRotationMinus.x, Mathf.Sqrt(Mathf.Abs(Sinq())));
            float newRotationY = Mathf.Lerp(originalRotation.y, newRotationMinus.y, Mathf.Sqrt(Mathf.Abs(Sinq())));
            float newRotationZ = Mathf.Lerp(originalRotation.z, newRotationMinus.z, Mathf.Sqrt(Mathf.Abs(Sinq())));
            float newRotationW = Mathf.Lerp(originalRotation.w, newRotationMinus.w, Mathf.Sqrt(Mathf.Abs(Sinq())));

            transform.localPosition = new Vector3(newPositionX, newPositionY, newPositionZ);
            transform.localRotation = new Quaternion(newRotationX, newRotationY, newRotationZ, newRotationW);
        }

        //Second lerp for the positive transformations
        else if (Sinq() > 0f)
        {
            //Position transforms
            float newPositionX = Mathf.Lerp(originalPosition.x, newPositionPlus.x, Mathf.Sqrt(Mathf.Abs(Sinq())));
            float newPositionY = Mathf.Lerp(originalPosition.y, newPositionPlus.y, Mathf.Sqrt(Mathf.Abs(Sinq())));
            float newPositionZ = Mathf.Lerp(originalPosition.z, newPositionPlus.z, Mathf.Sqrt(Mathf.Abs(Sinq())));

            transform.localPosition = new Vector3(newPositionX, newPositionY, newPositionZ);

            //Rotation transforms
            float newRotationX = Mathf.Lerp(originalRotation.x, newRotationPlus.x, Mathf.Sqrt(Mathf.Abs(Sinq())));
            float newRotationY = Mathf.Lerp(originalRotation.y, newRotationPlus.y, Mathf.Sqrt(Mathf.Abs(Sinq())));
            float newRotationZ = Mathf.Lerp(originalRotation.z, newRotationPlus.z, Mathf.Sqrt(Mathf.Abs(Sinq())));
            float newRotationW = Mathf.Lerp(originalRotation.w, newRotationPlus.w, Mathf.Sqrt(Mathf.Abs(Sinq())));

            transform.localPosition = new Vector3(newPositionX, newPositionY, newPositionZ);
            transform.localRotation = new Quaternion(newRotationX, newRotationY, newRotationZ, newRotationW);

        }
    }
}
