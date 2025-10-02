using UnityEngine;

public class ConnectJoints : MonoBehaviour
{
    void Start()
    {
        Rigidbody prevRb = null;

        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            ConfigurableJoint joint = child.GetComponent<ConfigurableJoint>();

            if (rb != null && joint != null)
            {
                if (prevRb != null)
                {
                    joint.connectedBody = prevRb;
                }
                prevRb = rb;
            }
        }
    }

    void Update()
    {
        
    }
}
