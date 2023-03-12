namespace Code.Model.Spawn
{
    public interface ISpawnModel
    {
        SidePoints GetSide(Sides side);
        void Init();
    }
}