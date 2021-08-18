using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieMarc.EnemyVision;

public class CleanZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.CompareTag("Player") )
        {
            PaintTarget target = other.gameObject.GetComponent<PaintTarget>(); //  GameObject.FindObjectsOfType<PaintTarget>() as PaintTarget[];

            target.ClearPaint();

            GameObject.FindGameObjectWithTag(Defs.Instance.playerTag).gameObject.GetComponent<VisionTarget>().visible = false;

            Destroy(gameObject);

        }

    }
 
}
