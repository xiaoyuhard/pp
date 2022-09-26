using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallbackWebServiceCommand : Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)] 
    public GameObject ContextView { get; set; }
    
    [Inject] 
    public RaceModel rm { get; set; }
    
    public override void Execute()    
    {
        //Debug.Log("5 - web service callback command");
        return;
        
        if (rm.isShow)
        {
            SceneManager.LoadScene("2-Show", LoadSceneMode.Additive);
            rm.isShow = false;
        }
        

        if (rm.CostSeconds > 30 && rm.CostSeconds < 33)
        {
            SceneManager.UnloadSceneAsync("2-Show");
        }

        
        if (rm.isRacing && rm.CostSeconds > 36 && rm.CostSeconds < 39)
        {
            SceneManager.LoadScene("4-Racing1200", LoadSceneMode.Additive);
            rm.isRacing = false;
        }
        
        if (rm.CostSeconds > 162 && rm.CostSeconds < 165)
        {
            SceneManager.UnloadSceneAsync("4-Racing1200");
        }

        if (rm.CostSeconds > 166)
        {
            //#if UNITY_EDITOR
            //    UnityEditor.EditorApplication.isPlaying = false;
            //#else
            //    Application.Quit();
            //#endif

            rm.CostSeconds = 1;
            rm.isRacing = rm.isShow = true;
        }
        rm.CostSeconds ++;
        Debug.Log(rm.CostSeconds);
        
    }
}