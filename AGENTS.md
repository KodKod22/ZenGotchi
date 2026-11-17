# AGENTS.md — context for AI coding tools

AI tools read this file before working on the repo. Keep it short, true and up to date.

## The game
ZenGotchi is a hybrid-casual virtual pet for students: time spent studying with the app
closed feeds the pet and earns coins, and a short basketball mini-game (Hoops) is the break
between study sessions. Core loop: study (app closed) → coins and a fed pet → Hoops break →
shop → evolution (Egg → Baby → Teen → Adult) → repeat. Android, portrait, one thumb.
Solo project for the Mobile Game Development course at Shenkar. Full design: `pitch.md`.

## Tech
- Engine: Unity 6 (6000.6.7f1), C#, URP 2D
- Target: Android, portrait, min API 29, package `com.kodyagin.zengotchi`
- UI: uGUI + TextMeshPro, Canvas Scaler reference 1080×1920
- Input: Input System package
- Art: Kenney packs (CC0) in `Assets/Art/Kenney/`, own sprites in `Assets/Art/Sprites/`
- Version control: Git with LFS for images, audio and fonts (see `.gitattributes`)

## Architecture
**Scenes** (Scene List order):
1. `Bootstrap`: only the `Bootstrap` script. Starts every service, then loads Home.
2. `Home`: the pet, coins, hunger and mood bars, next session card, Settings panel.
3. `Hoops`: the basketball mini-game screen (layout only for now).

In the Editor, `Assets/Editor/PlayFromBootstrap.cs` makes every Play start from Bootstrap.

**Services**: plain C# classes behind interfaces, reached through the static `Services` class:
| Service | Interface | Implementation | Stores |
| --- | --- | --- | --- |
| Save | `ISaveService` | `PlayerPrefsSaveService` | key → int, saved immediately |
| Settings | `ISettingsService` | `SettingsService` | sound, music, effects, vibration (through Save) |

**Init order** (in `Bootstrap.Awake`): Save → Settings (Settings needs Save) → load Home.

**Assemblies**:
- `ZenGotchi.Services` (`Assets/Scripts/Services/`): no references to game code.
- `ZenGotchi.Game` (`Assets/Scripts/Game/`): references Services, TextMeshPro, UnityEngine.UI.

## Rules for AI
- Services never reference Game. Never add `ZenGotchi.Game` to the Services assembly.
- Screens never call `PlayerPrefs` directly. Use `Services.Save` or another service.
- A new service = an interface + an implementation in `Scripts/Services/`, a property on
  `Services`, and one line in `Bootstrap.Awake` in the right order. Update this file.
- Bootstrap stays first in the Scene List.
- Follow the existing folder structure and naming. Don't create new top-level folders.
- Don't invent APIs. If unsure about an SDK or Unity API, say so and point to the docs.
- Don't change Player Settings, package name, API levels or build settings unless asked.
- Don't edit Kenney files in place; copy what's needed and add new art to `Art/Sprites/`.
- Keep changes small and explain what you changed and why.
- Ask before changing project files; verify instead of guessing.
- Never commit secrets (keystores, passwords, API keys).

## Conventions
- C# classes in the global namespace, one class per file, file name = class name.
- Interfaces start with `I` (`ISaveService`).
- Game scripts in `Scripts/Game/`, UI helpers in `Scripts/Game/UI/`, services in `Scripts/Services/`.
- Save keys are camelCase strings (`coins`, `soundOn`).
- UI buttons are wired in the Inspector (On Click), not with `AddListener` in code.
- Decisions go in `DECISIONS.md`, newest first.
