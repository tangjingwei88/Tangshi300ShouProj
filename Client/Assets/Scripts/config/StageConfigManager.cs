using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageConfigManager : ProtoBase {

    public class StageConfig
    {
        public int ID;
        public string Name;
        public string Icon;
        //游戏时长
        public string Author;
        //几位数结果
        public string PoemItem;                                                          
        public string PoemText;
        public string Annotation;  //注释


        public Dictionary<int, int> ResultDic = new Dictionary<int, int>();                     //存储结果
        public Dictionary<int, int> NumPoolDic = new Dictionary<int, int>();                    //存储数字选择项
        public Dictionary<int, string> iconPoolDic = new Dictionary<int, string>();             //存储icon选择项
        public Dictionary<int, string> numPoemItemDic = new Dictionary<int, string>();           //num-诗句选择项

        public StageConfig()
        {
            ID = 1;
            Name = "";
            Icon = "";
            Author = "";
            PoemItem = "";
            PoemText = "";
            Annotation = "";

        }


        public StageConfig(m.StageConfig s)
        {
            ID = s.ID;
            Name = s.Name;
            Icon = s.Icon;
            Author = s.author;
            PoemItem = s.PoemItem;
            PoemText = s.PoemText;
            Annotation = s.annotation;


            string[] str2List = s.PoemItem.Split(';');
            for (int i = 0; i < str2List.Length; i++)
            {
                numPoemItemDic.Add(i + 1, str2List[i]);         //"1:红豆生南国格"式保存"

                string[] numIcon = str2List[i].Split(':');
                NumPoolDic.Add(i + 1,int.Parse(numIcon[0]));    //只存储数字
                iconPoolDic.Add(i + 1, numIcon[1]);             //存储icon
            }
        }
    }


    #region 数据

    public static List<StageConfig> stageConfigList = new List<StageConfig>();
    public static Dictionary<int, StageConfig> stageConfigDic = new Dictionary<int, StageConfig>();

    #endregion




    /// <summary>
    /// 初始化
    /// </summary>
    public static void Init()
    {
        ReadData();
    }


    #region 读取数据

    private static void ReadData()
    {
        List<m.StageConfig> stageConfig = LoadPoto<m.StageConfig>("StageConfig");
        for (int i = 0; i < stageConfig.Count; i++)
        {
            m.StageConfig sc = stageConfig[i];
            StageConfig script = new StageConfig(sc);
            if(!stageConfigDic.ContainsKey(script.ID))
            {
                stageConfigDic.Add(script.ID, script);
                stageConfigList.Add(script);
            }

        }
    }

    #endregion

    public static StageConfig GetStageConfig(int ID)
    {
        if (stageConfigDic.ContainsKey(ID))
        {
            return stageConfigDic[ID];
        }
        else {
            Debug.LogError("GetStageConfig failed ID = " + ID.ToString());
            return new StageConfig();
        }
    }

    #region 接口


    #endregion
}
