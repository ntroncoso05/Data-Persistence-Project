using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Assets.Scripts;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField playerName;
    public TextMeshProUGUI highScore;
    public static MenuUIHandler instance;
    public SaveScore bestScore;
    public string currentPlayerName;

    void Awake()
    {
        bestScore = new SaveScore();
        LoadScore();
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public void StartNew()
    {
        if(playerName.text != "")
        {
            currentPlayerName = playerName.text;
            gameObject.SetActive(false);
            SceneManager.LoadScene(1);
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    private void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            bestScore = JsonUtility.FromJson<SaveScore>(json);

            highScore.text = $"Best score: {bestScore.Name} : {bestScore.HighScore}";
        }
        else highScore. text = "Best score: : 0";
    }
}
