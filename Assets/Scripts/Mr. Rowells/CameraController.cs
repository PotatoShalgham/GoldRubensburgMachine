using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

// Borrowed from https://gist.github.com/ashleydavis/f025c03a9221bc840a2b

/// <summary>
/// A simple free camera to be added to a Unity game object.
/// 
/// Keys:
///	wasd / arrows	- movement
///	q/e 			- up/down (local space)
///	r/f 			- up/down (world space)
///	pageup/pagedown	- up/down (world space)
///	hold shift		- enable fast movement mode
///	right mouse  	- enable free look
///	mouse			- free look / rotation
///     
/// </summary>
public class CameraController : MonoBehaviour
{
    public InputAction lookAction;
    public InputAction zoomAction;
    /// <summary>
    /// Normal speed of camera movement.
    /// </summary>
    public float movementSpeed = 10f;

    /// <summary>
    /// Speed of camera movement when shift is held down,
    /// </summary>
    public float fastMovementSpeed = 100f;

    /// <summary>
    /// Sensitivity for free look.
    /// </summary>
    public float freeLookSensitivity = 3f;

    /// <summary>
    /// Amount to zoom the camera when using the mouse wheel.
    /// </summary>
    public float zoomSensitivity = 10f;

    /// <summary>
    /// Amount to zoom the camera when using the mouse wheel (fast mode).
    /// </summary>
    public float fastZoomSensitivity = 50f;

    /// <summary>
    /// Set to true when free looking (on right mouse button).
    /// </summary>
    private bool looking = false;

    void Update()
    {
        var fastMode = Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed;
        var movementSpeed = fastMode ? this.fastMovementSpeed : this.movementSpeed;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            transform.position = transform.position + (-transform.right * movementSpeed * Time.deltaTime);
        }

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            transform.position = transform.position + (transform.right * movementSpeed * Time.deltaTime);
        }

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            transform.position = transform.position + (transform.forward * movementSpeed * Time.deltaTime);
        }

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            transform.position = transform.position + (-transform.forward * movementSpeed * Time.deltaTime);
        }

        if (Keyboard.current.qKey.isPressed)
        {
            transform.position = transform.position + (transform.up * movementSpeed * Time.deltaTime);
        }

        if (Keyboard.current.eKey.isPressed)
        {
            transform.position = transform.position + (-transform.up * movementSpeed * Time.deltaTime);
        }

        if (Keyboard.current.rKey.isPressed || Keyboard.current.pageUpKey.isPressed)
        {
            transform.position = transform.position + (Vector3.up * movementSpeed * Time.deltaTime);
        }

        if (Keyboard.current.fKey.isPressed || Keyboard.current.pageDownKey.isPressed)
        {
            transform.position = transform.position + (-Vector3.up * movementSpeed * Time.deltaTime);
        }

        Vector2 delta = lookAction.ReadValue<Vector2>();

        if (looking)
        {
            float newRotationX = transform.localEulerAngles.y + delta.y * freeLookSensitivity;
            float newRotationY = transform.localEulerAngles.x - delta.x * freeLookSensitivity;
            transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        }

        float axis = zoomAction.ReadValue<Vector2>().y;
        if (axis != 0)
        {
            var zoomSensitivity = fastMode ? this.fastZoomSensitivity : this.zoomSensitivity;
            transform.position = transform.position + transform.forward * axis * zoomSensitivity;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            StartLooking();
        }
        else if (!Mouse.current.leftButton.isPressed)
        {
            StopLooking();
        }
    }

    void OnEnable()
    {
        lookAction.Enable();
        zoomAction.Enable();
    }

    void OnDisable()
    {
        StopLooking();
        lookAction.Disable();
        zoomAction.Disable();
    }

    /// <summary>
    /// Enable free looking.
    /// </summary>
    public void StartLooking()
    {
        looking = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Disable free looking.
    /// </summary>
    public void StopLooking()
    {
        looking = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}