using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

public class GameContext : SignalContext
{
    public GameContext(MonoBehaviour contextView) : base(contextView)
    {

    }

    protected override void mapBindings()
    {
        base.mapBindings();
        if (Context.firstContext == this)
        {
        }

        //=========  model =========
        injectionBinder.Bind<IGameConfig>().To<GameConfig>().ToSingleton();

        injectionBinder.Bind<RaceModel>().ToSingleton();


        //==========  service  ==========
        injectionBinder.Bind<IWebService>().To<LocalService>().ToSingleton();
        // injectionBinder.Bind<IWebService>().To<WebService>().ToSingleton();

        //injectionBinder.Bind<WebServiceCallbackSignal>().ToSingleton();

        //=========  command  ==========
        //commandBinder.Bind<CallWebServiceSignal>().To<CallWebServiceCommand>();
        //commandBinder.Bind<CallbackWebServiceSignal>().To<CallbackWebServiceCommand>();

        injectionBinder.Bind<RaceStateChanged>().ToSingleton();
        injectionBinder.Bind<RaceInfoChanged>().ToSingleton();
        injectionBinder.Bind<RaceResultChanged>().ToSingleton();
        injectionBinder.Bind<SortRankingChanged>().ToSingleton();
        injectionBinder.Bind<SortRankingWinnerChanged>().ToSingleton();
        //commandBinder.Bind<ChangeCurrentRaceData_Signal>().To<ShowHorseCommand>();
        //绑定开始事件一个startcommand(只绑定一次，再触发就解绑了)
        //commandBinder.Bind(ContextEvent.START).To<StartCommand>().Once();
        commandBinder.Bind<StartSignal>().To<StartCommand>().Once();

        //=========  mediator(view)  ==========
        //mediationBinder.Bind<RequestPerSecondsView>().To<AMediaa>();
        mediationBinder.Bind<ShowHorseView>().To<ShowHorseMediator>();
        //mediationBinder.Bind<RacingView>().To<RacingMeditor>();
        mediationBinder.Bind<UIView>().To<UiMeditor>();
        // mediationBinder.Bind<MinimapPanel>().To<UiMeditor>();
    }

    protected override void addCoreComponents()
    {
        base.addCoreComponents();
        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }

    override public IContext Start()
    {
        base.Start();
        StartSignal startSignal = (StartSignal)injectionBinder.GetInstance<StartSignal>();
        startSignal.Dispatch();
        
        
        
        return this;
    }
    
    protected override void postBindings()
    {
        //BindCamera();
		
        base.postBindings();
		
        SetLightToDisable();
    }

    
}