# Console Buttons
NUGGET PACKAGE:
Clickable Buttons/Checkboxes for C# Console!

https://www.nuget.org/packages/ConsoleButtons/2.2.4

### Warning: Console Buttons only work on build versions, it won't work inside Visual Studio/Rider!!!

## Example usage:

```
static void Main(string[] args)
{ 
  ConsoleClick consoleClick = new ConsoleClick();

  bool initialized = false;
  //Main program loop for buttons (logic, etc)
  while (true)
  {
    //Update
    consoleClick.Update();
  
    //Initialization
    if (initialized)
      continue;
  
    //Button constructor that auto detects the COLLIDER WIDTH based on how many characters there are on the text.
    Button button = new Button("Sign Up", 0, 0, consoleClick.WindowRect);
    button.OnHoverOver += () => { button.WriteWithColor(Color.Gray); };
    button.OnHoverStop += () => { button.WriteWithNoColor(); };
    button.OnClick += () => { button.WriteWithColor(Color.Red); Thread.Sleep(50); };
    button.OnHold += () => { button.WriteWithColor(Color.Red); };
  
    //Checkbox constructor order (text string, marked checkbox char, is initialized as checked, collide with text, x and y.
    CheckBox checkBox = new CheckBox(text, 'X', false, true, 0, 0, consoleClick.WindowRect);
    checkBox.OnHoverOver += () => { checkBox.WriteWithColor(Color.Gray); };
    checkBox.OnHoverStop += () => { checkBox.WriteWithNoColor(); };
    checkBox.OnClick += () => { checkBox.WriteWithColor(Color.Cyan); Thread.Sleep(50); };
    checkBox.OnHold += () => { checkBox.WriteWithColor(Color.Red); };
  
    //Necessary to add the button to the update list.
    consoleClick.AddToComponents(checkBox);
    consoleClick.AddToComponents(button);
    
    initialized = true;
  }

  Console.ReadKey();
}
```
