using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// BallThrower.cs: swipe up from the ball to throw it. Fake depth: the ball shrinks as it flies.
// On the Ball. The level manager sets CanThrow and listens to Thrown and Landed.
public class BallThrower : MonoBehaviour
{
    [SerializeField] Transform rim;             // drag Hoop/Rim here
    [SerializeField] SpriteRenderer ballSprite; // drag the Ball's SpriteRenderer here
    [SerializeField] SpriteRenderer backboard;  // drag Hoop/Backboard here
    [SerializeField] int behindRimOrder = 5;    // sorting order while falling through the net

    public bool CanThrow { get; set; }
    public event Action Thrown;                 // a shot was used
    public event Action<bool> Landed;           // true = hit

    Vector3 start;
    int frontOrder;
    Vector3? swipeStart;
    Camera cam;

    HoopsConfig Hoops => Services.Config.Hoops;

    void Awake()
    {
        cam = Camera.main;
        start = transform.position;
        frontOrder = ballSprite.sortingOrder;
    }

    void Update()
    {
        var pointer = Pointer.current;
        if (!CanThrow || pointer == null) return;

        Vector3 world = cam.ScreenToWorldPoint(pointer.position.ReadValue());
        world.z = 0f;

        if (pointer.press.wasPressedThisFrame &&
            Vector2.Distance(world, start) <= Hoops.startRadius)
            swipeStart = world;

        if (pointer.press.wasReleasedThisFrame && swipeStart.HasValue)
        {
            Vector3 swipe = world - swipeStart.Value;
            swipeStart = null;
            if (swipe.y >= Hoops.minSwipe) StartCoroutine(Fly(swipe));
        }
    }

    IEnumerator Fly(Vector3 swipe)
    {
        CanThrow = false;
        Thrown?.Invoke();

        Vector3 landing = start + new Vector3(swipe.x * Hoops.aimPerUnit, swipe.y * Hoops.powerPerUnit, 0f);

        // 1 · the arc: up, then down onto the landing point, getting smaller
        for (float t = 0f; t < 1f; t += Time.deltaTime / Hoops.flightSeconds)
        {
            Vector3 p = Vector3.Lerp(start, landing, t);
            p.y += Hoops.arcHeight * 4f * t * (1f - t);
            transform.position = p;
            transform.localScale = Vector3.one * Mathf.Lerp(1f, Hoops.farScale, t);
            if (t > 0.5f) ballSprite.sortingOrder = behindRimOrder;   // past the top: behind the rim
            yield return null;
        }
        transform.position = landing;

        // 2 · hit or miss, against where the rim is right now
        Vector3 d = landing - rim.position;
        bool hit = Mathf.Abs(d.x) <= Hoops.hitToleranceX && Mathf.Abs(d.y) <= Hoops.hitToleranceY;
        bool banked = false;

        // 2b · too high but on the board: it bounces off and slides down to the rim
        if (!hit && d.y > Hoops.hitToleranceY && OnBoard(landing))
        {
            for (float t = 0f; t < 1f; t += Time.deltaTime / Hoops.bankSeconds)
            {
                transform.position = Vector3.Lerp(landing, new Vector3(landing.x, rim.position.y, 0f), t);
                yield return null;
            }
            transform.position = new Vector3(landing.x, rim.position.y, 0f);
            d = transform.position - rim.position;
            hit = banked = Mathf.Abs(d.x) <= Hoops.bankToleranceX;
        }
        Debug.Log($"Shot: {(banked ? "BANK" : hit ? "HIT" : "miss")}, off by x {d.x:0.00}, y {d.y:0.00}");   // temporary, for tuning

        // 3 · fall: through the net, or past it
        Vector3 from = transform.position;
        Vector3 to = from + Vector3.down * 2f;
        for (float t = 0f; t < 1f; t += Time.deltaTime / Hoops.fallSeconds)
        {
            transform.position = Vector3.Lerp(from, to, t);
            yield return null;
        }

        // 4 · back to the start
        transform.position = start;
        transform.localScale = Vector3.one;
        ballSprite.sortingOrder = frontOrder;
        Landed?.Invoke(hit);
    }

    // Is this point on the backboard?
    bool OnBoard(Vector3 p)
    {
        Bounds b = backboard.bounds;
        return p.x >= b.min.x && p.x <= b.max.x && p.y >= b.min.y && p.y <= b.max.y;
    }
}
