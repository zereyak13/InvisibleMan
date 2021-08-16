using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthController : MonoBehaviour
{
    public Image imageToFill;

    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            imageToFill.fillAmount -= 0.01f;
        }
        else if (Input.GetKey(KeyCode.Y))
        {
            imageToFill.fillAmount += 0.01f;
        }
    }
}
