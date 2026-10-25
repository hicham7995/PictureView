+------------------------------------------------------+
| MainWindow                                           |
|------------------------------------------------------|
| + DataContext : MainWindowViewModel                   |
| + MenuItem "Open" Command="{Binding OpenCommand}"     |
+------------------------------------------------------+
                         |
                         | 1 : bind
                         v
+------------------------------------------------------+
| MainWindowViewModel                                   |
|------------------------------------------------------|
| + OpenCommand : ICommand                              |
| + BrowseCommand : RelayCommand                        |
| + QuitCommand : RelayCommand                           |
| + Tabs : ObservableCollection<object>                |
| + SelectedTab : object?                              |
|------------------------------------------------------|
| + MainWindowViewModel(fileDialogService : IFileDialogService) |
| + OpenFiles()                                         |
| + ShowExplorerTab()                                   |
+------------------------------------------------------+
                         |
                         | uses
                         v
+------------------------------------------------------+
| IFileDialogService                                    |
|------------------------------------------------------|
| + OpenFiles(filter : string, multiSelect : bool) : string[] |
+------------------------------------------------------+
                         ^
                         |
                         | implements
                         v
+------------------------------------------------------+
| FileDialogService                                     |
|------------------------------------------------------|
| + OpenFiles(filter : string, multiSelect : bool) : string[] |
|------------------------------------------------------|
| - uses Microsoft.Win32.OpenFileDialog                 |
+------------------------------------------------------+

                         |
                         | returns selected file paths
                         v
+------------------------------------------------------+
| RelayCommand                                          |
|------------------------------------------------------|
| - _execute : Action<object?>                          |
| - _canExecute : Predicate<object?>?                  |
|------------------------------------------------------|
| + CanExecute(parameter) : bool                       |
| + Execute(parameter)                                  |
| + event CanExecuteChanged                             |
+------------------------------------------------------+

                         |
                         | executes
                         v
+------------------------------------------------------+
| MainWindowViewModel.OpenFiles()                       |
|------------------------------------------------------|
| 1. fileDialogService.OpenFiles(filter, true)          |
| 2. foreach selected file                              |
|    a. create new ImageTabViewModel(file)               |
|    b. Tabs.Add(tab)                                   |
|    c. SelectedTab = tab                                |
+------------------------------------------------------+

                         |
                         | creates
                         v
+------------------------------------------------------+
| ImageTabViewModel                                     |
|------------------------------------------------------|
| + Title : string                                      |
| + Image : ImageSource?                                 |
|------------------------------------------------------|
| + ImageTabViewModel(path : string)                    |
+------------------------------------------------------+