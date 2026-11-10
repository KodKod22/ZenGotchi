# Decision log

Add an entry every time the team makes a real choice: a tool, a design, a trade-off.
About 1–2 entries a week. Newest at the top. Write the reasoning yourselves; AI may fix the wording.

## Template

```
## YYYY-MM-DD — [The decision, in a few words]
Options: [A] / [B] / [C]
Chose: [what] — [why]
AI: [what the AI suggested, and what you did with it — or "not used"]
Who: [name(s)]
```

## Example

```
## 2026-11-20 — Save progress locally first
Options: PlayerPrefs / JSON file / Cloud Save
Chose: JSON file now, Cloud Save later
Why: works offline, easy to move later
AI: suggested a SQLite database — rejected, too heavy for one save file
Who: Yuval
```

---

<!-- Your entries start here -->

## 2026-11-10 — Boosters in Hoops: three design-only placeholders

Options: no boosters / boosters earned only by studying / design-only placeholder buttons, logic decided later
Chose: three placeholder booster buttons at the bottom of the Hoops screen: extra shot, extra time, freeze time; design only, no logic yet
Why: boosters help the player reach a higher score in the mini-game
AI: offered the three options and drew the booster icons in the same style as the rest of the art; I chose the placeholders and picked the three boosters myself
Who: Kody

## 2026-11-10 — Minimum API Level 29

Options: API 26 (Unity default) / API 29 / a higher level
Chose: API 29
Why: only a small share of users is left out
AI: set the value in Player Settings; the choice and the reason are mine
Who: Kody

## 2026-11-07 — Home screen: one Play button, shop through the coins

Options: three equal bottom buttons (study times / games / shop) / a game-selection screen before Hoops / one big Play button straight to Hoops
Chose: one big Play button with a ball icon at the bottom, going straight to Hoops; the study-times button moved to the top next to the coins; the shop opens by tapping the coins; the game-selection screen was dropped
Why: matches the lecturer's home-screen pattern; the MVP has only one mini-game, so a selection screen adds a tap for nothing
AI: compared the screens to the lecturer's template and suggested making one button primary and letting the coins open the shop — accepted. Also suggested and built a game-selection screen with locked "coming soon" games — tried, then dropped
Who: Kody

## 2026-11-07 — Hoops screen layout

Options: the pet throws the ball / only the ball appears; basketball-court background / same grass-and-sky as home
Chose: only the ball, at the bottom center, with the hoop above it; HUD at the top: shots left, time left, hits vs target (e.g. 3/6) and a pause button; same grass and sky as the home screen with the same white cards; a blinking up-arrow above the ball instead of hint text; 6 shots per level, and the level also ends when the timer runs out
Why: the swipe starts where the thumb already is; one visual world across screens; no throw animation per evolution stage (an egg can't throw)
AI: suggested showing the pet small in a corner as a spectator — not used. Pointed out that 6 shots conflicts with the pitch's targets (6/8/10 of 15) — targets still open. Changes the pitch: 15 shots → 6, "pet shoots" → only the ball, timer added
Who: Kody

## 2026-11-07 — Showing the pet's state

Options: status bars / icons / speech bubble from the pet
Chose: bars and a speech bubble together: fullness and mood bars always at the top, the bubble pops up only on events. At the Egg stage there is no bubble: the egg wiggles when touched and cracks show progress toward hatching; the bubble starts at Baby. The pet sleeps at night (floating Zzz)
Why: bars are read in half a second, the bubble makes the pet feel alive; an egg can't talk, and cracks show progress without numbers
AI: offered the three options and suggested splitting them (bars = constant state, bubble = events) — accepted. Suggested cracks as the progress display — accepted
Who: Kody

## 2026-11-07 — Home screen layout and world

Options: buttons at the top above the pet / info at the top and actions at the bottom
Chose: grass background with the pet centered; info at the top (coins, settings in the opposite corner, the status bars, a study-session card: "next session in…" / "study time! close the app"); sky follows the device clock (morning / evening / night); scenery: tree, bush, flowers, drifting clouds; no explanatory text on screen
Why: one-thumb portrait game, the top of the screen is hard to reach; the session card puts the core loop (study hours) on the home screen
AI: suggested moving the buttons to the bottom and offered a list of additions (session card, time-of-day sky, scenery, idle animation, shop cosmetics on screen) — took the session card, sky and scenery. Mockup made with Claude: https://claude.ai/artifact/CWmsaMCG2uSXSVmo74ngaW
Who: Kody
