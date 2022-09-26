using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;


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

    private List<float> _needTime = new List<float>();

    private List<float> lackTime = new List<float>();

    public List<GameObject> speedColliderList;

    List<float> distanceAll = new List<float>();

    List<float> selfDistances = new List<float>();

    List<Vector3> allHorsePos = new List<Vector3>();

    List<Vector3> wanHorPos = new List<Vector3>();


    public int _blockIndex;

    private CharacterController characterController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

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

    public bool isEndHrose = false;

    public bool isPlayEndHrose = false;

    public bool isEndHorseCount = false;

    public bool isEndHorseBl = false;

    public bool isDeleatDistance = false;

    public int horseDistanceGap;

    float disTime = 0;

    int endHorseCount = 0;

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
        if (exerciseBl)
        {
            if (setStartSpeed)
            {
                animator.SetBool("move", true);
                allSpeed = 20;
                animator.SetFloat("speed", allSpeed);
                setStartSpeed = false;
            }
        }
        if (isPlayEndHrose)
        {
            Camera.main.gameObject.SetActive(true);
            StartCoroutine(pauseHorse());
            if (isEndHrose)
            {
                if (transform.position.x - speedColliderList[speedColliderList.Count - 1].transform.position.x > -0.1f && isEndHorseBl)
                {
                    Time.timeScale = 0f;
                    isEndHorseCount = true;
                }
            }
        }
        if (isEndHorseCount)
        {
            endHorseCount++;
        }
        if (endHorseCount == 20 && isEndHorseCount)
        {
            Time.timeScale = 0.5f;
            isPlayEndHrose = false;
        }
    }

    IEnumerator pauseHorse()
    {
        yield return new WaitForSeconds(0.5f);
        if (isEndHorseBl)
        {
            isEndHrose = true;
        }
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
        t_Renderer.material.EnableKeyword("_NORMALMAP");
        t_Renderer.material.EnableKeyword("_METALLICGLOSSMAP");
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

    public void run()
    {
        selfOldPos = transform.localPosition;

        if (_horseItem != null && _horseItem.distancesNew != null && _horseItem.distancesNew.Count > 0)
        {
            int t = 1;        // 区间步长，或者叫区间序号
            int lastFramIndex = 0;   // 已经过去的上一个区间的帧序号
            int step_distance = 10;    // 每 10m 一个区间
            int step2 = 120;   // 赛事 1200m 每 10m 一个区间，过终点多一个区间, 总共 120 个区间
            float alwaysTime = 0;

            int tmp_distance = 0;//单位 cm

            int disCount = 0;
            for (int i = 0; i < _horseItem.distancesNew.Count; i++)
            {
                if (_horseItem.distancesNew[i].distance >= 50000 && _horseItem.distancesNew[i].distance <= 80000)
                {
                    disCount++;
                }
            }
            float perDu = -180f / disCount;
            float lastDis = 0;
            float posZ = 97;
            float posX1 = -300;
            float posX2 = 0;
            // 计算每个区间 step 的平均速度 
            for (int i = 0; i < _horseItem.distancesNew.Count; i++)
            {
                tmp_distance = _horseItem.distancesNew[i].distance;
                if (tmp_distance >= 116000)
                {
                    distanceAll.Add(tmp_distance);
                }
                if (tmp_distance >= wan1 && tmp_distance <= wan2)
                {

                    Vector3 endPoint;
                    int addPoint1 = 0, addPoint2 = 0;
                    float radian = (disTime * perDu * Mathf.PI) / 180;
                    if (_horseItem.distancesNew[i].track != 1)
                    {
                        int point = _horseItem.distancesNew[i].track - 1;
                        if (tmp_distance < wan3)
                        {
                            addPoint1 = -point;
                            addPoint2 = point;
                        }
                        else if (tmp_distance < wan4)
                        {
                            addPoint1 = -point;
                        }
                        else
                        {
                            addPoint1 = point;
                            addPoint2 = -point;
                        }
                    }
                    endPoint.z = posX2 + posZ * Mathf.Cos(radian) + addPoint2;
                    endPoint.y = transform.position.y;
                    endPoint.x = posX1 + posZ * Mathf.Sin(radian) + addPoint1;
                    wanHorPos.Add(endPoint);
                    disTime += 1;
                }
                float dis = tmp_distance * RaceModel.dataframeDeltaTime;
                lastDis = dis;
                selfDistances.Add(dis);
                // 找到超过区间的最近的值, 比如 找到一个在(50,51)  的 x ,x 最接近 50
                if (tmp_distance >= 100 * t * step_distance && t < step2)
                {
                    float dSpeed = 0;
                    float deltaTime = RaceModel.dataframeDeltaTime * (i - lastFramIndex + 1);
                    float length = 0;
                    if (lastFramIndex == 0)
                        length = (_horseItem.distancesNew[i].distance) / 100.00f; // 单位m
                    else
                        length = (_horseItem.distancesNew[i].distance - _horseItem.distancesNew[lastFramIndex].distance) / 100.00f;
                    alwaysTime += deltaTime;
                    lackTime.Add(deltaTime);
                    _needTime.Add(alwaysTime);
                    dSpeed = length / deltaTime;
                    _speeds.Add(dSpeed);
                    lastFramIndex = i;
                    t++;
                }
            }

            for (int i = 5; i < 1000; i += 5)
            {
                float dis = i * 0.1f + lastDis;
                selfDistances.Add(dis);
            }
            int tarck = 0;
            for (int i = 0; i < selfDistances.Count; i++)
            {
                if (i< _horseItem.distancesNew.Count)
                {
                    tarck = _horseItem.distancesNew[i].track;
                }

                if (selfDistances[i] >= wan1 * RaceModel.dataframeDeltaTime && selfDistances[i] <= wan2 * RaceModel.dataframeDeltaTime && wanHorPos.Count > 0)
                {
                    allHorsePos.Add(wanHorPos[0]);
                    wanHorPos.RemoveAt(0);
                }
                else if (selfDistances[i] > wan2 * RaceModel.dataframeDeltaTime)
                {
                    float residueDis = (-400) + (selfOldPos.x + selfDistances[i] - wan2 * RaceModel.dataframeDeltaTime);
                    Vector3 pos = new Vector3(residueDis, selfOldPos.y, -195 - tarck);
                    allHorsePos.Add(pos);
                }
                else
                {
                    Vector3 pos = new Vector3(selfOldPos.x - selfDistances[i], selfOldPos.y, tarck);
                    allHorsePos.Add(pos);
                }
                continue;
            }
            //string speed_str = _horseItem.serialNumber + "号马,花费了: " + _needTime[_needTime.Count - 1] + "秒，走了" +
            //                   _horseItem.distances.Length + "个0.01s,终点距离是" + _horseItem.rowNum + "    " +
            //                   _horseItem.distances[_horseItem.distances.Length - 1] +
            //                   "速度是: " + _speeds[0].ToString("F2");
            //for (int i = 1; i < _speeds.Count; i++)
            //{
            //    speed_str += "," + _speeds[i].ToString("F2");
            //}

            //Debug.Log(speed_str);

            // agent.speed = speeds[0];
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("SpeedCollider"))
        {
            if (_blockIndex < _speeds.Count)
            {
                //_agent.speed = _speeds[_blockIndex];
                //_blockIndex++;
            }
        }
    }
    public bool exerciseBl = false;
    bool setStartSpeed = true;
    public float allSpeed = 0;
    int wan1 = 50000, wan2 = 80000, wan3 = 60000, wan4 = 70000;
    int skipTime1 = 900, skipTime2 = 2300;
    int _indexDis = 0;
    bool isCloseRe = true;
    public bool isStartRe = false;
    Vector3 selfOldPos;


    private void FixedUpdate()
    {
        if (exerciseBl)
        {
            if (_indexDis < selfDistances.Count && !isEndHorseBl)
            {
                if (selfDistances[_indexDis] > 1140 && isCloseRe)
                {
                    isStartRe = true;
                    isCloseRe = false;
                }
                if (selfDistances[_indexDis] >= wan1 * RaceModel.dataframeDeltaTime && selfDistances[_indexDis] <= wan2 * RaceModel.dataframeDeltaTime)
                {
                    transform.position = allHorsePos[_indexDis];
                }
                else if (selfDistances[_indexDis] > wan2 * RaceModel.dataframeDeltaTime)
                {
                    characterController.radius = 0.5f;
                    transform.localPosition = allHorsePos[_indexDis];
                    transform.position = new Vector3(transform.localPosition.x + 100, transform.localPosition.y, transform.localPosition.z + 100);
                }
                else
                {
                    if (selfDistances[_indexDis] > 100)
                    {
                        allSpeed = 40;
                    }
                    else
                    {
                        allSpeed = 20;
                    }
                    transform.localPosition = allHorsePos[_indexDis];
                }
                _indexDis++;
                if (_indexDis == skipTime1)
                {
                    GameObject.FindGameObjectWithTag("camera2").transform.GetComponent<CinemachineVirtualCamera>().Priority = 12;
                    _indexDis = 1500;
                }
                if (_indexDis == skipTime2)
                {
                    _indexDis = 2800;
                }
            }
        }
    }

    public GameObject targetDistance;

    public RecordData data = new RecordData();

    private Rigidbody rigidbody;
    private NavMeshAgent agent;
    private Animator animator;
    public void ReplayHorsePos()
    {
        StartCoroutine(Recording());
        ReplayHorseManager.Singleton.OnReplayTimeChange += Replay;
        ReplayHorseManager.Singleton.OnReplayStart += OnReplayStart;

        rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    IEnumerator Recording()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / ReplayHorseManager.Singleton.recordRate);
            if (ReplayHorseManager.Singleton.isRecording)
            {
                data.Add(transform);
            }

        }
    }

    public void OnReplayStart()
    {
        if (rigidbody != null)
            rigidbody.isKinematic = true;

        if (agent)
            agent.enabled = false;

        if (animator)
            animator.enabled = false;
    }

    public void Replay(float t)
    {
        data.Set(t, transform);
    }
}
