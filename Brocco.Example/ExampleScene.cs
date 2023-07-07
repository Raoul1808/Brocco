namespace Brocco.Example;

public class ExampleScene : Scene
{
    private Player _player;
    
    public override void Load()
    {
        AddToScene(_player = new Player());
    }
    
    public override void Update()
    {
        _player.Update();
    }
}
