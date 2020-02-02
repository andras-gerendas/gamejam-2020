using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    Text gameOver;
    
    public static string lastScoreKey = "LastScore";

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        if (Input.GetKey (KeyCode.Escape)) {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKey(KeyCode.Print) || Input.GetKey(KeyCode.F12)) {
            ScreenCapture.CaptureScreenshot("SomeLevel");
        }
        gameOver.text = "Press ESC to continue\n\nScore: " + PlayerPrefs.GetInt(lastScoreKey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
