using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.CompareTag("Player") )
        {
            PaintTarget target = other.gameObject.GetComponent<PaintTarget>(); //  GameObject.FindObjectsOfType<PaintTarget>() as PaintTarget[];

            target.ClearPaint();

            Destroy(gameObject);

        }
    }
 
}
