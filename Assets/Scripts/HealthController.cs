using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthController : MonoBehaviour
{
    public static HealthController Instance;

    public Image imageToFill;

    private GameObject healthBar;
    private void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag(Defs.Instance.playerTag).transform.Find("HBCanvas").gameObject;
        healthBar.SetActive(false);
    }

    private void Awake()
    {
        Instance = this;
    }
    public void HealtDecrease()
    {
        imageToFill.fillAmount -= 0.2f;
        StartCoroutine(SetHealthBarVisibility());
    }
    public void HealthIncreased()
    {
        imageToFill.fillAmount += 0.2f;
        StartCoroutine(SetHealthBarVisibility());
    }

    IEnumerator SetHealthBarVisibility()
    {
        healthBar.SetActive(true);
        yield return new WaitForSeconds(2f);
        healthBar.SetActive(false);
    }

}
