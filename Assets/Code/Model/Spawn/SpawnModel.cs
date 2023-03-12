using UnityEngine;

namespace Code.Model.Spawn
{
    public class SpawnModel : ISpawnModel
    {
        private readonly Camera _camera;
        private const float Offset = .5f;

        private SidePoints[] _spawnSides;

        public SpawnModel(Camera camera) =>
            _camera = camera;

        public void Init()
        {
            Vector2 halfScreenSize =
                _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            _spawnSides = new[]
            {
                GetTop(halfScreenSize), GetLeft(halfScreenSize), GetRight(halfScreenSize), GetBottom(halfScreenSize)
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