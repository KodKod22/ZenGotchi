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

    [Header("Throw")]
    public float startRadius = 1.5f;        // a swipe must start this close to the ball
    public float minSwipe = 0.5f;           // shorter swipes are ignored
    public float powerPerUnit = 1.5f;       // how far the ball goes per unit of swipe up
    public float aimPerUnit = 1.5f;         // how far sideways per unit of swipe sideways
    public float flightSeconds = 0.9f;
    public float arcHeight = 2f;            // how high above the landing point the arc goes
    public float farScale = 0.6f;           // ball size at the hoop
    public float hitToleranceX = 0.55f;     // how close to the rim center counts as a hit
    public float hitToleranceY = 0.6f;
    public float fallSeconds = 0.5f;
    public float bankToleranceX = 0.45f;    // off the board: how close to the rim center still drops in
    public float bankSeconds = 0.25f;       // the slide down the board to the rim
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