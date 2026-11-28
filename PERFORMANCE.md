# Performance — Milestone 5

**Phone:** Samsung Galaxy A72 (SM-A725F), Android 14
**Moment measured:** 30 seconds inside a Hoops level (no scene change), Development Build, Profiler connected over USB

## Before: what the profiler showed
- Frame time: median 33.4 ms (30 fps, Unity's Android default). Slowest frame 35.5 ms, almost all of it waiting (GfxDeviceVK.Present, WaitForTargetFPS). Not CPU- or GPU-bound.
- Garbage: about 78 B in a normal frame (48 B from URP itself); the HUD already updates its labels only when a value changes.
- Draw calls: 24 (7 SetPass calls).
- Biggest textures: hills 400 KB, backboard 234 KB, egg 195 KB, all RGBA Compressed ETC2.

## The problem
What the player sees: nothing yet, but on a phone with little free memory Android can close the game without warning, especially when the player leaves it for a moment.
Cause: every texture was compressed as ETC2 (8 bits per pixel with alpha) instead of ASTC, and Max Size was left at the 2048 default.

## The fix
- Build Profile → Android → Texture Compression: ETC2 → ASTC (whole project).
- The 3 biggest textures: Override for Android, ASTC 6x6, Max Size 1024 / 1024 / 512, mipmaps and Read/Write off.
- Application.targetFrameRate = 30 in Bootstrap.Awake (see DECISIONS.md).

## Before / after

| Texture | Before (ETC2) | After (ASTC 6x6) |
|---|---|---|
| hills | 400.0 KB | 179.0 KB |
| backboard | 234.4 KB | 104.7 KB |
| egg | 195.3 KB | 87.9 KB |
| **Total** | **829.7 KB** | **371.6 KB (−55%)** |

Frame time after: median 33.5 ms, slowest 36.2 ms, still mostly waiting; no GC.Collect in the whole capture. Unchanged, as expected: the fix is about memory, not time.

## Screenshots

### Before
Frame time across the capture:
![Before: frame time](docs/performance/before-frame-time.png)

A normal gameplay frame (78 B of garbage, no GC):
![Before: gameplay frame](docs/performance/before-gameplay-frame-gc.png)

Draw calls (Rendering module):
![Before: draw calls](docs/performance/before-draw-calls.png)

Textures (Inspector, Android):
![Before: hills](docs/performance/before-hills.png)
![Before: backboard](docs/performance/before-backboard.png)
![Before: egg](docs/performance/before-egg.png)

Side note: the slowest frame in the first capture (35.9 ms, 46 KB of garbage) was the scene change from Hoops back to Home, a loading frame, so it is not counted above:
![Before: scene change spike](docs/performance/before-scene-change-spike.png)

### After
Frame time and allocations across the capture:
![After: frame time and GC](docs/performance/after-frame-time-gc.png)

Textures (Inspector, Android):
![After: hills](docs/performance/after-hills.png)
![After: backboard](docs/performance/after-backboard.png)
![After: egg](docs/performance/after-egg.png)
