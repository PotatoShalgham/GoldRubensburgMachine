using UnityEngine;

public class ChangePerspectives : MonoBehaviour
{
    public GameObject from;
    public GameObject to;
    public GameObject followCamera;

    public MouseOrbit script;
    private void Start()
    {
        script = followCamera.GetComponent<MouseOrbit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == from)
        {
            script.target = to.transform;
        }
    }
}
