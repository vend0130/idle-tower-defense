using System.Collections.Generic;
using Code.Data;
using Code.Extensions;
using Code.Game;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Views.Spells
{
    public class MeteoriteView : MonoBehaviour
    {
        [SerializeField] private Transform _moveObject;
        [SerializeField] private Transform _shadownObject;
        [SerializeField] private Vector2 _startPoint;

        private readonly Collider2D[] _hits = new Collider2D[20];

        private Tween _tween;
        private MeteoriteData _meteoriteData;

        [Inject]
        public void Constructor(MeteoriteData meteoriteData)
        {
            _meteoriteData = meteoriteData;
            Hide();
        }

        private void OnDestroy() =>
            _tween.SimpleKill();

        public void StartFallMeteorite(Vector2 endPoint)
        {
            Vector2 startPoint = new Vector2(endPoint.x, _startPoint.y);
            _moveObject.position = startPoint;
            _shadownObject.position = endPoint;

            _moveObject.gameObject.SetActive(true);
            _shadownObject.gameObject.SetActive(true);

            Move(endPoint);
        }

        private void Move(Vector2 endPoint)
        {
            _tween.SimpleKill();
            float distance = Vector2.Distance(_startPoint, endPoint);
            _tween = _moveObject.DOMove(endPoint, distance / _meteoriteData.Speed).SetEase(Ease.Linear);
            _tween?.OnComplete(EndFall);
        }

        private void EndFall()
        {
            Hide();
            Hits();
        }

        private void Hits()
        {
            List<IHealth> enemies = new List<IHealth>();
            int hits = Physics2D.OverlapCircleNonAlloc(_moveObject.position, _moveObject.localScale.x, _hits);

            for (int i = 0; i < hits; i++)
            {
                if (_hits[i].TryGetComponent(out IHealth health) && health.Unit == UnitType.Enemy)
                    enemies.Add(health);
            }

            foreach (var enemy in enemies)
                enemy.TakeDamage(100 + (10 * enemies.Count));
        }

        private void Hide()
        {
            _moveObject.gameObject.SetActive(false);
            _shadownObject.gameObject.SetActive(false);
        }
    }
}