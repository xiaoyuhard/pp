using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.context.api;

namespace HorseRacing.introduction
{
    public class IntroductionStartupCommand : Command
    {
        [Inject] public MainRaceModel MainRaceModel { get; set; }

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView{ get; set; }
        
        override public void Execute()
        {

            Debug.Log("introduction startup");


            Debug.Log(MainRaceModel.RaceInfo.phase);

            GameObject go = contextView.transform.Find("HorseShow").gameObject;
            if (go != null)
            {
                go.AddComponent<IntroductionHorseShowView> ();
            }
            
            if (MainRaceModel.RaceInfo == null)
            {
                Debug.Log("Ther has noting info data !");
                //return;
            }
        }
    }
}