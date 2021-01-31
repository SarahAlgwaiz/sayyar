using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to store constant values and predefined planets info
/// </summary>
public class Constants
{
    public static float SIMULATION_SPEED = 1;

    public const int SECONDS_IN_YEAR = 31536000;
    public const int SECONDS_IN_MONTH = 2592000;
    public const int SECONDS_IN_WEEK = 604800;
    public const int SECONDS_IN_DAY = 86400;

    public enum Objects
    {
        None,
        Sun,
        Mercury,
        Venus,
        Earth,
        Mars,
        Jupiter,
        Saturn,
        Uranus,
        Neptune
    }

    public static List<SolarObject> objects = new List<SolarObject> {
        new SolarObject { type = Objects.None },
        new SolarObject { type = Objects.Sun, xAxis = 0, zAxis = 0, orbitPeriodYears = 0, rotationPeriodDays = 26.5f, rotationAngle = 7.25f, isRotationClockwise = false, isMoving = false, isRotating = true },
        new SolarObject { type = Objects.Mercury, xAxis = 10, zAxis = 15, orbitPeriodYears = 0.24f, rotationPeriodDays = 176, rotationAngle = 2f, isRotationClockwise = false, isMoving = true, isRotating = true },
        new SolarObject { type = Objects.Venus, xAxis = 15, zAxis = 20, orbitPeriodYears = 0.61f, rotationPeriodDays = 242, rotationAngle = 177f, isRotationClockwise = true, isMoving = true, isRotating = true },
        new SolarObject { type = Objects.Earth, xAxis = 20, zAxis = 25, orbitPeriodYears = 1, rotationPeriodDays = 1, rotationAngle = 23.5f, isRotationClockwise = false, isMoving = true, isRotating = true },
        new SolarObject { type = Objects.Mars, xAxis = 25, zAxis = 30, orbitPeriodYears = 1.88f, rotationPeriodDays = 1.01f, rotationAngle = 25f, isRotationClockwise = false, isMoving = true, isRotating = true },
        new SolarObject { type = Objects.Jupiter, xAxis = 30, zAxis = 35, orbitPeriodYears = 11.86f, rotationPeriodDays = 0.41f, rotationAngle = 3f, isRotationClockwise = false, isMoving = true, isRotating = true },
        new SolarObject { type = Objects.Saturn, xAxis = 35, zAxis = 40, orbitPeriodYears = 29.5f, rotationPeriodDays = 0.43f, rotationAngle = 26f, isRotationClockwise = false, isMoving = true, isRotating = true },
        new SolarObject { type = Objects.Uranus, xAxis = 40, zAxis = 45, orbitPeriodYears = 84, rotationPeriodDays = 0.71f, rotationAngle = 97f, isRotationClockwise = false, isMoving = true, isRotating = true },
        new SolarObject { type = Objects.Neptune, xAxis = 45, zAxis = 50, orbitPeriodYears = 165, rotationPeriodDays = 0.66f, rotationAngle = 29.6f, isRotationClockwise = false, isMoving = true, isRotating = true },
    };

    /// <summary>
    /// Return selected planet info
    /// </summary>
    public static SolarObject GetObjectData(Objects selected)
    {
        return objects.Find(x => x.type == selected);
    }
}