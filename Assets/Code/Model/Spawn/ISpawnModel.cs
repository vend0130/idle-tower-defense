using UnityEngine;

namespace Code.Model.Spawn
{
    public interface ISpawnModel
    {
        SidePoints GetSide(Sides side);
        void Init();
        Vector2 HalfScreenSize { get; }
    }
}