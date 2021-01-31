using UnityEngine;

/// <summary>
/// Draw planet orbit
/// </summary>
[System.Serializable]
public class OrbitRenderer
{
    private readonly int segments = 100;

    /// <summary>
    /// Calculate orbit ellipse
    /// </summary>
    public void CalculateEllipse(SolarObject orbit, LineRenderer lr)
    {
        Vector3[] points = new Vector3[segments + 1];

        for (int i = 0; i < segments; i++)
        {
            Vector3 pos = orbit.Evaluate(i / (float)segments);
            points[i] = new Vector3(pos.x, 0, pos.z);
        }

        points[segments] = points[0];

        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }
}
