using System.Collections;
using UnityEngine;

/// <summary>
/// Main logic and preferences
/// </summary>
public class OrbitMotion : MonoBehaviour
{
    public SolarObject solarObject;
    public OrbitRenderer orbitRenderer;

    [Range(0f, 1f)]
    public float orbitProgress = 0f;
    public bool isActive = true;

    [Range(0f, 1f)]
    public float rotationProgress = 0f;
    private Vector3 rotationDirection;

    public SpeedOptions movementSpeed;
    public SpeedOptions rotationSpeed;

    private float simulationSpeedMovementValue = 0.4f;// كان ١ بس حسيته سريع
    private float simulationSpeedRotationValue = 1;

    public enum SpeedOptions
    {
        Normal,
        DayPerSecond,
        WeekPerSecond,
        MonthPerSecond,
        YearPerSecond
    }

    void Start()
    {
        StartCoroutine(Rotation());

        rotationProgress = 0;
    }

    /// <summary>
    /// Move object along its orbit
    /// </summary>
    void SetPosition()
    {
        Vector3 position = solarObject.Evaluate(orbitProgress);
        transform.localPosition = new Vector3(position.x, 0, position.z);
    }

    private void OnValidate()
    {
        // Load predefined planet values
        if (solarObject.type != Constants.Objects.None)
        {
            SolarObject obj = Constants.GetObjectData(solarObject.type);
            solarObject.xAxis = obj.xAxis;
            solarObject.zAxis = obj.zAxis;
            solarObject.orbitPeriodSeconds = obj.orbitPeriodYears;
            solarObject.rotationPeriodSeconds = obj.rotationPeriodDays;
            solarObject.orbitPeriodYears = obj.orbitPeriodYears;
            solarObject.rotationPeriodDays = obj.rotationPeriodDays;
            solarObject.rotationAngle = obj.rotationAngle;
            solarObject.isRotationClockwise = obj.isRotationClockwise;
            solarObject.isMoving = obj.isMoving;
            solarObject.isRotating = obj.isRotating;
        }

        // Use real world values
        if (solarObject.realWorldSimulation)
        {
            solarObject.orbitPeriodSeconds = solarObject.orbitPeriodYears * Constants.SECONDS_IN_YEAR;
            solarObject.rotationPeriodSeconds = solarObject.rotationPeriodDays * Constants.SECONDS_IN_DAY;

            // Adjust movement speed boost
            switch (movementSpeed)
            {
                case SpeedOptions.Normal: simulationSpeedMovementValue = 1; break;
                case SpeedOptions.DayPerSecond: simulationSpeedMovementValue = Constants.SECONDS_IN_DAY; break;
                case SpeedOptions.WeekPerSecond: simulationSpeedMovementValue = Constants.SECONDS_IN_WEEK; break;
                case SpeedOptions.MonthPerSecond: simulationSpeedMovementValue = Constants.SECONDS_IN_MONTH; break;
                case SpeedOptions.YearPerSecond: simulationSpeedMovementValue = Constants.SECONDS_IN_YEAR; break;
            }

            // Adjust rotation speed boot
            switch (rotationSpeed)
            {
                case SpeedOptions.Normal: simulationSpeedRotationValue = 1; break;
                case SpeedOptions.DayPerSecond: simulationSpeedRotationValue = Constants.SECONDS_IN_DAY; break;
                case SpeedOptions.WeekPerSecond: simulationSpeedRotationValue = Constants.SECONDS_IN_WEEK; break;
                case SpeedOptions.MonthPerSecond: simulationSpeedRotationValue = Constants.SECONDS_IN_MONTH; break;
                case SpeedOptions.YearPerSecond: simulationSpeedRotationValue = Constants.SECONDS_IN_YEAR; break;
            }
        }

        transform.rotation = Quaternion.Euler(solarObject.rotationAngle, 0, 0);

        // Rotate object clockwise or not
        if (solarObject.isRotationClockwise)
            rotationDirection = Vector3.up;
        else
            rotationDirection = Vector3.down;


        SetPosition();

        //Enable or disable orbit ellipse
        if (GetComponent<LineRenderer>() != null)
        {
            if (solarObject.drawOrbit)
            {
                GetComponent<LineRenderer>().enabled = true;
                orbitRenderer.CalculateEllipse(solarObject, GetComponent<LineRenderer>());
            }
            else
                GetComponent<LineRenderer>().enabled = false;
        }
    }

    /// <summary>
    /// Calculate movement position during play
    /// </summary>
    IEnumerator Movement()
    {
        while (true)
        {
            if (isActive && solarObject.isMoving)
            {
                float orbitSpeed = 1f / solarObject.orbitPeriodSeconds;

                orbitProgress += Time.deltaTime * orbitSpeed * simulationSpeedMovementValue;
                orbitProgress %= 1f;

                SetPosition();
            }

            yield return null;
        }
    }

    /// <summary>
    /// Calculate rotation angle during play
    /// </summary>
    IEnumerator Rotation()
    {
        while (true)
        {
            if (isActive && solarObject.isRotating)
            {
                float rotationSpeed = 360f / solarObject.rotationPeriodSeconds;

                rotationProgress += Time.deltaTime * rotationSpeed / 360f;
                rotationProgress %= 1f;

                transform.Rotate(rotationDirection, Time.deltaTime * rotationSpeed * simulationSpeedRotationValue);
            }

            yield return null;
        }
    }
}
