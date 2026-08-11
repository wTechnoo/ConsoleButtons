<h1 align="center" id="title">Console Buttons</h1>

<p align="center"><img src="https://socialify.git.ci/wTechnoo/ConsoleButtons/image?description=1&font=Source%20Code%20Pro&language=1&logo=https%3A%2F%2Fcdn.discordapp.com%2Fattachments%2F602356897726857216%2F922317460802273310%2FCBLogo_4.png&owner=1&pattern=Diagonal%20Stripes&stargazers=1&theme=Dark" alt="project-image"></p>

<p align="center" id="description">Clickable UI for C# Console</p>
<p align="center" id="description">Buttons, checkboxes, sliders and more!</p>

<h2 align="center">⚠️ Warning</h2>
<p align="center" id="description">Console buttons will only work when launched through the built .EXE on RELEASE mode</p>

<h2 align="center">✔️ Changes (3.0.0) — automatic sizing</h2>
<p align="center" id="description">Colliders are now measured, not hand-tuned. Everything works in console cells instead of pixels.</p>
<p align="center" id="description">1. <b>No more width/height arguments.</b> A component's collider is measured from exactly what it paints, so it can never drift away from its text</p>
<p align="center" id="description">2. <b>No more hardcoded font sizes.</b> The character cell is measured from the window's client area, so it stays correct across font changes, DPI scaling and resizes</p>
<p align="center" id="description">3. Wide text is measured properly — CJK and emoji paint two cells each, which <code>string.Length</code> gets wrong</p>
<p align="center" id="description">4. Multi-line text is supported: every line is placed at the component's X, and height comes from the line count</p>
<p align="center" id="description">5. Hover events are edge-triggered, so <code>OnHoverStop</code> no longer fires every frame on every component</p>

<h2 align="center">⚠️ Upgrading from 2.x</h2>
<p align="center" id="description">1. <code>new Button(text, x, y, w, h)</code> still compiles but the w and h are ignored — drop them</p>
<p align="center" id="description">2. <code>UIComponent</code> is now abstract; custom components implement <code>protected override string Render()</code> and get their collider for free</p>
<p align="center" id="description">3. <code>AABB</code> is in cells now, not pixels. <code>UICollision</code> takes a column and a row</p>
<p align="center" id="description">4. A slider's <code>[</code> now sits on the X you give it rather than one column to its left (so x: 0 no longer throws)</p>

<h2 align="center">❔ To be worked on</h2>
<p align="center" id="description">1. Adding a way to the user to use their own/downloaded console packages (such as Colorful.Console or any other color package)</p>

<h2 align="center"> 📌<a href="https://www.nuget.org/packages/ConsoleButtons/2.4.0">Package</a></h2>
<p align="center" id="description">Download the package through nugget or build it yourself!</p>

<h2 align="center">⌨️ Example Usage</h2>


```csharp
static void Main(string[] args)
{
    UIManager manager = new UIManager();
    
    //Button constructor order (text string, X and Y). The collider is measured from the text.
    Button button = new Button("Sign Up", 0, 0);
    button.OnHoverOver += () => { button.WriteWithColor(ConsoleColor.Gray); };
    button.OnHoverStop += () => { button.WriteWithNoColor(); };
    button.OnClick += () => { button.WriteWithColor(ConsoleColor.Red); Thread.Sleep(50); };
    button.OnHold += () => { button.WriteWithColor(ConsoleColor.Red); };

    //Checkbox constructor order (text string, marked checkbox char, is initialized as checked, collide with text, X and Y)
    //Without the "collide with text" argument only the [ ] box is clickable.
    CheckBox checkBox = new CheckBox("Checkbox", 'X', false, true, 0, 2);
    checkBox.OnHoverOver += () => { checkBox.WriteWithColor(ConsoleColor.Gray); };
    checkBox.OnHoverStop += () => { checkBox.WriteWithNoColor(); };
    checkBox.OnClick += () => { checkBox.WriteWithColor(ConsoleColor.Cyan); Thread.Sleep(50); };
    checkBox.OnHold += () => { checkBox.WriteWithColor(ConsoleColor.Red); };

    //Slider constructor order (initial value, max value, slider size, convert to int, filled char, unfilled char, X and Y)
    //"size" is the number of cells between the brackets, so this one is 12 cells wide in total.
    Slider slider = new Slider(0, 10, 10, false, '█', ' ', 5, 5);
    slider.OnHoverOver += () => { slider.WriteWithColor(ConsoleColor.Gray); };
    slider.OnHoverStop += () => { slider.WriteWithNoColor(); };
    slider.OnHold += () => { slider.WriteWithColor(ConsoleColor.Red); };

    manager.AddToComponents(button);
    manager.AddToComponents(checkBox);
    manager.AddToComponents(slider);

    while (true)
    {
        manager.Update();
    }
}
```

<h2 align="center">🧩 Custom components</h2>
<p align="center" id="description">Override <code>Render()</code> with whatever the component paints. That single method drives both the drawing and the collider, so they stay in sync on their own.</p>

```csharp
public class Label : UIComponent
{
    string _text;

    public Label(string text, int x, int y)
    {
        _text = text;
        ConsolePosition = new Point(x, y);
    }

    protected override string Render() => $"< {_text} >";

    //Optional: make only part of the render clickable. Defaults to all of it.
    //protected override string HitArea() => _text;
}
```
