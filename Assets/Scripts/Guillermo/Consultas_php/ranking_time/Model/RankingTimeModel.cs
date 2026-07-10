using System;
using System.Collections.Generic;

[Serializable]
public class RankingTimeEntry
{
    public string player_id;
    public string total_time;
}

[Serializable]
public class RankingTimeModel
{
    public List<RankingTimeEntry> data;
}