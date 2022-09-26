using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.U2D;
using System.Collections.Generic;

public class CompareMethod : IComparer<Sprite>  //继承IComparer<T>接口，T为要比较的元素的类型
{                                             //类中类，也可以放在类外面
    public int Compare(Sprite x, Sprite y)
    {
        return x.name.CompareTo(y.name);//返回值大于0表示x>y,等于0表示x=y,小于0表示x<y。Array.Sort内部会根据这个返回值来判断x和y的大小关系，并把小的元素放在前面
                           //如果想降序怎么办，返回y[0]-x[0]即可
    }
}
/// <summary>
/// 序列帧动画播放器
/// 支持UGUI的Image和Unity2D的SpriteRenderer
/// </summary>
[ExecuteInEditMode]
public class FramesAnimation : MonoBehaviour
{
    /// <summary>
    /// 序列帧
    /// </summary>
    public Sprite[] Frames { get { return frames; } set { frames = value; } }

    [SerializeField] private Sprite[] frames = null;
    public float FrameDuration;
    /// <summary>
    /// 帧率，为正时正向播放，为负时反向播放
    /// </summary>
    public float Framerate { get { return framerate; } set { framerate = value; } }

    private float framerate = 20.0f;

    /// <summary>
    /// 是否忽略timeScale
    /// </summary>
    public bool IgnoreTimeScale { get { return ignoreTimeScale; } set { ignoreTimeScale = value; } }

    [SerializeField] private bool ignoreTimeScale = true;

    /// <summary>
    /// 是否循环
    /// </summary>
    public bool Loop { get { return loop; } set { loop = value; } }

    [SerializeField] private bool loop = true;

    //动画曲线
    [SerializeField] private AnimationCurve curve = new AnimationCurve(new Keyframe(0, 1, 0, 0), new Keyframe(1, 1, 0, 0));

    /// <summary>
    /// 结束事件
    /// 在每次播放完一个周期时触发
    /// 在循环模式下触发此事件时，当前帧不一定为结束帧
    /// </summary>
    public event Action FinishEvent;

    //目标Image组件
    private Image image;
    //目标SpriteRenderer组件
    private SpriteRenderer spriteRenderer;
    //当前帧索引
    private int currentFrameIndex = 0;
    //下一次更新时间
    private float timer = 0.0f;
    //当前帧率，通过曲线计算而来
    private float currentFramerate = 20.0f;
    public bool isPlay = false;
    /// <summary>
    /// 重设动画
    /// </summary>
    public void Reset()
    {
        currentFrameIndex = framerate < 0 ? frames.Length - 1 : 0;
    }

    /// <summary>
    /// 从停止的位置播放动画
    /// </summary>
    public void Play()
    {
        isPlay = true;
    }

    /// <summary>
    /// 暂停动画
    /// </summary>
    public void Pause()
    {
        isPlay = false;
    }

    /// <summary>
    /// 停止动画，将位置设为初始位置
    /// </summary>
    public void Stop()
    {
        Pause();
        Reset();
    }

    //自动开启动画
    void Start()
    {
       
    }
    public void Init(SpriteAtlas spriteAtlas)
    {
        Frames = new Sprite[spriteAtlas.spriteCount];
        int count=spriteAtlas.GetSprites(Frames);
        Array.Sort(Frames,new CompareMethod());
        // Debug.Log(count);
        framerate = Frames.Length / FrameDuration;
        image = this.GetComponent<Image>();
        
        spriteRenderer = this.GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
        if (image == null && spriteRenderer == null)
        {
            Debug.LogError("No available component found. 'Image' or 'SpriteRenderer' required.", this.gameObject);
        }
#endif
        image.sprite = Frames[0];
    }
    
    void Update()
    {
        //帧数据无效，禁用脚本
        if (frames == null || frames.Length == 0)
        {
            isPlay = false;
        }
        else
        {
            if (isPlay)
            {
                //从曲线值计算当前帧率
                float curveValue = curve.Evaluate((float)currentFrameIndex / frames.Length);
                float curvedFramerate = curveValue * framerate;
                //帧率有效
                if (curvedFramerate != 0)
                {
                    //获取当前时间
                    float time = ignoreTimeScale ? Time.unscaledTime : Time.time;
                    //计算帧间隔时间
                    float interval = Mathf.Abs(1.0f / curvedFramerate);
                    //满足更新条件，执行更新操作
                    if (time - timer > interval)
                    {
                        //执行更新操作
                        DoUpdate();
                    }
                }
#if UNITY_EDITOR
                else
                {
                    Debug.LogWarning("Framerate got '0' value, animation stopped.");
                }
#endif
            }
        }
    }
    //public void SeekTo(int index)
    //{
    //    currentFrameIndex = index;
    //    //更新图片
    //    if (image != null)
    //    {
    //        image.sprite = frames[currentFrameIndex];
    //    }
    //    else if (spriteRenderer != null)
    //    {
    //        spriteRenderer.sprite = frames[currentFrameIndex];
    //    }
    //}
    public void SeekTo(float rate)
    {
        currentFrameIndex = (int)(frames.Length * rate);
        if (currentFrameIndex >= 0 && currentFrameIndex < frames.Length)
        {
            if (image != null)
            {
                image.sprite = frames[currentFrameIndex];
            }
            else if (spriteRenderer != null)
            {
                spriteRenderer.sprite = frames[currentFrameIndex];
            }
        }
    }

    //具体更新操作
    private void DoUpdate()
    {
        //计算新的索引
        int nextIndex = currentFrameIndex + (int)Mathf.Sign(currentFramerate);
        //索引越界，表示已经到结束帧
        if (nextIndex < 0 || nextIndex >= frames.Length)
        {
            //广播事件
            if (FinishEvent != null)
            {
                FinishEvent();
            }
            //非循环模式，禁用脚本
            if (loop == false)
            {
                currentFrameIndex = Mathf.Clamp(currentFrameIndex, 0, frames.Length - 1);
                this.enabled = false;
                return;
            }
        }
        //钳制索引
        currentFrameIndex = nextIndex % frames.Length;
        //更新图片
        if (image != null)
        {
            image.sprite = frames[currentFrameIndex];
        }
        else if (spriteRenderer != null)
        {
            spriteRenderer.sprite = frames[currentFrameIndex];
        }
        //设置计时器为当前时间
        timer = ignoreTimeScale ? Time.unscaledTime : Time.time;
    }
}
