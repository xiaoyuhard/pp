# 文件结构说明  

```
Assets                                   
├─AddressableAssetsData                 热更资源 
│  ├─Android                            Android平台 
│  ├─AssetGroups                        资源组 
│  │  └─Schemas                         资源组定义 
│  ├─AssetGroupTemplates                资源组模板 
│  ├─DataBuilders                        
│  └─Windows                            Windows平台 
├─HorseRacing                            
│  ├─Animator                           动画
│  ├─art                                艺术资源  
│  │  ├─Animation                       动画片段
│  │  ├─Audio                           声音
│  │  ├─AudioMixer                      声音混合器
│  │  ├─Materials                       材质
│  │  │  └─JianZhu                      建筑 
│  │  ├─Models                          模型 
│  │  ├─Prefabs                         预制体 
│  │  ├─Scenes                          场景 
│  │  │  └─game                          
│  │  ├─Shader                          着色器 
│  │  └─Textures                        贴图 
│  ├─Enviroment                         环境 
│  │  ├─Art                             
│  │  │  ├─Animation                     
│  │  │  ├─Materials                     
│  │  │  │  └─KanTai                     
│  │  │  ├─Models                        
│  │  │  ├─Skybox                        
│  │  │  └─Textures                      
│  │  │      ├─JianZhu                   
│  │  │      ├─SaiMaChang                
│  │  │      ├─SaiMaMaZha                
│  │  │      └─TuoLaJi                   
│  │  └─Prefabs                          
│  ├─Horse                              马 
│  │  ├─Art                              
│  │  │  ├─Materials                     
│  │  │  ├─Models                        
│  │  │  ├─Textures                      
│  │  │  └─TexturesEx                    
│  │  ├─Prefabs                          
│  │  └─Shaders                          
│  │      └─PBR                          
│  ├─Jockey                             骑师 
│  │  ├─Art                              
│  │  │  ├─Materials                     
│  │  │  ├─Models                        
│  │  │  ├─Textures                      
│  │  │  └─TexturesEx                    
│  │  └─Prefabs                          
│  ├─Material                            
│  ├─RenderTextrue                      实时渲染贴图 
│  ├─scripts                            脚本 
│  │  ├─common                          公共脚本 
│  │  ├─game                            游戏模块 
│  │  │  ├─controller                   游戏模块控制层
│  │  │  ├─model                        游戏模块模型层
│  │  │  ├─other                        其它 
│  │  │  ├─service                      游戏模块服务
│  │  │  └─view                         游戏模块视图层 
│  │  ├─introduction                    介绍模块
│  │  │  ├─controller                    
│  │  │  └─view                         
│  │  └─main                            主模块 
│  │      ├─controller                   
│  │      ├─model                        
│  │      └─view                         
│  ├─Timeline                           时间轴 
│  └─ui                                 UI相关 
│      ├─Animation                     	动画 
│      ├─Prefab                         预制体 
│      ├─Sprites                        UI贴图 
│      │  ├─caiyi-mini                  彩衣贴图 
│      │  ├─caodi                       进度条之草地贴图 
│      │  └─nidi                        进度条之泥地贴图 
│      └─Textures                       贴图 
├─HorseRacing2                          资源整理时的备份文件 
│  └─art                                 
│      ├─models                          
│      └─prefabs                         
├─Plugins                               插件 
│  └─Demigiant                           
│      ├─DemiLib                         
│      ├─DOTween                         
│      ├─DOTweenPro                      
│      └─DOTweenPro Examples             
├─Resources                             内置资源 
├─Scenes                                制作过程中的测试场景 
├─Scripts                               脚本 
│  └─VersionEditor                      版本号脚本 
├─StreamingAssets                       流资源 
├─third-party                           第三方插件 
│  ├─NavMeshComponents                   
│  ├─Strange                             
│  ├─TextMesh Pro                        
│  ├─unity-log-viewer
│  └─VersionEditor
└─VersionEditor
```


# 程序架构说明

程序从场景划分来看，整体上分为主场景(main)、赛事介绍场景（Introduction)和比赛场景（game).
每个场景依据MVC划分数据层、显示层、控制层。采用了StrangeIOC开发框架。
美术资源分为环境（Enviroment），赛具（），骑师（Jockey）和马（Horse），这样整理的从程序方面是为了资源的热更新，降低安装包大小，所有艺术类资产保持相对完整的路径，以及清楚的依赖关系。
资源热更采用了最新的Addressable方案，动静资源结合，于开发、测试、部署都很方便。