public interface IStage : IStartable
{
    public void Setup(StageData stageData);
    public void Destroy();
    public ScreenflashData GetScreenflashData();
}
