
using strange.extensions.context.impl;

namespace HorseRacing.introduction
{

    public class IntroductionBootstrap : ContextView
    {
        void Start()
        {
            context = new IntroductionContext(this);
        }
    }
}