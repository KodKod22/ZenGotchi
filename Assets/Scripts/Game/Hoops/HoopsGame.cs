using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// HoopsGame.cs: runs one Hoops level: clock, shots, hits, stars, reward, next level.
public class HoopsGame : MonoBehaviour
{
    [SerializeField] BallThrower ball;
    [SerializeField] HoopMover hoop;
    [SerializeField] GameObject swipeArrow;
    [SerializeField] TMP_Text shotsLabel;
    [SerializeField] TMP_Text timeLabel;
    [SerializeField] TMP_Text hitsLabel;

    const string LevelKey = "hoops_level";      // index of the level to play next

    HoopsConfig Hoops => Services.Config.Hoops;
    int level, shotsLeft, hits;
    float elapsed;
    bool inFlight, over;

    void Start()
    {
        level = Mathf.Clamp(Services.Save.GetInt(LevelKey), 0, Hoops.levels.Length - 1);
        shotsLeft = Hoops.shotsPerLevel;
        hoop.Begin(Hoops.levels[level].hoopSpeed);

        ball.Thrown += OnThrown;
        ball.Landed += OnLanded;
        ball.CanThrow = true;
        Show();
    }

    void OnDestroy()
    {
        ball.Thrown -= OnThrown;
        ball.Landed -= OnLanded;
    }

    void Update()
    {
        if (over) return;
        elapsed += Time.deltaTime;
        hoop.Tick(elapsed, Time.deltaTime);
        if (elapsed >= Hoops.levelSeconds && !inFlight) End();   // time is up, no ball in the air
        Show();
    }

    void OnThrown()
    {
        shotsLeft--;
        inFlight = true;
    }

    void OnLanded(bool hit)
    {
        inFlight = false;
        if (hit) hits++;
        if (shotsLeft == 0 || elapsed >= Hoops.levelSeconds) End();
        else ball.CanThrow = true;
    }

    void End()
    {
        over = true;
        ball.CanThrow = false;
        int stars = Hoops.Stars(level, hits);

        if (stars > 0)                          // won: pay and open the next level
        {
            int reward = stars * Services.Config.Economy.coinsPerStar;
            Services.Save.SetInt("coins", Services.Save.GetInt("coins") + reward);
            Services.Save.SetInt(LevelKey, Mathf.Min(level + 1, Hoops.levels.Length - 1));
        }

        Debug.Log($"Hoops: level {level + 1}, hits {hits}, stars {stars}");
        SceneManager.LoadScene("Home");
    }

    int shownShots = -1, shownHits = -1, shownSeconds = -1;   // last values on screen

    // Writes a label only when its value changed: no new strings every frame.
    void Show()
    {
        int seconds = Mathf.CeilToInt(Mathf.Max(0f, Hoops.levelSeconds - elapsed));

        if (shotsLeft != shownShots) { shownShots = shotsLeft; shotsLabel.text = shotsLeft.ToString(); }
        if (hits != shownHits) { shownHits = hits; hitsLabel.text = $"{hits}/{Hoops.levels[level].targetHits}"; }
        if (seconds != shownSeconds) { shownSeconds = seconds; timeLabel.text = seconds.ToString(); }

        if (swipeArrow.activeSelf != ball.CanThrow) swipeArrow.SetActive(ball.CanThrow);
    }
}