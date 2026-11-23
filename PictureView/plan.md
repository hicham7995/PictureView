## Feature one
On each tab add a close icon on the top right
The icon should be a builtin close icon the 'x' icon
Add the behavior in the ImageTabViewModel
Add also relevant test for the Feature
## Feature two
  The MenuItem Recent File located in the MainWindow.xaml
  Each time a open a file i would like to add up to last 5 open files in this menu so I can open them direclty
  The behavior to be added in the MainWindowViewModel
  Add the tests as usual
## Feature three
  On the MainWindow when I click the close menu button it should close the selected tab
  The behavior to be added in the MainWindowViewModel
  Add the tests as usual
## Feature Four
  On the MainWindow when I click the "close all" button it should close all the opened tabs
  The behavior to be added in the MainWindowViewModel
  Add the tests as usual
## Feature Five
  On the MainWindow when I click the "Save" button it should save to disk the opened file.
  If There are multiple tabs, the "Save" button should save the selected tab
  Add the covering tests as usual
## Feature Six
  On the MainWindow when I click the "Save As" button it should display a "Save As" dialog box
  The dialog box should point to "Document" folder by default, or the last used folder if already used.
  On the type of file, it should suggest current list to use : PNG - Portable Network Graphic, TIF-TIFF Rev 6, WebP-Webp,AVIF-Avif,Bmp-Windows Bitmap RGB 565,JPG-JPEGLI,ICO-Windows Icon
  When a format is selected in the list, the file should use this format to be saved with, for example if I select JPG it will save the file to Jpg format
  Add the covering tests as usual
## Feature Seven
  On the MainWindow when I click the "Print" button it should display the "Print" dialog box with the preview of the image to be printed, the print to use
  And when I click ok it should print the image
  Add the covering tests as usual
  