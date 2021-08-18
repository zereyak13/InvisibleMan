using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieMarc.EnemyVision;


public class ParticlePainter : MonoBehaviour
{
    public Brush brush;
    public bool RandomChannel = false;

    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        PaintTarget paintTarget;
        if (other.gameObject.CompareTag(Defs.Instance.playerTag))
        {
            paintTarget = other.transform.Find("Cube.006").GetComponent<PaintTarget>();

            GameObject.FindGameObjectWithTag(Defs.Instance.playerTag).gameObject.GetComponent<VisionTarget>().visible = true;
        }
        else
        {
            paintTarget = other.transform.GetComponent<PaintTarget>();
        }

        if (paintTarget != null)
        {
            if (RandomChannel) brush.splatChannel = Random.Range(0, 4);

            int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
            for (int i = 0; i < numCollisionEvents; i++)
            {
                PaintTarget.PaintObject(paintTarget, collisionEvents[i].intersection, collisionEvents[i].normal, brush);
            }
        }
    }
}