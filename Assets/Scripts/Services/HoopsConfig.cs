using System;
using UnityEngine;

// HoopsConfig.cs: every gameplay number of the Hoops mini-game in one asset.
[CreateAssetMenu(menuName = "Game/Hoops Config")]
public class HoopsConfig : ScriptableObject
{
    [Header("Every level")]
    public int shotsPerLevel = 6;
    public float levelSeconds = 60f;
    public int twoStarHits = 5;
    public int threeStarHits = 6;

    [Header("Hoop movement, same pattern in every level")]
    public float moveVerticalAt = 15f;      // seconds into the level
    public float moveHorizontalAt = 30f;
    public float moveBothAt = 45f;
    public float horizontalRange = 1.5f;    // world units from the center
    public float verticalRange = 0.8f;

    [Header("Levels")]
    public HoopsLevel[] levels =
    {
        new HoopsLevel { targetHits = 3, hoopSpeed = 0.5f },
        new HoopsLevel { targetHits = 4, hoopSpeed = 0.8f },
        new HoopsLevel { targetHits = 5, hoopSpeed = 1.1f },
    };

    // 0 = lost, 1-3 = stars
    public int Stars(int level, int hits)
    {
        if (hits >= threeStarHits) return 3;
        if (hits >= twoStarHits) return 2;
        if (hits >= levels[level].targetHits) return 1;
        return 0;
    }
}

[Serializable]
public class HoopsLevel
{
    public int targetHits;
    public float hoopSpeed;                 // world units per second
}