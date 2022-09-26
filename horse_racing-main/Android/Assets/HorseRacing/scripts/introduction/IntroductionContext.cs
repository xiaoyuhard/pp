using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace HorseRacing.introduction
{

    public class IntroductionContext : SignalContext
    {
        public IntroductionContext(MonoBehaviour contextView) : base(contextView)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            if (Context.firstContext == this)
            {
                
            }
            
            //=========  model =========


            //==========  service  ==========

            //=========  mediator(view)  ==========
            Debug.Log("mediator");
            mediationBinder.Bind<IntroductionHorseShowView>().To<IntroductionHorseShowMediator>();

            //=========  command  ==========

            //commandBinder.Bind<StartSignal>().To<MainStartupCommand>();
            commandBinder.Bind<StartSignal>().To<IntroductionStartupCommand>().Once();
            
        }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        /*verride public IContext Start()
        {
            base.Start();
            StartSignal startSignal = (StartSignal)injectionBinder.GetInstance<StartSignal>();
            startSignal.Dispatch();
            return this;
        }*/


        protected override void postBindings()
        {
            //BindCamera();

            base.postBindings();

            SetLightToDisable();
        }
    }

}