using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
public class GameConfig : IGameConfig
{
   
    private string gameConfigPath = Application.dataPath + "/StreamingAssets/";
    
    [PostConstruct]
    public void PostConstruct()
    {
        //TextAsset file = Resources.Load("gameConfig") as TextAsset;
        var gameConfigTxt = File.ReadAllText(gameConfigPath + "gameConfig.json", Encoding.UTF8);
        var n = SimpleJSON.JSON.Parse(gameConfigTxt);
        url = n ["url"];
        timeout = n ["timeout"].AsInt;
    }
    
    #region implement IGameConfig
    
    public string url { get; set; }
    
    public int timeout { get; set; }
    
    #endregion
}

public interface IGameConfig
{
    string url { get; set; }
    
    int timeout { get; set; }
}