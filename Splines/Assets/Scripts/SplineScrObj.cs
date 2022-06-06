using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spliner", menuName = "Spline")]
public class SplineScrObj : ScriptableObject
{
    [HideInInspector]
    public GameObject mainSpline;

    [HideInInspector]
    public List<GameObject> splineList = new List<GameObject>();
    [HideInInspector]
    public List<List<Transform>> anchorList = new List<List<Transform>>();
    [HideInInspector]
    public List<Transform> TPList = new List<Transform>();

    public void Add()
    {
        Spline sSpline;

        if (mainSpline == null)
        {
            mainSpline = new GameObject();
            mainSpline.name = "Spline Main";
        }

        GameObject spline = new GameObject();
        spline.name = "Spline";
        sSpline = spline.AddComponent<Spline>();
        spline.transform.SetParent(mainSpline.transform);
        splineList.Add(spline);

        List<Transform> anchors = new List<Transform>();
        for (int i = 0; i < 4; i++)
        {
            GameObject anchor = new GameObject();
            anchor.name = "Anchor " + i;
            anchor.transform.SetParent(spline.transform);
            anchors.Add(anchor.transform);
        }
        anchorList.Add(anchors);
        sSpline.SetAnchors(anchors);

        GameObject TP = new GameObject();
        TP.name = "Transform Point";
        TP.transform.SetParent(spline.transform);
        TPList.Add(TP.transform);
        sSpline.SetTransformPoint(TP.transform);
    }
}