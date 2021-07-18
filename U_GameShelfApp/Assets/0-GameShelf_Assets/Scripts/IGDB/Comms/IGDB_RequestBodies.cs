using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class IGDB_RequestBodies
{
    #region === REQUEST BODIES ===

    private static string searchGameRequestBody;
    private static string searchGameCountRequestBody;

    /// <summary>
    /// Builds the bodies of every IGDB request sent by this app.
    /// To be called on IGDB service initialization.
    /// </summary>
    public static void BuildIGDBRequestBodies()
    {
        BuildGameSearchIGDBRequestBody();
        BuildGameSearchCountIGDBRequestBody();
    }

    #endregion


    #region === Game Search IGDB request body ===

    private static void BuildGameSearchIGDBRequestBody()
    {
        StringBuilder strBuild = new StringBuilder();
        string command1 = "search \"{0}\";";
        string command2 = "fields name, cover.image_id, platforms;";
        string command3 = "limit {1};";
        strBuild.AppendLine(command1);
        strBuild.AppendLine(command2);
        strBuild.AppendLine(command3);
        searchGameRequestBody = strBuild.ToString();
        //search "witcher 3: wild hunt";
        //fields name, cover.image_id, rating, platforms;
    }

    private static void BuildGameSearchCountIGDBRequestBody()
    {
        // Query sample
        //query games/ count "Count of Games" {
        //    fields name;
        //    search"pro beach soccer"; //REMOVE THIS LINE -- Important to remove that blank space between search keyword and the name
        //};

        StringBuilder strBuild = new StringBuilder();

        //Doubling { char so it is process as a literal value and ignored in the formatting stage
        //Source: https://stackoverflow.com/a/3773868/15619464
        string line1 = "query games/count \"Count of Games\" {{"; 
        string line2 = "fields name;";
        string line3= "search\"{0}\";}};";
        strBuild.AppendLine(line1);
        strBuild.AppendLine(line2);
        strBuild.AppendLine(line3);
        searchGameCountRequestBody = strBuild.ToString();
    }

    public static string GetGameSearchIGDBRequestBody(string gameName, int gameCount =-1)
    {
        if (gameCount < 0)
            gameCount = 10;
        return string.Format(searchGameRequestBody, gameName, gameCount);
    }
    
    public static string GetGameSearchCountIGDBRequestBody(string gameName)
    {
        return string.Format(searchGameCountRequestBody, gameName);
    }

    #endregion

}
