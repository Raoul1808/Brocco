using Brocco.Basic;

namespace Brocco.Example;

internal class Program
{
    public static void Main(string[] args)
    {
        using BroccoGame game = new BroccoGame(new BroccoGameSettings
        {
            ShowMouse = true,
            CanvasSize = new Size(384, 216),
        });
        game.Window.Title = "Example Game Made With Brocco";

        SceneManager.Add("Example Scene", new ExampleScene());
        
        game.Run();
    }
}
