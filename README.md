# Console Buttons
Clickable Buttons/Checkboxes/Sliders for C# Console!

NUGGET PACKAGE: 
https://www.nuget.org/packages/ConsoleButtons/2.3.0

### Warning: Console Buttons only work on build versions, it won't work inside Visual Studio/Rider!!!

## Example usage:

```
static void Main(string[] args)
{
    ConsoleClick consoleClick = new ConsoleClick();
    
    consoleClick.Initialize();
    
    //Button constructor that auto detects the COLLIDER WIDTH based on how many characters there are on the text.
    Button button = new Button("Sign Up", 0, 0);
    button.OnHoverOver += () => { button.WriteWithColor(Color.Gray); };
    button.OnHoverStop += () => { button.WriteWithNoColor(); };
    button.OnClick += () => { button.WriteWithColor(Color.Red); Thread.Sleep(50); };
    button.OnHold += () => { button.WriteWithColor(Color.Red); };

    //Checkbox constructor order (text string, marked checkbox char, is initialized as checked, collide with text, X and Y)
    CheckBox checkBox = new CheckBox("Checkbox", 'X', false, true, 0, 0);
    checkBox.OnHoverOver += () => { checkBox.WriteWithColor(Color.Gray); };
    checkBox.OnHoverStop += () => { checkBox.WriteWithNoColor(); };
    checkBox.OnClick += () => { checkBox.WriteWithColor(Color.Cyan); Thread.Sleep(50); };
    checkBox.OnHold += () => { checkBox.WriteWithColor(Color.Red); };

    //Slider constructor order (initial value, max value, slider size, convert to int, filled char, unfilled char, X and Y)
    Slider slider = new Slider(0, 10, 10, false, '█', ' ', 5,5);
    slider.OnHoverOver += () => { slider.WriteWithColor(Color.Gray); };
    slider.OnHoverStop += () => { slider.WriteWithNoColor(); };
    slider.OnHold += () => { slider.WriteWithColor(Color.Red); Console.Write(slider.Value, Color.Aqua); };

    consoleClick.AddToComponents(button);
    consoleClick.AddToComponents(checkBox);
    consoleClick.AddToComponents(slider);

    while (true)
    {
        consoleClick.Update();
    }

    Console.ReadKey();
}
```
