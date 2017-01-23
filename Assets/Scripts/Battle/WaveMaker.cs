using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveMaker : MonoBehaviour
{

    public AnimationCurve wave;
    public GameObject ball;
    public int waveLength = 18;
    public int numberOfWaves = 3;
    public int maxPoints = 100;
    public float moveSpeed = 5;

    private List<Vector3> points = new List<Vector3>();
    private LineRenderer wave_visual;
    private float x_OriginPos;
    private float x_Limit;
    private int currentIndex;

    private void Start()
    {
        currentIndex = maxPoints / 2;
        wave_visual = GetComponent<LineRenderer>();
        x_OriginPos = transform.position.x;
        x_Limit = transform.position.x / 2.1f;
        GenerateCurve();
        ball.transform.position = new Vector2(0, points[currentIndex].y);
    }

    private void Update()
    {
        int xInput = 0;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            xInput = 1;
        }
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            xInput = -1;
        }
        int oldIndex = currentIndex;
        if (Mathf.Abs(xInput) > 0)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, x_OriginPos + x_Limit, x_OriginPos - x_Limit) + (Time.deltaTime * -xInput * moveSpeed), 0);
            if (transform.position.x / 4 > -points[currentIndex - 1].x) currentIndex--;
            else if (transform.position.x / 4 < -points[currentIndex + 1].x) currentIndex++;
        }
        ball.transform.position = Vector2.Lerp(ball.transform.position, new Vector2(0, (points[currentIndex].y + points[oldIndex].y) / 2), Time.deltaTime * moveSpeed);

    }

    private void GenerateCurve()
    {
        float xAxis = 0;
        float yAxis = 0;
        for (int i = 1; i < maxPoints + 1; i++)
        {
            yAxis = wave.Evaluate(xAxis);
            points.Add(new Vector2(xAxis, yAxis) * waveLength);
            xAxis += 0.02f/numberOfWaves;
        }
        wave_visual.SetPositions(points.ToArray());
    }
}
