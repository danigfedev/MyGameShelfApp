using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class IGDBRequestManager : MonoBehaviour
{
    //Test image
    public Image coverImage;

    private void Awake()
    {
        IGDB_RequestBodies.BuildIGDBRequestBodies();
    }

    /// <summary>
    /// Get the number of entries in IGDB that match given name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    public void GetGameSearchCount(string name, UnityAction<GameSearchCount_IGDB_down> callback)
    {
        Debug.Log("Preparing to send request: Get Search Count \"" + name + "\"");
        string requestBody = IGDB_RequestBodies.GetGameSearchCountIGDBRequestBody(name);
        //UnityAction<GameSearchData_IGDB_Down> callback = ProcessGameSearchData;
        IEnumerator requestCoroutine = IGDB_Post_Request/*<GameSearchData_IGDB_Down>*/(IGDB_Endpoints.MULTIQUERY, requestBody, callback);
        StartCoroutine(requestCoroutine);
    }

    public void GetGameSearchData(string name, int searchLimit, UnityAction<GameSearchData_IGDB_Down> callback = null)
    {
        Debug.Log("Preparing to send request: Search Game \"" + name + "\"");
        string requestBody = IGDB_RequestBodies.GetGameSearchIGDBRequestBody(name, searchLimit);
        
        //Debug.LogWarning(requestBody);

        IEnumerator requestCoroutine = IGDB_Post_Request/*<GameSearchData_IGDB_Down>*/(IGDB_Endpoints.GAMES_SEARCH_ENDPOINT, requestBody, callback);
        StartCoroutine(requestCoroutine);
    }


    public void GetGameCover(string image_id, string imgSize, UnityAction<KeyValuePair<GameSearchData, Texture2D>/*Texture2D*/> callback, KeyValuePair<GameSearchData, Texture2D> kvp /*, Texture2D refTexture = null*/)
    {
        IEnumerator getCoverCoroutine = IGDB_Get_Image_Request(image_id, imgSize, callback, kvp/*, refTexture*/);
        StartCoroutine(getCoverCoroutine);
    }


    /// <summary>
    /// Sends a customizable web request to IGDB
    /// </summary>
    /// <typeparam name="T">The type of the deserialized data for current request</typeparam>
    /// <param name="endpoint">IGDB endpoint</param>
    /// <param name="body">The body of the request</param>
    /// <returns></returns>
    private IEnumerator IGDB_Post_Request<T>(string endpoint, string body, UnityAction<T> callback)
    {

        string uri = IGDB_URIs.IGDB_BASE_URI + endpoint;
        var www = new UnityWebRequest(uri, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(body);

        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();

        www.SetRequestHeader(IGDB_RequestHeaders.CLIENT_ID_HEADER_KEY, IGDB_RequestHeaders.CLIENT_ID_HEADER_VALUE);
        www.SetRequestHeader(IGDB_RequestHeaders.AUTH_HEADER_KEY, IGDB_RequestHeaders.AUTH_HEADER_VALUE);

        string msg = string.Format("[IGDB] Sending request to {0} endpoint", endpoint);
        Debug.Log(msg);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            T downloadedData = DeserializeDataFromJSON<T>(www.downloadHandler.text, endpoint);

            if(callback!= null)
                callback.Invoke(downloadedData);
        }
    }


    private IEnumerator IGDB_Get_Image_Request(string image_ID, string imgSize, UnityAction<KeyValuePair<GameSearchData, Texture2D>/*Texture2D*/> callback, KeyValuePair<GameSearchData, Texture2D> kvp/*, Texture2D refTexture = null*/)
    {
        Texture2D refTexture = null;
        kvp = new KeyValuePair<GameSearchData, Texture2D>(kvp.Key, refTexture);
        if (string.IsNullOrEmpty(image_ID))
        {
            callback.Invoke(kvp);
            yield break;
        }

        string extension = ".jpg"; // always the same, according to IGDB docs
        string url = IGDB_URIs.IGDB_IMAGE_BASE_URI + imgSize + "/" + image_ID + extension;
        //Debug.LogWarning(url);

        //Example= "t_thumb/co1wyy.jpg"
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            refTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            kvp = new KeyValuePair<GameSearchData, Texture2D>(kvp.Key, refTexture);

            //if (callback != null && refTexture != null)
            //    callback.Invoke(kvp/*refTexture*/);
            
        }

        if (callback != null /*&& refTexture != null*/)
            callback.Invoke(kvp/*refTexture*/);
    }

    private T DeserializeDataFromJSON<T>(string jsonPlatformData, string endpoint)
    {
        //Platform_IGDB_Down_List platformData = new Platform_IGDB_Down_List();
        //JsonUtility expects an object. Downloaded data from IGDB does not come in that format. 
        // For that reason, we need to add "{\"platform_list_igdb_down\":" + <DATA> + "}"

        string objectName = IGDB_JSONObjectNames.GetJSONObjectNameOfEndpoint((endpoint));
        return JsonUtility.FromJson<T>("{\"" + objectName + "\":" + jsonPlatformData + "}");



        //Platform example *****
        //platformData = JsonUtility.FromJson<Platform_IGDB_Down_List>("{\"" + objectName + "\":" + jsonPlatformData + "}");
        //platformData = JsonUtility.FromJson<Platform_IGDB_Down_List>("{\"platform_list_igdb_down\":" + jsonPlatformData + "}");

        //Debugging processed data:
        //Platform[] platformList = platformData.platform_list_igdb_down;
        //foreach(Platform platform in platformList)
        //{
        //    Debug.Log(platform.abbreviation);
        //}
    }


    /*
    // Testing purposes:
    public void GetGameCover()
    {
        StartCoroutine(IGDB_Get_Image_Request());
    }

    

    public void GetGeneralData()
    {
        StartCoroutine(Send_IGDB_General_Data_Request());
    }
   

    public void GetPlatforms()
    {

        StartCoroutine(Send_IGDB_Platform_Request());

    }



    private IEnumerator Send_IGDB_Platform_Request()
    {
        string uri = "https://api.igdb.com/v4/platforms";

        var www = new UnityWebRequest(uri, "POST");
        string body = "limit 5; fields name, abbreviation, platform_logo;";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Client-ID", "k4wgv73l6stngu0v9yf52ufze4lemg");
        www.SetRequestHeader("Authorization", "Bearer agb1nxlx8oedmzxm6v0rtaqy0q4yy7");

        yield return www.SendWebRequest();


        Debug.Log("Response received");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            //ProcessPlatformData(www.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
        }
    }


    

    //Nueva forma de realizar peticiones a IGDB (Usar Postman para ver los ejemplos)
    //Ahora es petición POST
    //El truco es construir la petición manualmente: cabeceras, upload (body) y download Handler 
    IEnumerator Send_IGDB_General_Data_Request()
    {
        Debug.Log("Sending request");
        //string uri = "https://api-v3.igdb.com/games";
        string uri = "https://api.igdb.com/v4/games";
    

        var www = new UnityWebRequest(uri, "POST");
        string example1 = "fields *; where id = 1942;";
        //string example2 = "search \"Zelda\"; fields name, release_dates.human;";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(example1);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Client-ID", "k4wgv73l6stngu0v9yf52ufze4lemg");
        www.SetRequestHeader("Authorization", "Bearer agb1nxlx8oedmzxm6v0rtaqy0q4yy7");


        // Request and wait for the desired page.
        yield return www.SendWebRequest();


        Debug.Log("Response received");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }

    

    */
}
