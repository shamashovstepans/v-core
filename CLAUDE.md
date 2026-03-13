# v-core — Claude Context

Unity C# core framework and game project. Use this file as the authoritative reference for architecture, conventions, and workflows.

---

## Project Overview

**Engine:** Unity 6000.3.5f2 · **Render Pipeline:** URP · **Input:** New Input System

A feature-based Unity game with a custom core framework built on VContainer DI and scoped lifecycle management. Features are isolated modules that register themselves through installers and are composed at the app root.

---

## Tech Stack

| Package | Purpose |
|---|---|
| [VContainer](https://github.com/hadashiA/VContainer) | Dependency injection & scoped lifetimes |
| [UniTask](https://github.com/Cysharp/UniTask) | Async/await (use instead of `Task` or coroutines) |
| [Newtonsoft.Json](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@latest) | JSON config deserialization |
| TextMeshPro | UI text |
| UI Toolkit | UI framework |

---

## Assembly Structure

```
Assets/Scripts/
├── CompositionRoot/   # AppBootstrap — wires DI root, no game logic
├── Core/              # Framework: Config, State, FeatureManager, Scopes, Widgets, Popups, Utils
├── Core.Features/     # Shared feature infrastructure (NavButtons)
└── Features/          # Game feature modules (one subfolder per feature)
```

---

## Architecture Patterns

### Dependency Injection (VContainer)

- All bindings go through `IContainerBuilder` in Installer classes.
- Never use `new` for services — resolve through the container.
- Use `Lifetime.Singleton` for services, `Lifetime.Scoped` within a scope.
- `CompositeInstaller` composes multiple installers together.
- `ActionInstaller` is the inline installer (lambda-based).

### Feature Modules

Every feature implements `IFeatureInstaller`:
```csharp
public interface IFeatureInstaller
{
    string Id { get; }              // matches FeatureIds constant
    Type StateType { get; }         // the feature's serializable state class
    IInstaller GetConfigInstaller(string json);  // deserializes config, returns installer
    void Install(IContainerBuilder builder);     // registers services, popups, nav buttons
}
```

Feature IDs are constants in `FeatureIds`. Features are registered in `FeaturesInstaller` and started by `FeaturesStarter` via `FeatureScopeInstaller`.

**To add a new feature:**
1. Add its ID constant to `FeatureIds`.
2. Create a subfolder under `Features/` with `{Name}Installer.cs`, `{Name}State.cs`, `{Name}Config.cs`.
3. Add a JSON config at `Assets/Resources/Configs/{id}.json`.
4. Register in `FeaturesInstaller`.

### Scopes

Scopes are child DI containers with explicit lifecycles.
- `IScopeFactory.CreateScope(...)` — manual dispose.
- `IScopeFactory.AttachScope(...)` — disposed when parent disposes.
- `ScopeTree` tracks the live scope hierarchy; used by the cheat editor window.
- Tag scopes with `RegisterMainScopeTag` / `RegisterScopeTag` for cheat UI visibility.

### Widgets

Widgets are MonoBehaviour-backed UI units with their own scope.
- `IWidgetFactory.Create(id)` / `IWidgetFactory.Attach(id)` create widget scopes.
- Widget IDs are constants in `WidgetIds`.
- `WidgetsStarter` loads the root widget prefab from `Resources/Prefabs/`.
- `IWidgetInstaller` — implemented per-widget, registers its presenter and view.

### Screens & Popups

- Screens live inside feature scopes; opened by their feature's `Starter` class.
- Popups: use `IPopupManager.ShowPopup(id)` / `ShowPopupAsync(id, token)`.
- Register popups with `builder.RegisterPopup<TInstaller>()` — also auto-adds a cheat.
- Popup prefabs must be at `Resources/Prefabs/Popups/{id}.prefab`.

### Presenter / View Pattern

Each UI unit has:
- `I{Name}View` — interface for the MonoBehaviour view.
- `{Name}Presenter` — injected service that drives the view; implements `IStartable` or `IDisposable` as needed.
- `{Name}Installer` — registers both in the widget/popup scope.

### Config

- Configs are JSON files in `Assets/Resources/Configs/{featureId}.json`.
- Loaded via `IConfigProvider` (implementation: `ResourcesConfigProvider`).
- Deserialized with Newtonsoft.Json in `GetConfigInstaller`.

### State Persistence

- `IRepository<TState>` — feature-scoped key-value store (Get, Set, Clear, Updated event).
- `FileSystemStateHandler` — persists under `Application.persistentDataPath/State/`.
- State is automatically scoped per feature via `FeatureScopeInstaller`.

### NavButtons

- NavButton = bottom-nav tab entry for a feature.
- Implement `INavButtonInstaller`, register with `builder.RegisterNavButton<TInstaller>()`.
- NavButton IDs go in `NavButtonIds`.
- View: `I{Name}NavButtonView`, Presenter: `{Name}NavButtonPresenter`.

### Cheats

- Inject `IScopeCheatsHandler` and call `AddAction` / `AddInfo` in scoped services.
- `RegisterPopup<T>` auto-adds a "Show {Name}" cheat.
- Editor window: **Window → Core cheats** (Play Mode only).

### Reactive Utilities

- Use `ReactiveProperty<T>` / `IReadonlyReactiveProperty<T>` for observable values.
- Use `CompositeDisposable` to aggregate subscriptions.
- Prefer `UniTask` for all async work over `Task` or coroutines.

---

## Naming Conventions

- **Interfaces:** `I` prefix — `IFeatureInstaller`, `IPopupView`.
- **Installers:** `{Name}Installer`.
- **IDs files:** `{Domain}Ids` (e.g. `FeatureIds`, `WidgetIds`, `NavButtonIds`).
- **Config classes:** `{Name}Config`.
- **State classes:** `{Name}State`.
- **All files/types:** PascalCase. Namespaces match assembly name (`Core`, `Core.Features`, `Features`, `CompositionRoot`).
- **JSON config keys:** snake_case (e.g. `battle_pass.json`).

---

## Workflow Rules

- **Never use `new` for injected services** — always resolve via DI.
- **Always use `UniTask`** for async code, not `Task` or coroutines.
- **Prefer `IDisposable` + `CompositeDisposable`** for subscription cleanup.
- **Register every new popup via `RegisterPopup<T>`** (not manually).
- **Keep `AppBootstrap` free of game logic** — it only wires installers.
- **State keys are auto-prefixed** by feature ID — never manually prefix state keys.
- **Do not add Unity `[SerializeField]` to injected dependencies** — use constructor injection via VContainer.

---

## Key Entry Points

| File | Role |
|---|---|
| `CompositionRoot/AppBootstrap.cs` | App root; builds the DI container on `OnEnable` |
| `Core/CoreInstaller.cs` | Registers all core services |
| `Core.Features/CoreFeaturesInstaller.cs` | Registers shared feature infrastructure |
| `Features/FeaturesInstaller.cs` | Registers all game features |
| `Core/FeatureManager/FeaturesStarter.cs` | Creates a scope per feature on startup |
| `Assets/Scenes/main_scene.unity` | The only scene; Press Play to run |
