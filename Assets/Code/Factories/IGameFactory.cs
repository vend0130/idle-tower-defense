using Code.Controllers.Spells;
using Code.Game.Hero;
using Code.Game.UI;

namespace Code.Factories
{
    public interface IGameFactory
    {
        void CreateHud();
        HeroHealth CreateHero();
        void CreateEndGame();
        SpellView CreateSpell(SpellType spellType);
    }
}