using System.Collections.Generic;
using Code.Factories.AssetsManagement;
using Code.Game.Arrow;
using UnityEngine;
using Zenject;

namespace Code.Factories.Arrow
{
    public class ArrowFactory : IInitializable, IArrowFactory, IArrowPoolable
    {
        private readonly Vector2 _defaultPosition = new Vector2(-100, 0);
        private readonly IAssetsProvider _assetsProvider;
        private readonly List<ArrowView> _arrows = new List<ArrowView>();

        private GameObject _arrowPrefab;

        public ArrowFactory(IAssetsProvider assetsProvider) =>
            _assetsProvider = assetsProvider;

        public void Initialize() =>
            _arrowPrefab = _assetsProvider.Warmup(AssetPath.ArrowPath);

        public void Spawn(Transform attacker, float damage, Vector2 point, Quaternion rotation, float speed)
        {
            ArrowView arrow;

            if (_arrows.Count > 0)
            {
                arrow = _arrows[0];
                _arrows.Remove(arrow);
            }
            else
            {
                arrow = Create();
            }

            SetTransform(arrow, point, rotation);
            arrow.Activate(attacker, damage, speed);
        }

        public void UnSpawn(ArrowView arrow) =>
            _arrows.Add(arrow);

        private ArrowView Create()
        {
            var arrow = _assetsProvider.Instantiate(_arrowPrefab, _defaultPosition).GetComponent<ArrowView>();
            arrow.gameObject.SetActive(false);
            arrow.InitPool(this);
            return arrow;
        }

        private void SetTransform(ArrowView arrow, Vector2 point, Quaternion rotation)
        {
            arrow.transform.position = point;
            arrow.transform.rotation = rotation;
        }
    }
}