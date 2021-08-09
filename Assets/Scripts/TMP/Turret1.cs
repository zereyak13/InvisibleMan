using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1 : MonoBehaviour
{
    public Brush brush;
    public bool RandomChannel = false;
    public bool SingleShotClick = false;
    public bool ClearOnClick = false;
    public bool IndexBrush = false;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private bool HoldingButtonDown = false;

    //private Vector3 rotatePoint = Vector3.zero;

    private bool Help = false;

    private void Start()
    {
        rotationX = transform.eulerAngles.y;
        rotationY = -transform.eulerAngles.x;

        if (brush.splatTexture == null)
        {
            brush.splatTexture = Resources.Load<Texture2D>("splats");
            brush.splatsX = 4;
            brush.splatsY = 4;
        }
    }

    private void Update()
    {
        if (RandomChannel) brush.splatChannel = Random.Range(0, 4);

        if (Input.GetMouseButton(0))
        {
            if (!SingleShotClick || (SingleShotClick && !HoldingButtonDown))
            {
                if (ClearOnClick) PaintTarget.ClearAllPaint();
                PaintTarget.PaintCursor(brush);
                if (IndexBrush) brush.splatIndex++;
            }
            HoldingButtonDown = true;
        }
        else
        {
            HoldingButtonDown = false;
        }
    }

    
}
