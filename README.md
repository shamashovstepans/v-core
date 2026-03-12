# v-core

Unity C# core framework with VContainer DI, feature-based architecture, scopes, widgets, and state management.

## Requirements

- **Unity** 6000.3.5f2 or compatible
- **URP** (Universal Render Pipeline)
- **Input System** (new)

## Tech Stack

- [VContainer](https://github.com/hadashiA/VContainer) – dependency injection
- [UniTask](https://github.com/Cysharp/UniTask) – async/await
- [Newtonsoft.Json](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@latest) – JSON config
- [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest) – UI text
- [UI Toolkit](https://docs.unity3d.com/Packages/com.unity.ui@latest) – UI framework
## Architecture

- **Core** – Config, FeatureManager, State, Scopes, Widgets, Popups
- **Core.Features** – shared feature infrastructure (e.g. NavButtons)
- **Features** – feature modules (Lobby, BattlePass, Puzzles, TestYourLuck, TestFeature)
- **CompositionRoot** – `AppBootstrap` wires DI and installers

## Project Structure

```
Assets/
├── Scripts/
│   ├── Core/           # Framework (Config, State, Scopes, Widgets, Popups)
│   ├── Core.Features/  # Shared NavButtons, etc.
│   ├── Features/       # Feature modules
│   └── CompositionRoot/
├── Resources/
│   ├── Configs/        # JSON configs
│   └── Prefabs/        # UI screens, popups, widgets
├── Scenes/
│   └── main_scene.unity
├── Settings/           # URP assets
└── TextMesh Pro/
```

## Getting Started

1. Clone and open in Unity.
2. Open `Assets/Scenes/main_scene.unity`.
3. Press Play – `AppBootstrap` starts the app and loads features.

## License

MIT © 2026 Stepan Shamashov
