using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class csDestroyEffect : MonoBehaviour {
	
	void Update () 
    {
        if (Keyboard.current.xKey.isPressed || Keyboard.current.cKey.isPressed || Keyboard.current.zKey.isPressed)
        {
            Destroy(gameObject);
        }
    }
}
