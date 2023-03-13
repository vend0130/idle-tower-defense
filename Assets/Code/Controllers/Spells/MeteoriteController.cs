using System;
using Code.Game.UI;
using Code.Model.Spawn;
using Code.Views.Spells;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Controllers.Spells
{
    public class MeteoriteController : IMeteoriteController, IDisposable
    {
        private readonly ISpawnModel _spawnModel;

        private SpellView _spellView;
        private MeteoriteView _meteoriteView;

        public MeteoriteController(MeteoriteView meteoriteView, ISpawnModel spawnModel)
        {
            _meteoriteView = meteoriteView;
            _spawnModel = spawnModel;
        }

        public void InitSpellView(SpellView spellView)
        {
            _spellView = spellView;
            _spellView.ClickHandler += Cast;
        }

        public void Dispose()
        {
            if (_spellView == null)
                return;

            _spellView.ClickHandler -= Cast;
        }

        private void Cast() =>
            _meteoriteView.StartFallMeteorite(GetEndPoint());

        private Vector2 GetEndPoint()
        {
            Vector2 halfScreenSize = _spawnModel.HalfScreenSize;
            float x = Random.Range(-halfScreenSize.x, halfScreenSize.x);
            float y = Random.Range(-halfScreenSize.y, halfScreenSize.y);
            return new Vector2(x, y);
        }
    }
}