using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGDB_JSONObjectNames
{
    public const string GAME_SEARCH_DATA_OBJECT_NAME = "gameSearchData_list_igdb_down";
    public const string PLATFORM_LIST_IGDB_DOWN = "platform_list_igdb_down";
    public const string GAME_SEARCH_COUNT_MULTI = "gameSearchCount_igdb_down";

    public static string GetJSONObjectNameOfEndpoint(string endpoint)
    {
        switch (endpoint)
        {
            case IGDB_Endpoints.GAMES_SEARCH_ENDPOINT:
                return GAME_SEARCH_DATA_OBJECT_NAME;
            //case IGDB_Endpoints.GAMES_ENDPOINT:
            case IGDB_Endpoints.MULTIQUERY:
                return GAME_SEARCH_COUNT_MULTI;
            default:
                return "";
        }
    }
}
