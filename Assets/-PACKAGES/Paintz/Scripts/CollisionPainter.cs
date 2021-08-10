using UnityEngine;

public class CollisionPainter : MonoBehaviour
{
    public Brush brush;
    public bool RandomChannel = false;

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collision collision)
    {
        Destroy(gameObject,0.02f);
        //ORİGİNAL
        foreach (ContactPoint contact in collision.contacts)
        {
            PaintTarget paintTarget = contact.otherCollider.GetComponent<PaintTarget>();
            if (paintTarget != null)
            {
                if (RandomChannel) brush.splatChannel = Random.Range(0, 4);
                PaintTarget.PaintObject(paintTarget, contact.point, contact.normal, brush);
            }
        }

        //ContactPoint contact = collision.contacts[0];
        //PaintTarget paintTarget = contact.otherCollider.GetComponent<PaintTarget>();
        //if (paintTarget != null)
        //{
        //    if (RandomChannel) brush.splatChannel = Random.Range(0, 4);
        //    PaintTarget.PaintObject(paintTarget, contact.point, contact.normal, brush);
        //}
    }
}