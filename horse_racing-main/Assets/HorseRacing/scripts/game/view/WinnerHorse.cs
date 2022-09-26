using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerHorse : MonoBehaviour
{
    
    private HorseItem _horseItem;
   

    public void updated(HorseItem horseItem)
    {
        _horseItem = horseItem;
        SetHorseAppearance();
        SetJockeyAppearance();
    }
    
    private void SetHorseAppearance()
    {
        // Debug.Log("set horse appearance");
        
        SkinnedMeshRenderer t_Renderer;
        Texture maZuoDian;
        
        // 马身体颜色
        t_Renderer = transform.Find("MA_GRP/MA_Body001").gameObject.GetComponent<SkinnedMeshRenderer>();
        t_Renderer.material = Resources.Load<Material>("Materials/horse_body " + _horseItem.appearance.horse.skin);

        // 马排号布
        t_Renderer = transform.Find("MA_GRP/MA_ZuoDian001").gameObject.GetComponent<SkinnedMeshRenderer>();
        maZuoDian = Resources.Load<Texture>("Textures/Ma_ZuoDian " + _horseItem.serialNumber);
        //Make sure to enable the Keywords
        t_Renderer.material.EnableKeyword ("_NORMALMAP");
        t_Renderer.material.EnableKeyword ("_METALLICGLOSSMAP");
        //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
        t_Renderer.material.SetTexture("_MainTex", maZuoDian);

        //白色的马，尾巴都为白色
        if (_horseItem.appearance.horse.skin == 5)
        {
            
        }
        
    }

    private void SetJockeyAppearance()
    {
        // Debug.Log("set Jockey appearance");
        
        // 上衣
        SkinnedMeshRenderer t_Renderer;
        t_Renderer = transform.Find("QiShou_ShangYi").gameObject.GetComponent<SkinnedMeshRenderer>();
        t_Renderer.material = Resources.Load<Material>("Materials/QiShou_Shangyi " + _horseItem.appearance.jockey.dress);
        
        // 手套
        t_Renderer = transform.Find("QiShou_ShouTao").gameObject.GetComponent<SkinnedMeshRenderer>();
        t_Renderer.material = Resources.Load<Material>("Materials/QiShou_ShouTao " + _horseItem.appearance.jockey.dress);
        
        // 帽子
        t_Renderer = transform.Find("polySurface25").gameObject.GetComponent<SkinnedMeshRenderer>();
        t_Renderer.material = Resources.Load<Material>("Materials/QiShou_MaoZi " + _horseItem.appearance.jockey.dress);
        
        // 护肩
        t_Renderer = transform.Find("QiShou_HuJian").gameObject.GetComponent<SkinnedMeshRenderer>();
        t_Renderer.material = Resources.Load<Material>("Materials/QiShou_HuJian " + _horseItem.appearance.jockey.dress);
    }
}
