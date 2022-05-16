using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    public Transform transformPoint;

    public List<Transform> anchors;

    List<Vector3> pointsBetweenAnchors = new List<Vector3>();
    List<Vector3> pointsBetweenPoints = new List<Vector3>();

    Vector3 TP = new Vector3(0, 0, 0);

    float t = 0f;

    int anchorsSize = 0;
    int betweenAnchorsSize = 0;
    int betweenPointsSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        TP = anchors[0].position;
        transformPoint.position = TP;

        anchorsSize = anchors.Count;

        for (int i = 0; i < anchorsSize - 1; i++)
            pointsBetweenAnchors.Add(anchors[i].position);

        betweenAnchorsSize = pointsBetweenAnchors.Count;

        for (int i = 0; i < betweenAnchorsSize - 1; i++)
            pointsBetweenPoints.Add(pointsBetweenAnchors[i]);

        betweenPointsSize = pointsBetweenPoints.Count;
    }

    // Update is called once per frame
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

        TP = Lerp(pointsBetweenPoints[betweenPointsSize - 2], pointsBetweenPoints[betweenPointsSize - 1], t);

        transformPoint.position = TP;
    }

    Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        return a + (b - a) * t;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawLine(anchors[0].position, anchors[1].position);
        Gizmos.DrawLine(anchors[2].position, anchors[3].position);
    }
}