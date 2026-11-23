# Copilot prompts for PictureView

This repository is a WPF desktop application built with .NET 10 and Prism. The main app is in `PictureView/`, and automated tests live in `PictureViewerTests/`.

## Repository context
- App project: `PictureView/`
- Test project: `PictureViewerTests/`
- Target framework: `net10.0-windows`
- UI framework: WPF
- Dependency injection / MVVM: Prism
- Test framework: NUnit
- Main window: `PictureView/MainWindow.xaml` and `MainWindow.xaml.cs`
- View models: `PictureView/ViewModels/`

## Coding rules for this project
- Keep UI logic minimal and prefer MVVM patterns.
- Use view model properties and commands for behavior when possible.
- For file system work, prefer the existing service abstractions in `PictureView/Dialogs` and `PictureView/Services`.
- Preserve the current Prism-based architecture and dependency injection patterns.
- Keep naming consistent with the existing codebase.
- Use nullable reference types and modern C# syntax.
- Prefer small, focused changes rather than large refactors.

## Recommended prompt templates

### 1. Fix a bug
Prompt:
```
Fix the bug in this WPF Prism project without changing unrelated behavior.

Project context:
- .NET 10 WPF app
- Prism MVVM
- tests in PictureViewerTests/

Please:
1. Identify the root cause
2. Patch the smallest relevant code path
3. Keep the fix consistent with the existing MVVM and Prism patterns
4. Add or update tests if the bug is user-visible or logic-based

Files to inspect first:
- PictureView/
- PictureViewerTests/
```

### 2. Add a new view model feature
Prompt:
```
Add a new feature to the app in the MVVM layer.

Requirements:
- Follow the existing Prism + WPF patterns used in the project
- Add or update the relevant view model and command logic
- Keep UI code-behind minimal
- Ensure the feature is testable
- Add NUnit tests for the new behavior if applicable

Use the existing project structure:
- PictureView/ViewModels/
- PictureView/MainWindow.xaml
- PictureView/MainWindow.xaml.cs
```

### 3. Write or fix a WPF unit test
Prompt:
```
Write a deterministic WPF test for this project.

Important:
- Use STA threads for any UI interaction
- Do not rely on random timing or sleeps
- Prefer asserting the final state rather than the event itself
- Keep tests in `PictureViewerTests/`
- Use NUnit conventions already present in the project

Target behavior:
[Describe the exact behavior to test]
```

### 4. Refactor without breaking behavior
Prompt:
```
Refactor this code with a minimal, safe change set.

Constraints:
- Preserve the current architecture and public behavior
- Do not change the WPF/Prism patterns used in the project
- Keep the refactor small and readable
- Ensure all existing tests still pass
- Update only the files directly affected by the refactor
```

### 5. Investigate failing tests
Prompt:
```
Investigate the failing test in this repository.

Please:
1. Identify the failing test and root cause
2. Explain the underlying issue in plain language
3. Patch the code with the smallest safe fix
4. Validate with the relevant test(s)
5. Do not broaden the scope beyond the failing behavior
```

### 6. Add command behavior
Prompt:
```
Implement the missing command behavior for this feature in the existing MVVM pattern.

Requirements:
- Use RelayCommand or Prism command patterns already used in the project
- Keep the logic in the view model/service layer when possible
- Ensure the command is testable
- Add or update NUnit tests for success and edge cases
```

### 7. Fix WPF event handling
Prompt:
```
Fix the WPF event handling in this desktop app.

Requirements:
- Keep the event handler lightweight
- Prefer invoking a view model/service instead of embedding app logic in code-behind
- Ensure the handling works correctly on the UI thread
- Add a test covering the behavior if it is user-visible or business-critical
```

## Test commands
Use the smallest relevant validation command:
```bash
dotnet test PictureView.sln
```

For a single test project:
```bash
dotnet test PictureViewerTests/PictureViewerTests.csproj
```

## Preferred work style
- Start by reading the smallest relevant files
- Confirm the root cause before patching
- Keep the fix localized
- Validate with the relevant test or build
- Avoid speculative rewrites or unrelated cleanup

## Special repo notes
- The app includes a `TreeView` and a file explorer pattern.
- `MainWindow.xaml` has custom event handlers like `MouseLeftButtonUp`.
- Some UI tests require careful handling of WPF threading and layout.
- Prefer deterministic tests over UI automation tricks when possible.
