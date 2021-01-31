using UnityEngine;

/// <summary>
/// Planet model
/// </summary>
[System.Serializable]
public class SolarObject
{
    public Constants.Objects type = Constants.Objects.None;

    public float xAxis;
    public float zAxis;

    public float orbitPeriodSeconds;
    public float rotationPeriodSeconds;
    public float rotationAngle;
    public bool isRotationClockwise;

    public bool realWorldSimulation = false;
    public float orbitPeriodYears = 1;
    public float rotationPeriodDays = 1;

    public bool drawOrbit = true;

    public bool isMoving = true;
    public bool isRotating = true;

    /// <summary>
    /// Calculate current orbit position
    /// </summary>
    public Vector3 Evaluate(float t)
    {
        float angle = -Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(angle) * xAxis;
        float z = Mathf.Cos(angle) * zAxis;

        return new Vector3(x, 0, z);
    }
}