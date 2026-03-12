# Core

Unity C# core module with VContainer DI, scopes, features, widgets, and state.

## Config

- **IConfigProvider** – loads JSON config by key
- **ResourcesConfigProvider** – loads from `Resources/Configs/{key}.json`

## State

- **IStateHandler** – key-value persistence (Exists, Get, Set, Clear, ClearByPrefix)
- **FileSystemStateHandler** – stores under `Application.persistentDataPath/State/`
- **IRepository&lt;TState&gt;** – feature-scoped state with Get, Set, Clear, Updated event
- **FeatureStateRepository** – uses IStateHandler + featureId prefix
- **FeatureStateClearer** – clears feature state, raises Cleared

## Feature Manager

- **IFeatureInstaller** – Id, StateType, GetConfigInstaller(json)
- **FeatureIds** – constants (TestFeature, BattlePass, …)
- **FeatureScopeInstaller** – loads config, registers state/repository, runs feature installer
- **FeaturesStarter** – creates feature scopes via FeatureScopeInstaller

## Scopes

- **IScopeFactory** – CreateScope (manual dispose), AttachScope (disposed with parent)
- **ScopeFactory** – builds child scopes, registers ScopeNode, notifies ScopeTree
- **ScopeNode** – tree node with MainTag, AdditionalTags, Children
- **ScopeTree** – Root node, ScopeRegistered/ScopeRemoved events
- **RegisterMainScopeTag**, **RegisterScopeTag** – bind tags to scope for cheat UI

## Scopes / Cheats

- **IScopeCheatsHandler** – AddAction, AddInfo (injected into scopes)
- **ScopeCheatsRegistry** – collects cheats per scope node
- **ScopeTreeCheatsTabHandler** – drives cheat UI, subscribes to ScopeTree
- **CheatAction**, **ICheatInfo** – cheat definitions
- **ScopeCheatsEditorWindow** – Window → Core cheats (Play Mode)

## Widgets

- **IWidgetView** – MonoBehaviour marker
- **IWidgetInstaller** – Id, Install
- **IWidgetRegistry** – Get(widgetId) → IWidgetInstaller
- **IWidgetFactory** – Create, Attach (creates widget scopes)
- **WidgetIds** – Root, UiToolkit
- **WidgetsStarter** – loads root widget from Resources, creates via WidgetFactory
- **RootWidgetPresenter** – spawns UiToolkit widget under root
- **ICloseHandler** (Widgets/Utils) – Close() for closing widgets/popups

## Popups (Widgets/Popups)

- **IPopupManager** – ShowPopup(id), ShowPopupAsync(id, token)
- **IPopupRegistry** – Register(popupId, installer), Get(popupId)
- **IPopupView** – extends IWidgetView, marker for popup views
- **PopupManager** – glue, delegates to IPopupManagerWidget
- **PopupManagerWidgetPresenter** – implements IPopupManagerWidget, loads Canvas prefabs from Resources/Prefabs/Popups/{id}.prefab, creates popup scope, uses UniTask for ShowPopupAsync
- **RegisterPopup&lt;T&gt;** – registers popup installer and auto-adds "Show {Name}" cheat

## Utils

- **CompositeDisposable** – aggregate IDisposable
- **ReactiveProperty**, **IReadonlyReactiveProperty** – reactive values
- **CompositeInstaller** – compose multiple installers
- **DisposableToken** – disposable with callback
- **ContainerUtils** – container helpers
