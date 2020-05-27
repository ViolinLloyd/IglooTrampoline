using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideTrajectory : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 apex;
    private Vector2 endPos;

    private List<GameObject> guidePoints;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject crosshairs;

    private float score;

    // Start is called before the first frame update
    void Start()
    {
        guidePoints = new List<GameObject>();
        foreach(Transform child in transform)
        {
            guidePoints.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get three points to form the parabola
        startPos = new Vector2(player.transform.position.x, player.transform.position.y);
        apex = new Vector2(crosshairs.transform.position.x, crosshairs.transform.position.y);
        endPos = new Vector2(apex.x + (apex.x - startPos.x), startPos.y);

        // Properly position all the guide points
        for(int x = 0; x < guidePoints.Count; x++)
        {
            guidePoints[x].transform.position = GetParabolaPosition(x);
        }
    }

    private Vector3 GetParabolaPosition(int index)
    {
        float x = -10;
        float y = -10;
        int length = guidePoints.Count;

        float midpoint = Mathf.Floor(length / 2.0f);

        if(index <= midpoint)
        {
            // Find the distance the guidepoints will cover
            float xDist = apex.x - startPos.x;

            // Divide that distance by the number of guidePoints
            float interval = xDist / midpoint;

            // Find the current guidePoint position
            x = startPos.x + interval * (float)index;

            // Solve the parabola using startPos, apex, endPos
            y = CalcParabolaVertex(x);
        }
        else if(index <= length - score)
        {
             // Find the distance the guidepoints will cover
            float xDist = apex.x - startPos.x;

            // Divide that distance by the number of guidePoints
            float interval = xDist / midpoint;

            // Find the current guidePoint position
            x = apex.x + interval * (float)(index-midpoint);

            // Solve the parabola using startPos, apex, endPos
            y = CalcParabolaVertex(x);
        }

        return new Vector3(x, y, 0);
    }

    private float CalcParabolaVertex(float x)
    {
        float x1 = startPos.x;
        float y1 = startPos.y;
        float x2 = apex.x;
        float y2 = apex.y;
        float x3 = endPos.x;
        float y3 = endPos.y;

        float denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
        float A     = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
        float B     = (x3*x3 * (y1 - y2) + x2*x2 * (y3 - y1) + x1*x1 * (y2 - y3)) / denom;
        float C     = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;

        return A * Mathf.Pow(x, 2) + B * x + C;
    }

    public void IncreaseScore()
    {
        score += 2;
    }
}
