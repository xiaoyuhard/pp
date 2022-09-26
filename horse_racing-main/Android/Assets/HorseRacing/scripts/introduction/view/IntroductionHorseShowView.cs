using System.Collections;
using System.Collections.Generic;
using System.Linq;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace HorseRacing.introduction
{
    public class IntroductionHorseShowView : View
    {
        private static string career_tpl = @"<size=""18""><color=#ABABAB><b>CAREER  </b></color></size><size=""26""><color=#F3F3F3>${career}</color></size>";
        [Inject] public MainRaceModel MainRaceModel { get; set; }

        //private Dictionary<int, HorseItem> _horses = new Dictionary<int, HorseItem>();
        
        
        private List<HorseItem> _horses = new List<HorseItem>();
        
        private Transform gam;
        internal void Init()
        {
            gam = transform.GetChild(1);
            for (int j = 0; j < MainRaceModel.RaceInfo.horses.Count; j++)
            {
                //_horses.Add(MainRaceModel.RaceInfo.horses[j].rowNum, MainRaceModel.RaceInfo.horses[j]);
                _horses.Add(MainRaceModel.RaceInfo.horses[j]);
            }

            /*
            for (int i = 1; i <= gameObject.transform.childCount; i++)
            {
                Transform gam = gameObject.transform.GetChild(i - 1);
                Debug.Log(gam.name);

                SetJockeyAppearance(gam.GetChild(0), _horses[i]);
                SetHorseAppearance(gam.GetChild(1), _horses[i]);
                // Debug.Log(gam.Find("Jockey").gameObject.name);
            }
            */

        }

        public void Change(int index)
        {
            SetJockeyAppearance(gam, _horses[index-1]);
            SetHorseAppearance(gam,  _horses[index-1]);
        }

        public void ChagneUI(Transform tr, int index)
        {
            var txt_name = tr.GetChild(6).GetComponent<TMP_Text>();
            var txt_gate = tr.GetChild(5).GetComponent<TMP_Text>();
            var txt_career = tr.GetChild(9).GetComponent<TMP_Text>();
            txt_name.text = _horses[index - 1].name;
            txt_gate.text = "GATE " + _horses[index - 1].rowNum.ToString();
            txt_career.text = career_tpl.Replace("${career}",_horses[index - 1].record.ToString());
        }

        private void SetHorseAppearance(Transform go, HorseItem _horseItem)
        {
            // Debug.Log("set horse appearance");

            //SkinnedMeshRenderer t_Renderer;
            //Texture maZuoDian;

            Addressables.LoadAssetsAsync<Texture>("MA_Body_Albedo 0" + _horseItem.appearance.horse.skin,
                obj =>
                {
                    // 马身体颜色
                    SkinnedMeshRenderer t_Renderer = go.Find("MA_GRP/MA_Body001").gameObject.GetComponent<SkinnedMeshRenderer>();
                    t_Renderer.material.SetTexture("_MainTex", obj);
                });

            Addressables.LoadAssetsAsync<Texture>("Ma_ZuoDian " + _horseItem.serialNumber,
                obj =>
                {
                    // 马排号布
                    SkinnedMeshRenderer t_Renderer = go.Find("MA_GRP/MA_ZuoDian001").gameObject.GetComponent<SkinnedMeshRenderer>();
                    t_Renderer.material.SetTexture("_MainTex", obj);
                });

            //Make sure to enable the Keywords
            //t_Renderer.material.EnableKeyword("_NORMALMAP");
            //t_Renderer.material.EnableKeyword("_METALLICGLOSSMAP");

            //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
            //t_Renderer.material.SetTexture("_MainTex", maZuoDian);

            //白色的马，尾巴都为白色
            if (_horseItem.appearance.horse.skin == 5)
            {

            }

        }

        private void SetJockeyAppearance(Transform go, HorseItem _horseItem)
        {
          
            // Debug.Log("set Jockey appearance");
            //t_Renderer.material = Resources.Load<Material>("Materials/QiShou_Shangyi " + _horseItem.appearance.jockey.dress);
            // 上衣
            Addressables.LoadAssetsAsync<Texture>("QiShou_Shangyi_" + _horseItem.appearance.jockey.dress,
                obj =>
                {
                    
                    SkinnedMeshRenderer t_Renderer = go.Find("QiShou_ShangYi").gameObject.GetComponent<SkinnedMeshRenderer>();
                    t_Renderer.material.SetTexture("_MainTex", obj);
                });
            // 手套
           Addressables.LoadAssetsAsync<Texture>("QiShou_ShouTao_" + _horseItem.appearance.jockey.dress,
               obj =>
               {
                   
                   SkinnedMeshRenderer t_Renderer = go.Find("QiShou_ShouTao").gameObject.GetComponent<SkinnedMeshRenderer>();
                   t_Renderer.material.SetTexture("_MainTex", obj);
               });

           // 帽子
           Addressables.LoadAssetsAsync<Texture>("QiShou_MaoZi_" + _horseItem.appearance.jockey.dress,
               obj =>
               {
                   SkinnedMeshRenderer t_Renderer = go.Find("polySurface25").gameObject.GetComponent<SkinnedMeshRenderer>();
                   t_Renderer.material.SetTexture("_MainTex", obj);
               });

           // 护肩
           Addressables.LoadAssetsAsync<Texture>("QiShou_HuJian_" + _horseItem.appearance.jockey.dress,
               obj =>
               {
                   
                   SkinnedMeshRenderer t_Renderer = go.Find("QiShou_HuJian").gameObject.GetComponent<SkinnedMeshRenderer>();
                   t_Renderer.material.SetTexture("_MainTex", obj);
               });
           
           // 鞋子
           /*Addressables.LoadAssetsAsync<Texture>("QiShou_XieZi", obj =>
           {
               SkinnedMeshRenderer t_Renderer = go.Find("QiShou_XieZi").gameObject.GetComponent<SkinnedMeshRenderer>();
               t_Renderer.material.SetTexture("_MainTex", obj);
           });*/
        }

        private void OnDestroy()
        {
            //Addressables.Release();
        }
    }
}