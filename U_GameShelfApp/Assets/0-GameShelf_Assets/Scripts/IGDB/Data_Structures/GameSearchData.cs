using System;

[Serializable]
public class GameSearchData
{
    public int id;
    public string name;
    public Cover cover;
    public int[] platforms; //platform ids to search in local list and retrieve data
}
