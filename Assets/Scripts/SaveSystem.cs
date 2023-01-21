using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public int totalPoints = 0;

    public static SaveSystem Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        if (!PlayerPrefs.HasKey("highScore"))
        {
            PlayerPrefs.SetInt("highScore", 0);
        }

        
    }

    public int GetPoints()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    public void SaveGame()
    {
        if(GameManager.Instance.totalPoints > GetPoints())
        {
            PlayerPrefs.SetInt("highScore", GameManager.Instance.totalPoints);

        }
    }


}
