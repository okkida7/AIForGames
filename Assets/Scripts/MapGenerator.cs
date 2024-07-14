using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class MapGenerator : MonoBehaviour
{
    private string url = "YOUR_SERVER_URL"; // Replace with your server URL
    private string testURL = "https://alexis-jin.com/"; // Test URL for local server

    public IEnumerator TestConnection()
    {
        UnityWebRequest request = UnityWebRequest.Get(testURL);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Server is running");
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }

    public IEnumerator GenerateMap(string prompt)
    {
        Dictionary<string, string> jsonData = new Dictionary<string, string>
        {
            { "prompt", prompt }
        };
        
        string json = JsonUtility.ToJson(jsonData);
        byte[] postData = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest(url, "POST")
        {
            uploadHandler = new UploadHandlerRaw(postData),
            downloadHandler = new DownloadHandlerBuffer()
        };

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            int[,] map = JsonUtility.FromJson<int[,]>(responseText);
            GenerateMapFromData(map);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }

    private void GenerateMapFromData(int[,] mapData)
    {
        // Implement your map generation logic here using the mapData array
        for (int y = 0; y < mapData.GetLength(0); y++)
        {
            for (int x = 0; x < mapData.GetLength(1); x++)
            {
                // Example: Instantiate a tile based on the value in mapData[y, x]
                Debug.Log($"Map value at [{y},{x}]: {mapData[y, x]}");
                // Instantiate your tile here based on the value
            }
        }
    }
}
