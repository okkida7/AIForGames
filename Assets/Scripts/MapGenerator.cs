using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{

    // Assign these in the Unity Editor
    public GameObject[] tilePrefabs; // Array to hold tile sprites
    public static List<Vector3> landPositions = new List<Vector3>();
    public static List<Vector3> waterPositions = new List<Vector3>();
    public static List<Vector3> treePositions = new List<Vector3>();
    public static List<Vector3> obstaclePositions = new List<Vector3>();

    public IEnumerator PostPrompt(string url, string prompt)
    {
        // Create JSON object
        var requestData = new { prompt = prompt };
        string json = JsonConvert.SerializeObject(requestData);

        // Create UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Parse the response JSON into a 2D array
            string responseJson = request.downloadHandler.text;
            int[][] map = JsonConvert.DeserializeObject<int[][]>(responseJson);

            // Generate the map based on the 2D array
            GenerateMap(map);
        }
    }

    public void GenerateMap(int[][] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                Vector3 position = new Vector3(j, -i, 0); // Adjust as needed
                int tileIndex = map[i][j];

                if (tileIndex >= 0 && tileIndex < tilePrefabs.Length)
                {
                    GameObject tile = Instantiate(tilePrefabs[tileIndex], position, Quaternion.identity);

                    if (tile.CompareTag("Land"))
                    {
                        landPositions.Add(position);
                    }
                    else if (tile.CompareTag("Water"))
                    {
                        waterPositions.Add(position);
                    }
                    else if (tile.CompareTag("Tree"))
                    {
                        treePositions.Add(position);
                    }
                    else if (tile.CompareTag("Obstacle"))
                    {
                        obstaclePositions.Add(position);
                    }
                }
            }
        }
    }
}
