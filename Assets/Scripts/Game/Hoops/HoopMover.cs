using UnityEngine;

// HoopMover.cs: moves the hoop in the pattern from HoopsConfig.
// The level manager owns the clock and calls Tick every frame.
public class HoopMover : MonoBehaviour
{
    Vector3 center;
    float speed;
    Vector2 offset;
    float horizontalPhase, verticalPhase;

    HoopsConfig Hoops => Services.Config.Hoops;

    void Awake() => center = transform.position;

    // Called once when a level starts, with that level's speed.
    public void Begin(float levelSpeed)
    {
        speed = levelSpeed;
        offset = Vector2.zero;
        horizontalPhase = verticalPhase = 0f;
        transform.position = center;
    }

    // elapsed: seconds since the level started. dt: this frame's time.
    public void Tick(float elapsed, float dt)
    {
        bool horizontal = elapsed >= Hoops.moveHorizontalAt;
        bool vertical = elapsed >= Hoops.moveVerticalAt
                        && (elapsed < Hoops.moveHorizontalAt || elapsed >= Hoops.moveBothAt);

        offset.x = Axis(horizontal, ref horizontalPhase, Hoops.horizontalRange, offset.x, dt);
        offset.y = Axis(vertical, ref verticalPhase, Hoops.verticalRange, offset.y, dt);
        transform.position = center + (Vector3)offset;
    }

    float Axis(bool active, ref float phase, float range, float current, float dt)
    {
        if (!active)
        {
            phase = 0f;                                         // restart smoothly next time
            return Mathf.MoveTowards(current, 0f, speed * dt);  // glide back to the center
        }
        phase += speed / range * dt;
        return range * Mathf.Sin(phase);
    }
}