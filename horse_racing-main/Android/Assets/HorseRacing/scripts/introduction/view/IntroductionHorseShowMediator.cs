using strange.extensions.mediation.impl;

namespace HorseRacing.introduction
{
    public class IntroductionHorseShowMediator : Mediator
    {
        [Inject] public IntroductionHorseShowView view { get; set; }


        public override void OnRegister()
        {
            view.Init();
        }
    }
}