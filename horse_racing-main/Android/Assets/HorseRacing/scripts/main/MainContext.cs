using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace HorseRacing.scripts.main
{
    public class MainContext : SignalContext
    {
        public MainContext(MonoBehaviour contextView) : base(contextView)
        {
           
        }


        protected override void mapBindings()
        {
            base.mapBindings();

            if (Context.firstContext == this)
            {
            }

            //=========  model =========
            injectionBinder.Bind<MainRaceModel>().ToSingleton().CrossContext();

            //==========  service  ==========


            //=========  command  ==========


            //=========  mediator(view)  ==========
            mediationBinder.Bind<AppInterfaceView>().To<AppInterfaceMeditor>();
            //commandBinder.Bind<StartSignal>().To<MainStartupCommand>();
//TODO fix bug
//            commandBinder.Bind<StartSignal>().To<MainTestCommand>();

#if UNITY_EDITOR
            commandBinder.Bind<StartSignal>().To<MainTestCommand>();
#else
            commandBinder.Bind<StartSignal>().To<MainStartupCommand>();
#endif

        }


        protected override void postBindings()
        {
            //BindCamera();
		
            base.postBindings();
		
            SetLightToDisable();
        }

    }
}