# Pitch: ZenGotchi

ZenGotchi turns your study time into food for your pet: every hour away from your phone helps it grow, and a short basketball mini-game is the break between study sessions.

**Unity 6 · Android · portrait, one thumb · solo developer · 3 levels**

## Core loop
A basketball is shot at a hoop with a one-finger swipe: aim, release, score. 6 shots per level, and the level also ends when its timer runs out.
Around it: study (app closed) → coins and a fed pet → Hoops break → shop → evolution → repeat.

## Meta
- **Earn:** 5 coins per study hour. The app saves the time it closed and computes the coins on return (no code runs in the background, Android limits it). Returning after the session ended pays the coins and feeds the pet; opening the app before it ends breaks the session: no coins, and the pet is disappointed. In Hoops, 5 coins per star.
- **Spend:** food and cosmetic skins in the shop. Nothing affects mini-game difficulty.
- **Unlock:** evolution Egg → Baby → Teen → Adult, one-way. Each stage needs 20 study hours (5 days × 4 h) and a fed, happy pet.
- **Rewarded ads:** double coins after a level, or free coins in the shop.

## Why come back?
1. **Study schedule:** the player picks study days and hours. The pet gets hungry on those days, and skipping lowers its mood.
2. **Evolution:** each study week brings the pet closer to its next stage, which unlocks new looks.

## Who and when
Students, at home or in the library. Hoops is the 1–3 minute break between study sessions. Study time itself happens with the app closed.

## Goals
| | Level 1 | Level 2 | Level 3 |
| --- | --- | --- | --- |
| Target | 6/15 | 8/15 | 10/15 |
| Hoop | Static | Slow | Fast |
| Distance | Close, large | Farther | Far, small |
| Wind | None | Light | Strong, shifting |

Stars: target = 1, 80% (12) = 2, 100% (15) = 3.
Moments of progress: level completed, new star record, study session completed, skin bought, pet evolves.

## Feel
- Swish sound and a light haptic tap when the ball goes in; a dull rim sound on a miss.
- The ball follows the swipe speed; a short slow-motion moment on a perfect shot.
- Particles and a coin counter that ticks up when stars are awarded.
- The pet cheers or sulks after each shot, and reacts when tapped on the home screen.

## Scope check
- [x] Hybrid-casual: simple core + a meta layer
- [x] Portrait, one thumb
- [x] 1–3 minute sessions
- [x] Small: about 3 levels (more for a team of 2–3)

**Engineering focus:** two parallel state machines for the pet (growth, mood) · Observer via C# `Action` events, UI only listens · Object Pooling for balls · ScriptableObjects for levels, items and skins · `MiniGameBase` so new mini-games plug in (Open/Closed) · Singleton GameManager and SaveManager, JSON save · offline study time computed on return: close time saved in OnApplicationPause/OnApplicationQuit, checked against network time (NTP) on return, device clock offline (v1 limitation); no background work.

**Stretch:** levels 4–5, more mini-games.
