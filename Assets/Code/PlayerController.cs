using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runSpeedMultiplier = 1.5f;
    [SerializeField] float sensitivity = 100f;
    [SerializeField] Transform pitchTransform;

    PlayerFootstep playerFootstepPlayer;
    CharacterController controller;
    Animator anim;
    float xRotation = 0f;
    float mouseX;
    float mouseY;
    float horizontal;
    float vertical;
    bool running = false;
    Vector3 moveDirection = Vector3.zero;


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerFootstepPlayer = GetComponentInChildren<PlayerFootstep>();
        anim = GetComponent<Animator>();
        sensitivity = PlayerPrefs.GetFloat("sensitivity", sensitivity);
    }

    void OnEnable() => PauseMenu.OnSensitivityChanged += UpdateSensitivity;

    void OnDisable() => PauseMenu.OnSensitivityChanged -= UpdateSensitivity;

    void OnApplicationFocus(bool focus)
    {
        Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !focus;
    }

    void Update()
    {
        ReadInput();
        HandleLook();
        HandleMove();
        HandleAniamtions();
    }

    void ReadInput()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        running = Input.GetKey(KeyCode.LeftShift);

        moveDirection = GetMoveDirection();
    }

    void HandleLook()
    {
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        pitchTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMove()
    {
        controller.SimpleMove(moveDirection * moveSpeed * (running ? runSpeedMultiplier : 1f));
    }

    void HandleAniamtions()
    {
        anim.SetBool("moving", moveDirection != Vector3.zero);
        anim.speed = running ? 2f : 1f;
    }

    Vector3 GetMoveDirection()
    {
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        if (direction.sqrMagnitude > 1f)
            direction.Normalize();

        return direction;
    }

    public void PlayFootstepSound() => playerFootstepPlayer.PlayStepSound();

    void UpdateSensitivity(float newValue) => sensitivity = newValue;
}
