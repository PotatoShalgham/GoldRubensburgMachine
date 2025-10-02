using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public Camera followCamera;
    public Camera freeLookCamera;


    public void Start()
    {
        followCamera.enabled = true;
        freeLookCamera.enabled = false;
    }

    public void Update()
    {
        if(Keyboard.current.tabKey.isPressed)
        {
            if(followCamera.isActiveAndEnabled == true)
            {
                followCamera.enabled = false;
                freeLookCamera.enabled = true;
            }
            else
            {
                followCamera.enabled = true;
                freeLookCamera.enabled = false;
            }
        }
    }
}