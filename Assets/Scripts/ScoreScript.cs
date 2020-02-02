using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    private static string scoreBase = "HighScore";
    private static int[] scores = new int[5];
    // Start is called before the first frame update
    
    [SerializeField]
    Text scoreList;
    
    void Start()
    {
        UpdateLocalHighScores();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey (KeyCode.Escape)) {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKey(KeyCode.Print) || Input.GetKey(KeyCode.F12)) {
            ScreenCapture.CaptureScreenshot("SomeLevel");
        }
        
        string output = "High Scores\n\nPress ESC to return\n\n";
        
        for (int i = 0; i < 5; i++) {
            if (scores[i] > 0) {
                output += scores[i] + "\n";
            }
        }
        
        scoreList.text = output;
    }
    
    public static void UpdateLocalHighScores() {
        for (int i = 0; i < 5; i++) {
            scores[i] = PlayerPrefs.GetInt(scoreBase + i.ToString(), 0);
        }
    }
    
    public static void UpdateStoredHighScores() {
        for (int i = 0; i < 5; i++) {
            PlayerPrefs.SetInt(scoreBase + i.ToString(), scores[i]);
        }
    }
    
    public static void UpdateHighScore(int score) {
        Debug.Log("High score: " + score);
        UpdateLocalHighScores();
        for (int i = 0; i < 5; i++) {
            if (score > scores[i]) {
                for (int j = 4; j > i; j--) {
                    scores[j] = scores[j - 1];
                }
                scores[i] = score;
                break;
            }
        }
        UpdateStoredHighScores();
    }
}
