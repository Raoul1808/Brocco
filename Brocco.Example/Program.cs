namespace Brocco.Example;

internal class Program
{
    public static void Main(string[] args)
    {
        using BroccoGame game = new BroccoGame();
        game.Window.Title = "Example Game Made With Brocco";
        game.SetResolution(1280, 720);
        game.Run();
    }
}
