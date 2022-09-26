using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;

public class HorseItemView : MonoBehaviour
{

    public bool isPassEnd = false;
    
    private HorseItem _horseItem;
    public HorseItem horseItem
    {
        get
        {
            return _horseItem;
        }
        set
        {
            _horseItem = value;
        }
    }

    private NavMeshAgent _agent;

    
    private AudioSource _audioSource;

    private List<float> _speeds = new List<float>();

    private int _blockIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = transform.GetComponent<NavMeshAgent>();
        //RaceInfoHorseItem t = t.raceResult.horses[0];
        if (_horseItem != null)
        {
            // frameIndex = 0;
            // frameIndex2 = 0;
            SetHorseAppearance();
            SetJockeyAppearance();
        }
        
        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource || !_audioSource.enabled) return;
        _audioSource.loop = true;
        _audioSource.Play();
        _audioSource.volume = 0f;
    }

    private void Update()
    {
        if (_agent.speed < 6)
        {
            _audioSource.volume = 0.0f;
        }
        else if (!isPassEnd)
        {
            _audioSource.volume = 0.4f;
        }
        else
        {
            _audioSource.volume = 0.0f;
        }
    }

    private void SetHorseAppearance()
    {
        Addressables.LoadAssetsAsync<Texture>("MA_Body_Albedo 0" + _horseItem.appearance.horse.skin,
            obj =>
            {
                // 马身体颜色
                SkinnedMeshRenderer t_Renderer = transform.Find("MA_GRP/MA_Body001").gameObject.GetComponent<SkinnedMeshRenderer>();
                t_Renderer.material.SetTexture("_MainTex", obj);
            });

        Addressables.LoadAssetsAsync<Texture>("Ma_ZuoDian " + _horseItem.serialNumber,
            obj =>
            {
                // 马排号布
                SkinnedMeshRenderer t_Renderer = transform.Find("MA_GRP/MA_ZuoDian001").gameObject.GetComponent<SkinnedMeshRenderer>();
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
        var h = Addressables.LoadAssetsAsync<Texture>("QiShou_Shangyi_" + _horseItem.appearance.jockey.dress,
            obj =>
            {
                    
                SkinnedMeshRenderer t_Renderer = transform.Find("QiShou_ShangYi").gameObject.GetComponent<SkinnedMeshRenderer>();
                t_Renderer.material.SetTexture("_MainTex", obj);
            });
        // 手套
        Addressables.LoadAssetsAsync<Texture>("QiShou_ShouTao_" + _horseItem.appearance.jockey.dress,
            obj =>
            {
                   
                SkinnedMeshRenderer t_Renderer = transform.Find("QiShou_ShouTao").gameObject.GetComponent<SkinnedMeshRenderer>();
                t_Renderer.material.SetTexture("_MainTex", obj);
            });

        // 帽子
        Addressables.LoadAssetsAsync<Texture>("QiShou_MaoZi_" + _horseItem.appearance.jockey.dress,
            obj =>
            {
                SkinnedMeshRenderer t_Renderer = transform.Find("polySurface25").gameObject.GetComponent<SkinnedMeshRenderer>();
                t_Renderer.material.SetTexture("_MainTex", obj);
            });

        // 护肩
        Addressables.LoadAssetsAsync<Texture>("QiShou_HuJian_" + _horseItem.appearance.jockey.dress,
            obj =>
            {
                   
                SkinnedMeshRenderer t_Renderer = transform.Find("QiShou_HuJian").gameObject.GetComponent<SkinnedMeshRenderer>();
                t_Renderer.material.SetTexture("_MainTex", obj);
            });

        // 鞋子
        /*Addressables.LoadAssetsAsync<Texture>("QiShou_XieZi", obj =>
        {
            SkinnedMeshRenderer t_Renderer = transform.Find("QiShou_XieZi").gameObject.GetComponent<SkinnedMeshRenderer>();
            t_Renderer.material.SetTexture("_MainTex", obj);
        });*/
    }

    public void run()
    {
        if (_horseItem != null && _horseItem.distances != null && _horseItem.distances.Length > 0)
        {
            int t = 1;        // 区间步长，或者叫区间序号
            int lastFramIndex = 0;   // 已经过去的上一个区间的帧序号
            int step_distance = 10;    // 每 10m 一个区间
            int step2 = 120;   // 赛事 1200m 每 10m 一个区间，过终点多一个区间, 总共 120 个区间

            int tmp_distance = 0;//单位 cm
            
            // 计算每个区间 step 的平均速度 
            for (int i = 0; i < _horseItem.distances.Length; i++)
            {
                tmp_distance = _horseItem.distances[i];
                // 找到超过区间的最近的值, 比如 找到一个在(50,51)  的 x ,x 最接近 50
                //if (tmp_distance >= 100 * t * step_distance  && tmp_distance < 100 * (t * step_distance + 1) && t < step2)
                if (tmp_distance >= 100 * t * step_distance  && t < step2)
                {
                    // 速度单位 m/s
                    float dSpeed = RaceModel.dataframeDeltaTime * (tmp_distance - (t - 1) * 100.00f * step_distance) 
                                   / ((i - lastFramIndex) * RaceModel.dataframeDeltaTime);

                    float deltaTime = RaceModel.dataframeDeltaTime * (i - lastFramIndex + 1);
                    float length = 0;
                    if (lastFramIndex == 0)
                        length = (_horseItem.distances[i]) / 100.00f; // 单位m
                    else
                        length = (_horseItem.distances[i] - _horseItem.distances[lastFramIndex]) / 100.00f;
                    dSpeed =  length / deltaTime;
                    _speeds.Add(dSpeed);
                    lastFramIndex = i;
                    t++;
                }
            }
            
            /**/
            string speed_str = _horseItem.serialNumber + "号马,花费了: " + "秒，走了"+ 
                               _horseItem.distances.Length + "个0.04s,终点距离是" +
                               _horseItem.distances[_horseItem.distances.Length-1]  + 
                               "速度是: " + _speeds[0].ToString("F2");
            for (int j = 1; j < _speeds.Count; j++)
            {
                speed_str += "," + _speeds[j].ToString("F2");
            }
            
            Debug.Log(speed_str);
           
            // agent.speed = speeds[0];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("SpeedCollider"))
        {
            if (_blockIndex < _speeds.Count)
            {
                _agent.speed = _speeds[_blockIndex];
                _blockIndex++;
            }
        }
    }

    private void OnDestroy()
    {
        //Addressables.Release();
    }
}
