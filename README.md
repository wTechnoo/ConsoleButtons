<h1 align="center" id="title">Console Buttons</h1>

<p align="center"><img src="https://socialify.git.ci/wTechnoo/ConsoleButtons/image?description=1&font=Source%20Code%20Pro&language=1&logo=https%3A%2F%2Fcdn.discordapp.com%2Fattachments%2F602356897726857216%2F922317460802273310%2FCBLogo_4.png&owner=1&pattern=Diagonal%20Stripes&stargazers=1&theme=Dark" alt="project-image"></p>

<p align="center" id="description">Clickable UI for C# Console</p>
<p align="center" id="description">Buttons, checkboxes, sliders and more!</p>

<h2 align="center">⚠️ Warning</h2>
<p align="center" id="description">Console buttons will only work when launched through the built .EXE on RELEASE mode</p>

<h2 align="center">✔️ Changes (24/12/21)</h2>
<p align="center" id="description">1. Removed Colorful.Console dependency</p>
<p align="center" id="description">2. Added proper WriteLine and Clear that works as expected (default Console.WriteLine wasn't producing proper results)</p>
<p align="center" id="description">3. Renamed some of the scripts</p>

<h2 align="center">❔ To be worked on</h2>
<p align="center" id="description">1. Adding a way to the user to use their own/downloaded console packages (such as Colorful.Console or any other color package)</p>

<h2 align="center"> 📌<a href="https://www.nuget.org/packages/ConsoleButtons/2.4.0">Package</a></h2>
<p align="center" id="description">Download the package through nugget or build it yourself!</p>

<h2 align="center">⌨️ Example Usage</h2>


```csharp
static void Main(string[] args)
{
    UIManager manager = new UIManager();
    
    //Button constructor that auto detects the COLLIDER WIDTH based on how many characters there are on the text.
    Button button = new Button("Sign Up", 0, 0);
    button.OnHoverOver += () => { button.WriteWithColor(ConsoleColor.Gray); };
    button.OnHoverStop += () => { button.WriteWithNoColor(); };
    button.OnClick += () => { button.WriteWithColor(ConsoleColor.Red); Thread.Sleep(50); };
    button.OnHold += () => { button.WriteWithColor(ConsoleColor.Red); };

    //Checkbox constructor order (text string, marked checkbox char, is initialized as checked, collide with text, X and Y)
    CheckBox checkBox = new CheckBox("Checkbox", 'X', false, true, 0, 0);
    checkBox.OnHoverOver += () => { checkBox.WriteWithColor(ConsoleColor.Gray); };
    checkBox.OnHoverStop += () => { checkBox.WriteWithNoColor(); };
    checkBox.OnClick += () => { checkBox.WriteWithColor(ConsoleColor.Cyan); Thread.Sleep(50); };
    checkBox.OnHold += () => { checkBox.WriteWithColor(ConsoleColor.Red); };

    //Slider constructor order (initial value, max value, slider size, convert to int, filled char, unfilled char, X and Y)
    Slider slider = new Slider(0, 10, 10, false, '█', ' ', 5,5);
    slider.OnHoverOver += () => { slider.WriteWithColor(ConsoleColor.Gray); };
    slider.OnHoverStop += () => { slider.WriteWithNoColor(); };
    slider.OnHold += () => { slider.WriteWithColor(ConsoleColor.Red); Console.Write(slider.Value, ConsoleColor.Aqua); };

    manager.AddToComponents(button);
    manager.AddToComponents(checkBox);
    manager.AddToComponents(slider);

    while (true)
    {
        manager.Update();
    }

    Console.ReadKey();
}
```
