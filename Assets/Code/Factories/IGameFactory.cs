using Code.Game.Hero;

namespace Code.Factories
{
    public interface IGameFactory
    {
        void CreateHud();
        HeroHealth CreateHero();
        void CreateEndGame();
    }
}