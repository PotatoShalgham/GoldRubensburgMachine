using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class csShowAllEffect : MonoBehaviour
{
    public string[] EffectNames;
    public string[] Effect2Names;
    public Transform[] Effect;
    public UnityEngine.UI.Text Text1;
    int i = 0;
    int a = 0;


    void Start()
    {
        Instantiate(Effect[i], new Vector3(0, 5, 0), Quaternion.identity);
    }


    void Update()
    {
        Text1.text = i + 1 + ":" + EffectNames[i];

        if (Keyboard.current.zKey.isPressed)
        {
            if (i <= 0)
                i = 99;

            else
                i--;

            for (a = 0; a < Effect2Names.Length; a++)
            {
                if (EffectNames[i] == Effect2Names[a])
                {
                    Instantiate(Effect[i], new Vector3(0, 0.01f, 0), Quaternion.identity);
                    break;
                }
            }
            if (a++ == Effect2Names.Length)
                Instantiate(Effect[i], new Vector3(0, 5, 0), Quaternion.identity);
        }

        if (Keyboard.current.xKey.isPressed)
        {
            if (i < 99)
                i++;

            else
                i = 0;

            for (a = 0; a < Effect2Names.Length; a++)
            {
                if (EffectNames[i] == Effect2Names[a])
                {
                    Instantiate(Effect[i], new Vector3(0, 0.01f, 0), Quaternion.identity);
                    break;
                }
            }
            if (a++ == Effect2Names.Length)
                Instantiate(Effect[i], new Vector3(0, 5, 0), Quaternion.identity);
        }

        if (Keyboard.current.cKey.isPressed)
        {

            for (a = 0; a < Effect2Names.Length; a++)
            {
                if (EffectNames[i] == Effect2Names[a])
                {
                    Instantiate(Effect[i], new Vector3(0, 0.01f, 0), Quaternion.identity);
                    break;
                }
            }
            if (a++ == Effect2Names.Length)
                Instantiate(Effect[i], new Vector3(0, 5, 0), Quaternion.identity);
        }

    }
}
