using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    private List<GameObject> points = new List<GameObject>();
    private List<GameObject> lines = new List<GameObject>();

    public GameObject PointOriginal;
    public Slider slider;
    public TextMeshProUGUI sliderText;

    private int numberOfPoints = 2;
    private int lengthOfLineRenderer = 2;

    void Start()
    {
        CreatePoints(numberOfPoints);
        CreateLines(numberOfPoints);

        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        sliderText.text = (slider.value * 2).ToString();

        destroyObjects();

        numberOfPoints = (int)slider.value * 2;

        CreatePoints(numberOfPoints);
        CreateLines(numberOfPoints);
    }

    void CreatePoints(int numberOfPoints)
    {
        for (float angle = 0; angle < Mathf.PI * 2; angle += Mathf.PI / (numberOfPoints / 2))
        {
            GameObject PointClone = Instantiate(PointOriginal, new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)), Quaternion.identity);
            points.Add(PointClone);
        }
    }

    void CreateLines(int numberOfPoints)
    {
        for (int i = 1; i < numberOfPoints; i++)
        {
            GameObject child = new GameObject();

            lines.Add(child);

            LineRenderer lineRenderer = child.AddComponent<LineRenderer>();
            lineRenderer.name = "line";
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.gray;
            lineRenderer.endColor = Color.gray;
            lineRenderer.widthMultiplier = 0.01f;
            lineRenderer.positionCount = lengthOfLineRenderer;

            lineRenderer.SetPosition(0, points[i].transform.position);
            if (i > numberOfPoints / 2 - 1)
                lineRenderer.SetPosition(1, points[i * 2 - numberOfPoints].transform.position);
            else
                lineRenderer.SetPosition(1, points[i * 2].transform.position);
        }
    }

    void destroyObjects()
    {
        foreach (GameObject point in points)
            DestroyImmediate(point.gameObject);

        foreach (GameObject line in lines)
            DestroyImmediate(line.gameObject);

        points.Clear();
        lines.Clear();
    }
}
