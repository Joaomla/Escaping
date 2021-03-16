using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionBehavior : MonoBehaviour
{
    
    [SerializeField] GameObject companion = null;
    [SerializeField] GameObject player = null;
    [SerializeField] float xOffset = 0f;
    [SerializeField] float yOffset = 0f;
    [SerializeField] int resolution = 50;
    [SerializeField] float waveScale = 1f;
    [SerializeField] float waveAmplitude = 1f;
    [SerializeField] float period = 1f;
    [SerializeField] float startWidth = 0.06f;
    [SerializeField] float endWidth = 0.15f;
    LineRenderer lineRenderer;

    AnimationCurve curve;

    float t0 = 0;
    float width = 1f;

    Vector3 companionPos;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = this.GetComponent(typeof(LineRenderer)) as LineRenderer;
        companionPos = companion.transform.position;
        playerPos = player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
    }

    private void DrawLine()
    {
        GetPositions();

        curve = new AnimationCurve();

        Vector3 startPos = playerPos;
        Vector3 endPos = companionPos - new Vector3(xOffset, yOffset, 0);

        lineRenderer.positionCount = resolution;
        lineRenderer.SetPosition(0, startPos);
        curve.AddKey(0.0f, startWidth);

        t0 += (period*180)*Time.deltaTime;

        if (t0*Mathf.Deg2Rad > 2*Mathf.PI)
        {
            t0 -= 360;
        }

        for (int i = 1; i < resolution-1; i++)
        {
            float Sin, Cos, rotatedX, rotatedY;

           // gets the respective increment in the trajectory player - companion
            Vector3 dir = i * (endPos-startPos) / resolution;

            // sine function y = sin(x*T + t0)
            float X = dir.magnitude;
            float Y = waveAmplitude*Mathf.Sin(X * waveScale + t0*Mathf.Deg2Rad);

            // using a rotation matrix to put the sine wave in the direction player - companion
            float angle = (Vector3.Angle(dir, Vector3.right) )* Mathf.Deg2Rad;
            Sin = Mathf.Sin(angle);
            Cos = Mathf.Cos(angle);
            
            if (dir.y > Vector3.right.y)
            {
                rotatedX = X * Cos - Y * Sin;
                rotatedY = X * Sin + Y * Cos;
            }
            else
            {
                rotatedX = X * Cos + Y * Sin;
                rotatedY = -X * Sin + Y * Cos;
            }

            lineRenderer.SetPosition(i, new Vector3(startPos.x + rotatedX, startPos.y + rotatedY, startPos.z));

            curve.AddKey(i/(resolution-1), startWidth*(resolution-2-i)/(resolution-2) + endWidth*i/(resolution-2));
        }
        lineRenderer.SetPosition(resolution-1, endPos);
        curve.AddKey(1f, endWidth);

        lineRenderer.widthCurve = curve;
        lineRenderer.widthMultiplier = width;

    }

    public void GetPositions()
    {
        companionPos = companion.transform.position;
        playerPos = player.transform.position;
    }
}
