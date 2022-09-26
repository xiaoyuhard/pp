using strange.extensions.context.impl;

namespace HorseRacing.scripts.main
{
    public class MainBootstrap : ContextView
    {
        void Start()
        {
            context = new GameContext(this);
        }
    }
}