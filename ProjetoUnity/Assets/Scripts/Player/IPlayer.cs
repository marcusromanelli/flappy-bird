
public interface IPlayer : IStartable
{
    public void Setup(IGameController gameController, SkinData[] availableSkins);
    public void Flap();
}
