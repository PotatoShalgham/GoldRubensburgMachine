using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour
{
    public float force;
    public Transform target;
    public Collider col;
    public bool rewind;

    void Start()
    {
        rewind = false;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = (target.position - collision.transform.position).normalized;
            rb.AddForce(direction * force, ForceMode.Impulse);
            StartCoroutine(RotatePlank());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == col)
        {
            rewind = true;
            Debug.Log("Rewind");
        }
    }

    private IEnumerator RotatePlank() // >>Assited<< by ChatGPT
    {
        yield return new WaitForSeconds(2f / force);

        Quaternion upRotation = Quaternion.Euler(0f, 0f, 90f);
        Quaternion downRotation = Quaternion.Euler(0f, 0f, 0f);

        // Swing up
        while (!rewind && Quaternion.Angle(transform.localRotation, upRotation) > 0.5f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, upRotation, Time.deltaTime * 5f);
            yield return null;
        }

        // Swing back
        while (rewind && Quaternion.Angle(transform.localRotation, downRotation) > 0.5f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, downRotation, Time.deltaTime * 5f);
            yield return null;
        }

        transform.localRotation = downRotation; // snap to clean final rotation
        rewind = false;

        yield break;
    }

    private void OnApplicationQuit()
    {
        StopCoroutine(RotatePlank());
    }
}
