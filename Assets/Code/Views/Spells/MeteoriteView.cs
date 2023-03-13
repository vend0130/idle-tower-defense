using Code.Extensions;
using DG.Tweening;
using UnityEngine;

namespace Code.Views.Spells
{
    public class MeteoriteView : MonoBehaviour
    {
        [SerializeField] private Transform _moveObject;
        [SerializeField] private Transform _shadownObject;
        [SerializeField] private Vector2 _startPoint;
        [SerializeField] private float _speed;

        private Tween _tween;

        private void Awake() =>
            Hide();

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
            _tween = _moveObject.DOMove(endPoint, distance / _speed).SetEase(Ease.Linear);
            _tween?.OnComplete(EndFall);
        }

        private void EndFall()
        {
            Hide();
        }

        private void Hide()
        {
            _moveObject.gameObject.SetActive(false);
            _shadownObject.gameObject.SetActive(false);
        }
    }
}