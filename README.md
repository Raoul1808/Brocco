# Brocco
A small .NET Library based on FNA to kickstart game projects.
 
The structure of the project is pretty much experimental. It might evolve over time.

## Features
This is a non-exhaustive list of Brocco's currently implemented features.
- Scene Manager
- Automatic Entity Rendering
  - Manual access to rendering code is still possible
- Sound, Font and Texture loading
- Text Rendering
- Keyboard Input Handling
- Gamepad Input Handling
- High-Level Menu Builder API
- Automatic Keyboard Menu Navigation Management

## Building
1. Download and install the .NET 6 SDK
2. Clone this repo and its submodules
3. Build the project

### Running the example
1. Build the example project
2. Grab fnalibs over [here](https://fna.flibitijibibo.com/archive/fnalibs.tar.bz2)
3. Follow [these instructions](https://github.com/FNA-XNA/FNA/wiki/Appendix-E:-Modern-.NET-and-NativeAOT#when-developing) to get the native libraries correctly in place
4. Run

## Usage in other projects
#### The structure of the library is prone to change. Make sure to check back regularly

In your main function, reference `Brocco` and create a `BroccoGame` to run a game loop. You can pass a `BroccoGameSettings` object to adjust the game loop settings.
```csharp
using Brocco;

...

BroccoGame game = new BroccoGame();  // Default settings
game.Run();

...

BroccoGame game = new BroccoGame(new BroccoGameSettings  // Custom settings
{
    ShowMouse = true,
    CanvasSize = new Size(727, 1116),
});
game.Run();
```

Next, create a scene and add it to the scene manager.

```csharp
// SampleScene.cs
using Brocco;

public class SampleScene : Scene
{
    public override void Load()
    {
        // Here you should initialize the scene. This is where you should be loading all your assets.
    }
}

// in your Main function
BroccoGame game = new ...;
SceneManager.Add("Sample Scene", new SampleScene());
game.Run();
```

From here you can add custom entities.

Unlike a real Entity-Component-System architecture, this project simply uses Entities only.

```csharp
// SampleEntity.cs
using Brocco;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

public class SampleEntity : Entity
{
    private SoundEffect _sound;

    public SampleEntity()
    {
        // This is where you should be loading your entity assets.
        // Entities are drawn automatically using protected fields. I invite you to check the Entity.cs file in Brocco to see what you can access.
        CurrentTexture = Assets.GetTexture("SampleTexture");  // Will load SampleTexture.png in your default assets folder
        _sound = Assets.GetSound("SampleSound");  // Will load SampleSound.wav in your default assets folder
        Position = new Vector2(100, 50);
    }
    
    public override void Update()
    {
        // This method runs every frame.
        if (InputManager.GetKeyPress(Keys.Space))  // If we just pressed the space key, play the sound
            _sound.Play();
    }
}

// SampleScene class
public override void Load()
{
    AddToScene(new SampleEntity());
}
```

Scenes and entities are automatically updated and rendered every frame in the background. The scenes and entities update and render code all happens in virtual methods, in case you want to render everything yourself.

## License

This project is licensed under the MIT License.
