using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace HorseRacing.scripts.main
{
    public class MainContext : MVCSContext
    {
        public MainContext(MonoBehaviour contextView) : base(contextView)
        {

        }


        protected override void mapBindings()
        {
        }
        
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            //injectionBinder.Unbind<ICommandBinder>();
            //injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        override public IContext Start()
        {
            base.Start();
            //StartSignal startSignal = (StartSignal)injectionBinder.GetInstance<StartSignal>();
            //startSignal.Dispatch();
            return this;
        }


    }
}