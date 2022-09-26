
public class MainRaceModel
{
    private RaceResult _raceResult;

    public RaceResult RaceResult
    {
        get { return _raceResult; }
        set {
            _raceResult = value;
            UpdateDistance();
        }
    }

    private RaceInfo _raceInfo;

    public RaceInfo RaceInfo
    {
        get { return _raceInfo; }
        set { _raceInfo = value; }
    }
    
    private void UpdateDistance()
    {
        // set distance to info data
        for (int i = 0; i < _raceInfo.horses.Count; i++)
        {
            HorseItem HorseItem_info_tmp;
            HorseItem HorseItem_result_tmp;
     
            HorseItem_info_tmp = _raceInfo.horses[i];
            for (int j = 0; j < _raceResult.horses.Length; j++)
            {
                HorseItem_result_tmp = _raceResult.horses[j];
                if (HorseItem_info_tmp.rowNum == HorseItem_result_tmp.rowNum)
                {
                    HorseItem_info_tmp.distances = HorseItem_result_tmp.distances;
                    break;
                }
            }
        }
    }
}
