using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDetection : MonoBehaviour
{
    private GameObject player;
    private List<GameObject> enemies;
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        }
        if(enemies.Count == 0)
        {
            Invoke("WinGame", 1f);
        }
    }

    public void WinGame()
    {
        SceneManager.LoadScene("WinScene");
    }
}
