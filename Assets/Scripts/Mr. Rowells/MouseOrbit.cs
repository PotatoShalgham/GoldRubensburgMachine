using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbit : MonoBehaviour
{
    // Borrowed from http://wiki.unity3d.com/index.php?title=MouseOrbitImproved#Code_C.23

    public InputAction lookAction;
    public InputAction zoomAction;
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    Rigidbody rb;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rb = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            Vector2 delta = lookAction.ReadValue<Vector2>();

            x += delta.x * xSpeed * distance * 0.02f;
            y -= delta.y * ySpeed * 0.02f;


            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector2 scrollDelta = zoomAction.ReadValue<Vector2>();
            float scroll = scrollDelta.y;
            distance = Mathf.Clamp(distance - scroll * 5, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    void OnEnable()
    {
        lookAction.Enable();
        zoomAction.Enable();
    }

    void OnDisable()
    {
        lookAction.Disable();
        zoomAction.Disable();
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}