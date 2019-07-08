using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicPlayerBehavior : MonoBehaviour
{
    public int ID;
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    #region INPUT NAMES

    string LeftJoystickHorizontal;
    string LeftJoystickVertical;
    string RightJoystickHorizontal;
    string RightJoysticVertical;
    string AButton;
    string BButton;
    string XButton;
    string YButton;

    #endregion

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        InitInputs();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis(LeftJoystickHorizontal), 0.0f, -Input.GetAxis(LeftJoystickVertical));
            moveDirection *= speed;
        
            if (Input.GetButton(AButton))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    #region INITIALIZATION

    void InitInputs()
    {
        LeftJoystickHorizontal = "J" + ID + "LeftJoystickHorizontal";
        LeftJoystickVertical = "J" + ID + "LeftJoystickVertical";
        RightJoystickHorizontal = "J" + ID + "RightJoystickHorizontal";
        RightJoysticVertical = "J" + ID + "RightJoystickVertical";
        AButton = "J" + ID + "AButton";
        BButton = "J" + ID + "BButton";
        XButton = "J" + ID + "XButton";
        YButton = "J" + ID + "YButton";
    }

    #endregion
    /*
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hit.transform.SendMessage("SomeFunction", SendMessageOptions.DontRequireReceiver);
    }*/
}
