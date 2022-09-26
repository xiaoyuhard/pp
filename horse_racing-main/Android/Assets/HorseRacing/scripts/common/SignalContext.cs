using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using UnityEngine;

public class SignalContext : MVCSContext
{
    public SignalContext (MonoBehaviour contextView) : base(contextView)
    {
    }

    protected override void addCoreComponents ()
    {
        base.addCoreComponents ();
        injectionBinder.Unbind<ICommandBinder> ();
        injectionBinder.Bind<ICommandBinder> ().To<SignalCommandBinder> ().ToSingleton ();
    }

    public override void Launch ()
    {
        base.Launch ();
        StartSignal startSignal = (StartSignal)injectionBinder.GetInstance<StartSignal>(); 
        startSignal.Dispatch();
    }

    protected override void mapBindings ()
    {
        base.mapBindings ();
        //implicitBinder.ScanForAnnotatedClasses (new string[]{"strange.examples.strangerobots"});
    }
    
    internal void BindCamera()
    {
        Camera cam = (contextView as GameObject).GetComponentInChildren<Camera>();
        if (cam == null)
        {
            throw new Exception("Couldn't find the UI camera");
        }

        //injectionBinder.Bind<Camera>().ToValue(cam).ToName(HorseRacingElement.GAME_CAMERA);
    }

    internal void SetLightToDisable()
    {
        if (Context.firstContext != this)
        {
            //Disable the AudioListener
            AudioListener listener = (contextView as GameObject).GetComponentInChildren<AudioListener>();
            listener.enabled = false;

            //Disable the light
            Light[] lights = (contextView as GameObject).GetComponentsInChildren<Light>();
            foreach (Light light in lights)
            {
                light.enabled = false;
            }
        }
    }
}
