using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerLevel
{
    Level2,
    Level4,
    Level8,
    Level16,
    Level32,
    Level64,
    Level128,
    Level256,
    Level512,
    Level1024,
    Level2048
}

public static class PlayerLevelMethods
{
    private static Dictionary<PlayerLevel, Color32> colors = new Dictionary<PlayerLevel, Color32>
    {
        { PlayerLevel.Level2,       new Color32(236, 77, 134, 255) },
        { PlayerLevel.Level4,       new Color32(94, 221, 93, 255) },
        { PlayerLevel.Level8,       new Color32(71, 138, 255, 255) },
        { PlayerLevel.Level16,      new Color32(230, 116, 81, 255) },
        { PlayerLevel.Level32,      new Color32(255, 248, 0, 255) },
        { PlayerLevel.Level64,      new Color32(255, 141, 230, 255) },
        { PlayerLevel.Level128,     new Color32(145, 57, 255, 255) },
        { PlayerLevel.Level256,     new Color32(255, 108, 54, 255) },
        { PlayerLevel.Level512,     new Color32(0, 159, 79, 255) },
        { PlayerLevel.Level1024,    new Color32(83, 255, 231, 255) },
        { PlayerLevel.Level2048,    new Color32(217, 0, 80, 255) },
    };

    /// <summary> Returns the color for a cube of the given level. </summary>
    public static Color32 GetColor(this PlayerLevel level)
    {
        return colors[level];
    }

    private static Dictionary<PlayerLevel, float> scales = new Dictionary<PlayerLevel, float>
    {
        { PlayerLevel.Level2,       1.0f },
        { PlayerLevel.Level4,       1.25f },
        { PlayerLevel.Level8,       1.5f },
        { PlayerLevel.Level16,      1.75f },
        { PlayerLevel.Level32,      2.0f },
        { PlayerLevel.Level64,      2.25f },
        { PlayerLevel.Level128,     2.5f },
        { PlayerLevel.Level256,     2.75f },
        { PlayerLevel.Level512,     3.0f },
        { PlayerLevel.Level1024,    3.25f },
        { PlayerLevel.Level2048,    3.5f },
    };

    /// <summary> Returns the scale for a cube of the given level. </summary>
    public static Vector3 GetScale(this PlayerLevel level)
    {
        var scale = scales[level];
        return new Vector3(scale, scale, scale);
    }

    /// <summary> Returns the level as its numeric value, i.e. PlayerLevel.Level256 returns 256 as an int. </summary>
    public static int GetNumericalValue(this PlayerLevel level)
    {
        return (int)Mathf.Pow(2f, (int)level + 1);
    }
}