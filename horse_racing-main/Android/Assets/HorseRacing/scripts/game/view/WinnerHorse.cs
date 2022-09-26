using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
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

        Addressables.LoadAssetsAsync<Texture>("MA_Body_Albedo 0" + _horseItem.appearance.horse.skin,
            obj =>
            {
                // 马身体颜色
                SkinnedMeshRenderer t_Renderer =
                    transform.Find("MA_GRP/MA_Body001").gameObject.GetComponent<SkinnedMeshRenderer>();
                t_Renderer.material.SetTexture("_MainTex", obj);
            });

        Addressables.LoadAssetsAsync<Texture>("Ma_ZuoDian " + _horseItem.serialNumber,
            obj =>
            {
                // 马排号布
                SkinnedMeshRenderer t_Renderer = transform.Find("MA_GRP/MA_ZuoDian001").gameObject
                    .GetComponent<SkinnedMeshRenderer>();
                t_Renderer.material.SetTexture("_MainTex", obj);
            });

        //白色的马，尾巴都为白色
        if (_horseItem.appearance.horse.skin == 5)
        {

        }

    }

    private void SetJockeyAppearance()
    {
        // 上衣
        Addressables.LoadAssetsAsync<Texture>("QiShou_Shangyi_" + _horseItem.appearance.jockey.dress,
            obj =>
            {

                SkinnedMeshRenderer t_Renderer =
                    transform.Find("QiShou_ShangYi").gameObject.GetComponent<SkinnedMeshRenderer>();
                t_Renderer.material.SetTexture("_MainTex", obj);
            });
        // 手套
        Addressables.LoadAssetsAsync<Texture>("QiShou_ShouTao_" + _horseItem.appearance.jockey.dress,
            obj =>
            {

                SkinnedMeshRenderer t_Renderer =
                    transform.Find("QiShou_ShouTao").gameObject.GetComponent<SkinnedMeshRenderer>();
                t_Renderer.material.SetTexture("_MainTex", obj);
            });

        // 帽子
        Addressables.LoadAssetsAsync<Texture>("QiShou_MaoZi_" + _horseItem.appearance.jockey.dress,
            obj =>
            {
                SkinnedMeshRenderer t_Renderer =
                    transform.Find("polySurface25").gameObject.GetComponent<SkinnedMeshRenderer>();
                t_Renderer.material.SetTexture("_MainTex", obj);
            });

        // 护肩
        Addressables.LoadAssetsAsync<Texture>("QiShou_HuJian_" + _horseItem.appearance.jockey.dress,
            obj =>
            {

                SkinnedMeshRenderer t_Renderer =
                    transform.Find("QiShou_HuJian").gameObject.GetComponent<SkinnedMeshRenderer>();
                t_Renderer.material.SetTexture("_MainTex", obj);
            });

        // 鞋子
        /*Addressables.LoadAssetsAsync<Texture>("QiShou_XieZi", obj =>
        {
            SkinnedMeshRenderer t_Renderer = transform.Find("QiShou_XieZi").gameObject.GetComponent<SkinnedMeshRenderer>();
            t_Renderer.material.SetTexture("_MainTex", obj);
        });*/
    }
}
