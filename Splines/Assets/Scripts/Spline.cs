using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    // Game object to see in game
    public Transform objectToFollowSpline;

    // the position the objectToFollowPath is tied to
    protected Transform transformPoint;

    // list of the anchor points
    protected List<Transform> anchors;

    // lerped points between the anchors positions
    protected List<Vector3> pointsBetweenAnchors = new List<Vector3>();
    // lerped points between other points
    protected List<Vector3> pointsBetweenPoints = new List<Vector3>();

    // t value
    private float t = 0f;

    // the amount of anchors
    private int anchorsSize = 0;
    // the amount of points between anchors
    private int betweenAnchorsSize = 0;
    // the amount of points between other points
    private int betweenPointsSize = 0;

    public void SetFollowObject(Transform a_object)
    {
        objectToFollowSpline = a_object;
    }

    public void SetTransformPoint(Transform a_transformPoint)
    {
        transformPoint = a_transformPoint;
    }

    public void SetAnchors(List<Transform> a_anchors)
    {
        anchors = a_anchors;
    }

    void Start()
    {
        transformPoint.position = anchors[0].position;
        objectToFollowSpline.position = transformPoint.position;

        anchorsSize = anchors.Count;

        for (int i = 0; i < anchorsSize - 1; i++)
            pointsBetweenAnchors.Add(anchors[i].position);

        betweenAnchorsSize = pointsBetweenAnchors.Count;

        for (int i = 0; i < betweenAnchorsSize - 1; i++)
            pointsBetweenPoints.Add(pointsBetweenAnchors[i]);

        betweenPointsSize = pointsBetweenPoints.Count;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            t += Time.deltaTime;
            t = Mathf.Clamp(t, 0, 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            t -= Time.deltaTime;
            t = Mathf.Clamp(t, 0, 1);
        }

        for (int i = 0; i < betweenAnchorsSize; i++)
            pointsBetweenAnchors[i] = Lerp(anchors[i].position, anchors[i + 1].position, t);

        for (int i = 0; i < betweenPointsSize; i++)
            pointsBetweenPoints[i] = Lerp(pointsBetweenAnchors[i], pointsBetweenAnchors[i + 1], t);

        transformPoint.position = Lerp(pointsBetweenPoints[betweenPointsSize - 2], pointsBetweenPoints[betweenPointsSize - 1], t);

        objectToFollowSpline.position = transformPoint.position;
    }

    Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        return a + (b - a) * t;
    }

    private void OnDrawGizmos()
    {
        // Draw lines between anchor points
        Gizmos.color = Color.grey;
        Gizmos.DrawLine(anchors[0].position, anchors[1].position);
        Gizmos.DrawLine(anchors[2].position, anchors[3].position);

        // Draw anchor points
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(anchors[0].position, 0.25f);
        Gizmos.DrawSphere(anchors[1].position, 0.25f);
        Gizmos.DrawSphere(anchors[2].position, 0.25f);
        Gizmos.DrawSphere(anchors[3].position, 0.25f);

        // Draw Transform point
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transformPoint.position, 0.5f);
    }
}