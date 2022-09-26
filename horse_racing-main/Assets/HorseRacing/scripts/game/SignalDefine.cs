using strange.extensions.signal.impl;

public class StartSignal : Signal { }

public class CallWebServiceSignal : Signal{ }

public class CallbackWebServiceSignal : Signal<string> { }


public class ClickSignal : Signal{ }



public class ChangeCurrentRaceData_Signal : Signal { }

public class RaceStateChanged : Signal<RaceStateType> {}


public enum RaceStateType
{
    INTRODUCTION,
    RACING_READY,
    RACING_RUN,
    RACING_RECODER
}

public class RaceInfoChanged : Signal<string>{}

public class RaceResultChanged : Signal<bool> {}

public class SortRankingChanged : Signal<SortItem[]> {}

public class SortRankingWinnerChanged : Signal<SortItem[]> {}