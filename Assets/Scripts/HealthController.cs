using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthController : MonoBehaviour
{
    public static HealthController Instance;

    public Image imageToFill;


    private void Awake()
    {
        Instance = this;
    }
    public void HealtDecrease()
    {
        imageToFill.fillAmount -= 0.2f;
    }
    public void HealthIncreased()
    {
        imageToFill.fillAmount += 0.2f;
    }
}
