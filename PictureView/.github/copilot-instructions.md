# Copilot instructions for PictureView

## Project overview
- This repository is a Windows desktop WPF application built with .NET 10.
- The main app project is `PictureView/`.
- The test project is `PictureViewerTests/`.
- The app uses Prism for dependency injection and MVVM patterns.
- UI is defined in XAML (`MainWindow.xaml`) and interactions are mostly handled in code-behind or via view models.

## Architecture
- Keep business logic in `PictureView/ViewModels/`.
- Keep UI-specific behavior in `PictureView/MainWindow.xaml` and `MainWindow.xaml.cs`.
- Prefer MVVM and commands over direct imperative logic in the view.
- Use `RelayCommand` and view-model state updates instead of embedding complex logic in XAML event handlers when possible.
- File and system operations should go through services such as:
  - `PictureView/Dialogs/IFileDialogService.cs`
  - `PictureView/Services/`


## Coding conventions
- Use C# with nullable enabled.
- Favor clear, explicit naming and small, focused methods.
- Keep event handlers minimal; if the code handles app logic, move it into a view model or service.
- Preserve existing naming conventions already used in the project.
- Use `ObservableCollection`, `BindableBase`, and Prism patterns where appropriate.
- Avoid adding unnecessary dependencies or new frameworks.

## WPF / XAML guidance
- Prefer binding to view-model properties instead of manually manipulating controls in code.
- If you add new XAML controls, keep them consistent with the existing style and layout patterns.
- For `TreeView`, `ListView`, and tab content, ensure data context and templates remain consistent with the current hierarchical structure.
- If a UI event is required, it should typically be a lightweight dispatcher to a view model/service call rather than heavy logic inline.

## Testing
- Tests live in `PictureViewerTests/` and use NUnit.
- Prefer small, focused tests for view models and command behavior.
- Use `Assert.That(...)` style assertions and keep tests readable.
- For WPF UI tests, ensure they run on the STA thread and do not depend on timing races.
- Do not introduce flaky UI tests; if a scenario needs interaction, drive the actual UI elements in a deterministic way.

## Validation
- Build and validate with:
  - `dotnet test PictureView.sln`
- Before finishing a change, verify the relevant tests still pass.
- If you change a behavior in the UI, add or update a corresponding test in `PictureViewerTests/`.
- Ask questions before writing code if anything is unclear
- Do not remove existing code unless explicitly asked

## Project-specific notes
- The project targets `net10.0-windows`.
- `Prism.Wpf` and `Prism.Unity` are already used.
- `StyleCop.Analyzers` is enabled; keep code style and naming consistent.
- Use `MainWindowViewModel` and related view models as the source of app behavior when possible.

## Do not
- Do not add broad architectural rewrites without a clear reason.
- Do not change the app from WPF/Prism patterns to another framework.
- Do not remove existing tests or rename public APIs without updating callers.
- Do not add heavy dependency injection patterns that are inconsistent with the current Prism-based design.
- No code behind in the views, unless asked