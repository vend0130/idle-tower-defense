using UnityEngine;

namespace Code.Model.Spawn
{
    public class SpawnModel : ISpawnModel
    {
        public Vector2 HalfScreenSize { get; private set; }

        private const float Offset = .5f;

        private readonly Camera _camera;

        private SidePoints[] _spawnSides;

        public SpawnModel(Camera camera) =>
            _camera = camera;

        public void Init()
        {
            HalfScreenSize =
                _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            _spawnSides = new[]
            {
                GetTop(HalfScreenSize), GetLeft(HalfScreenSize), GetRight(HalfScreenSize), GetBottom(HalfScreenSize)
            };
        }

        public SidePoints GetSide(Sides side) =>
            _spawnSides[(int)side];

        //note: top of the screen plus offset...
        private SidePoints GetTop(Vector2 halfScreenSize) =>
            new SidePoints(-halfScreenSize.x - Offset,
                halfScreenSize.x + Offset, halfScreenSize.y + Offset);

        private SidePoints GetLeft(Vector2 halfScreenSize) =>
            new SidePoints(halfScreenSize.y + Offset,
                -halfScreenSize.y - Offset, -halfScreenSize.x - Offset);

        private SidePoints GetRight(Vector2 halfScreenSize) =>
            new SidePoints(halfScreenSize.y + Offset,
                -halfScreenSize.y - Offset, halfScreenSize.x + Offset);

        private SidePoints GetBottom(Vector2 halfScreenSize) =>
            new SidePoints(-halfScreenSize.x - Offset,
                halfScreenSize.x + Offset, -halfScreenSize.y - Offset);
    }
}