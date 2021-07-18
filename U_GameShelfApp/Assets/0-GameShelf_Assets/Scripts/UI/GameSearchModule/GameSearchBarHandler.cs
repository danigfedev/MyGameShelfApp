using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSearchBarHandler : MonoBehaviour
{
    public IGDBRequestManager IGDB_manager;
    [Header("UI elements")]
    public TMPro.TMP_InputField gameInputField;
    public Transform gameListParent;
    public GameObject searchItemPrefab;

    private Dictionary<GameSearchData, Texture2D> gameSearchDataDictionary;
    int coverCount = 0;
    int downloadedDataCount = 10; //TODO refactor this!

    public void SearchGame()
    {
        Debug.Log("Searching...");
        string gamename = gameInputField.text;
        if (string.IsNullOrEmpty(gamename))
            return;


        gameSearchDataDictionary = new Dictionary<GameSearchData, Texture2D>();

        GetSearchGameCountInDB();

        //IGDB_manager.GetGameSearchData(gamename, PopulateGameSearchList);
    }

    public void GetSearchGameCountInDB()
    {
        string gamename = gameInputField.text;
        if (string.IsNullOrEmpty(gamename))
            return;

        UnityAction<GameSearchCount_IGDB_down> callback = ProcessGameSearchCount_Callback;
        IGDB_manager.GetGameSearchCount(gamename, callback);
    }

    private void ProcessGameSearchCount_Callback(GameSearchCount_IGDB_down data)
    {
        Debug.Log("Processing search count");
        string gamename = gameInputField.text;
        downloadedDataCount = data.gameSearchCount_igdb_down[0].count; //data.count;
        UnityAction<GameSearchData_IGDB_Down> callback = GetGameCovers;
        IGDB_manager.GetGameSearchData(gamename, downloadedDataCount, callback);
    }

    private void GetGameCovers(GameSearchData_IGDB_Down gameSearchData)
    {
        //Debug.LogError("Game Count: " + gameSearchData.gameSearchData_list_igdb_down.Length);
        CorrectGameSearchCount(gameSearchData.gameSearchData_list_igdb_down.Length);

        foreach (GameSearchData game in gameSearchData.gameSearchData_list_igdb_down)
        {
            KeyValuePair<GameSearchData, Texture2D> kvp = new KeyValuePair<GameSearchData, Texture2D>(game, null);

            UnityAction<KeyValuePair<GameSearchData, Texture2D>/*Texture2D*/> callback = ProcessGameCover;
            IGDB_manager.GetGameCover(game.cover.image_id, thumb_ImgSize.size, callback, kvp /*, tex*/);
        }
    }


    private void ProcessGameCover(KeyValuePair<GameSearchData, Texture2D> kvp/*Texture2D cover*/)
    {
        if(kvp.Key == null || kvp.Value == null)
        {
            Debug.Log("Key or Value (or both) is null");
        }
        //if (cover == null) Debug.Log("is null you fucking bastard");
        coverCount++;

        gameSearchDataDictionary.Add(kvp.Key, kvp.Value);

        if (coverCount < downloadedDataCount) return;

        PopulateGameSearchList();
        ResetData();
    }

    private void PopulateGameSearchList()
    {
        ClearList();

        if(gameSearchDataDictionary == null)
        {
            Debug.LogError("No data found.");
            return;
        }

        foreach (KeyValuePair<GameSearchData, Texture2D> gameEntry in gameSearchDataDictionary)
        {
            GameObject item = Instantiate(searchItemPrefab, gameListParent);
            Texture2D tex = gameSearchDataDictionary[gameEntry.Key];
            item.GetComponent<GameSearchItemHandler>().InitializeItem(gameEntry.Key, tex);
        }
    }

    /// <summary>
    /// Cleans found games list (UI)
    /// </summary>
    private void ClearList()
    {
        for (int i= gameListParent.childCount-1; i >= 0; i--)
        {
            Destroy(gameListParent.GetChild(i).gameObject);
        }
        
    }

    /// <summary>
    /// Compares IGDB game count and downloaded count and gets smallest value
    /// </summary>
    /// <param name="downloadCount"></param>
    private void CorrectGameSearchCount(int downloadCount)
    {
        if (downloadCount < downloadedDataCount)
            downloadedDataCount = downloadCount;
    }

    /// <summary>
    /// Sets count data to its initial value
    /// </summary>
    private void ResetData()
    {
        coverCount = 0;
        downloadedDataCount = 10;
    }
}