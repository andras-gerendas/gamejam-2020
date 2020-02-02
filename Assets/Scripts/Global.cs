using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    private float summonTime;
    private static Global m_Instance;
    private int score;
    private int lives;
    private bool m_isGameOver;
  
    public GameObject soulPrefab;
    
    [SerializeField]
    Text scoreText;
    
    [SerializeField]
    Text lifeText;
  
    public static Global Instance { get { return m_Instance; } }

    void Awake()
    {
        summonTime = Time.time;
        m_Instance = this;
        Cursor.visible = false;
        lives = 10;
        m_isGameOver = false;
    }

    void OnDestroy()
    {
        m_Instance = null;
    }

    void FixedUpdate()
    {
        if (Input.GetKey (KeyCode.Escape)) {
            SceneManager.LoadScene("Menu");
        } else if (Input.GetKey(KeyCode.Print) || Input.GetKey(KeyCode.F12)) {
            ScreenCapture.CaptureScreenshot("SomeLevel");
        }
        if (m_isGameOver) {
            return;
        }
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = Camera.main.aspect * camHalfHeight;
        
        float top = -camHalfHeight;
        float left = -camHalfWidth;
        float right = camHalfWidth;
        float bottom = camHalfHeight;
        
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall")) {
            SpriteRenderer sr = wall.GetComponent<SpriteRenderer>();
            if (wall.name == "WallTop") {
                top += sr.bounds.size.y;
            } else if (wall.name == "WallLeft") {
                left += sr.bounds.size.x;
            } else if (wall.name == "WallRight") {
                right -= sr.bounds.size.x;
            } else if (wall.name == "WallBottom") {
                bottom -= sr.bounds.size.y;
            }
        }
        
        if (Time.time - summonTime > 2) {
            float x = Random.Range(left, right);
            float y = Random.Range(top, bottom);
            
            if (SoulScript.InstanceCount < 10) {
                GameObject obj = (GameObject)Instantiate(soulPrefab, new Vector2(x, y), Quaternion.identity);
                obj.name = "Soul";
                obj.tag = "Soul";
            }
            summonTime = Time.time;
        }
    }
    
    public void UpdateScore() {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
    
    public void UpdateLives() {
        lives--;
        if (lives > 0) {
            if (lives != 1) {
                lifeText.text = "Lives: ";
            } else {
                lifeText.text = "Life: ";
            }
        } else {
            lifeText.text = "Game Over";
            m_isGameOver = true;
            ScoreScript.UpdateHighScore(score);
            PlayerPrefs.SetInt(GameOverScript.lastScoreKey, score);
            SceneManager.LoadScene("GameOver");
        }
        foreach (GameObject life in GameObject.FindGameObjectsWithTag("Life")) {
            SpriteRenderer sr = life.GetComponent<SpriteRenderer>();
            for (int i = 0; i < 10; i++) {
                if (life.name == "Life" + i) {
                    if (lives <= i) {
                        sr.enabled = false;
                    }
                }
            }
        }
    }
    
    public bool isGameOver() {
        return m_isGameOver;
    }

    void OnGui()
    {
    }
}
