using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private int selectedItem = 0;
    private int timeout = 0;
    
    [SerializeField]
    Text startText;
    
    [SerializeField]
    Text scoreText;
    
    [SerializeField]
    Text quitText;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Update()
    {
        UpdateColor();
    }
    
    void UpdateColor() {
        startText.color = Color.white;
        scoreText.color = Color.white;
        quitText.color = Color.white;
        Text selectedText;
        switch (selectedItem) {
            case 0:
                selectedText = startText;
                break;
            case 1:
                selectedText = scoreText;
                break;
            default:
                selectedText = quitText;
                break;
        }
        selectedText.color = Color.red;
    }

    void FixedUpdate()
    {
        if (timeout < 10) {
            timeout++;
        }
        if (Input.GetKey (KeyCode.Escape)) {
            Application.Quit();
        } else if (Input.GetKey(KeyCode.Print) || Input.GetKey(KeyCode.F12)) {
            ScreenCapture.CaptureScreenshot("SomeLevel");
        } else if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space)) {
            if (selectedItem == 0) {
                SceneManager.LoadScene("Main");
            } else if (selectedItem == 1) {
                SceneManager.LoadScene("Scoreboard");
            } else {
                Application.Quit();
            }
        } else if (Input.GetKey(KeyCode.DownArrow) && timeout == 10) {
            selectedItem++;
            if (selectedItem > 2) {
                selectedItem = 0;
            }
            UpdateColor();
            timeout = 0;
        } else if (Input.GetKey(KeyCode.UpArrow) && timeout == 10) {
            selectedItem--;
            if (selectedItem < 0) {
                selectedItem = 2;
            }
            UpdateColor();
            timeout = 0;
        }
    }
}
