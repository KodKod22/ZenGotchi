# ZenGotchi

**Team:** kody agin peters
**Course:** Mobile Game Development, Shenkar

## The game in one sentence
Study with the app closed to feed your pet and earn coins, then take a short basketball
break in Hoops — and come back tomorrow because your pet is hungry on your study days
and grows with every study week.

## How to open and build
1. Open the project in Unity 6 LTS (the exact version is on Moodle).
2. File → Build Profiles → Android → Switch Platform.
3. Build and run on an Android phone.

## Architecture
![Architecture: scenes, services and init order](docs/architecture.png)

Bootstrap is the first scene. It starts the services in order (Save, then Settings,
which needs Save) and then loads Home. Screens reach the services only through
`Services`; details and rules in `AGENTS.md`.

## Economy
Every economy number lives in `Assets/Config/EconomyConfig.asset`, read through
`Services.Config`. The numbers come from the economy sheet:
[ZenGotchi economy sheet](https://docs.google.com/spreadsheets/d/1a0pGj1G0ExBrS3PheG2zJquY8BN0ihHaOKbcV0LCAM8/edit)

## Repo map
| File | What it is |
| --- | --- |
| `pitch.md` | One-page pitch of the game |
| `DECISIONS.md` | Every real decision the team made, and why |
| `AGENTS.md` | Context for AI coding tools: architecture, rules, conventions |
| `docs/architecture.png` | Architecture diagram: scenes, services, init order |
| `CREDITS.md` | Sources and licenses of the art |
