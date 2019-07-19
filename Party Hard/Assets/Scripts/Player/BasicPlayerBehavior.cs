using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicPlayerBehavior : MonoBehaviour
{
    public int ID;
    CharacterController characterController;

    #region Player Rotation

    float RotationPrecision = 0.001f;
    public float RotationSpeed;

    #endregion

    #region Player Movement

    public int AmountOfPossibleJumps;
    int CurrentAmountOfJumps;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public Vector3 moveDirection = Vector3.zero;

    #endregion

    #region DEATH

    bool ShouldWarpToPosition;
    Vector3 WarpPosition;

    #endregion

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

    #region UNITY API

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        InitInputs();
    }

    void Update()
    {
    }

    private void LateUpdate()
    {
        MovePlayer();

        if (ShouldWarpToPosition)
        {
            characterController.enabled = false;
            transform.position = WarpPosition;
            ShouldWarpToPosition = false;
            characterController.enabled = true;
        }
    }

    #endregion

    #region Movement

    void MovePlayer()
    {
        float movedirectiony = moveDirection.y;
        moveDirection = GetLeftJoystickDirection() * speed;
        moveDirection.y = movedirectiony;

        if (characterController.isGrounded || CurrentAmountOfJumps < AmountOfPossibleJumps)
        {
            if (characterController.isGrounded)
            {
                CurrentAmountOfJumps = 0;
            }

            if (Input.GetButtonDown(AButton))
            {
                CurrentAmountOfJumps++;
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        HandlePlayerRotation();

    }

    void HandlePlayerRotation()
    {
        if (!isRightJoystickBeingUsed())
        {
            return;
        }

        Quaternion eulerRot = Quaternion.Euler(-GetPlayerOrientation());

        transform.rotation = Quaternion.Slerp(transform.rotation, eulerRot, Time.deltaTime * RotationSpeed);
    }

    float GetPlayerAngle()
    {
        Vector3 RawOrientation = GetRightJoystickDirection();

        return ((Mathf.Atan2(RawOrientation.z, RawOrientation.x) * Mathf.Rad2Deg) + 180f);
    }

    Vector3 GetPlayerOrientation()
    {
        Vector3 RawOrientation = GetRightJoystickDirection();

        return Vector3.up * ((Mathf.Atan2(RawOrientation.z, RawOrientation.x) * Mathf.Rad2Deg) + -90f);
    }

    Vector3 GetLeftJoystickDirection()
    {
        return new Vector3(
            Input.GetAxis(LeftJoystickHorizontal),
            0.0f,
            -Input.GetAxis(LeftJoystickVertical));
    }

    Vector3 GetRightJoystickDirection()
    {
        return new Vector3(
            Input.GetAxis(RightJoystickHorizontal),
            0.0f,
            -Input.GetAxis(RightJoysticVertical));
    }

    bool isRightJoystickBeingUsed()
    {

        if ((Input.GetAxis(RightJoystickHorizontal) < -RotationPrecision || Input.GetAxis(RightJoystickHorizontal) > RotationPrecision) && 
            (Input.GetAxis(RightJoysticVertical) < -RotationPrecision || Input.GetAxis(RightJoysticVertical) > RotationPrecision))
        {
            return true;
        }

        return false;
    }

    #endregion

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

    #region Death

    public void WarpPlayer(Vector3 position)
    {
        moveDirection = Vector3.zero;
        WarpPosition = position;
        ShouldWarpToPosition = true;
    }

    #endregion
}
