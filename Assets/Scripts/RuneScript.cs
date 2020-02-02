using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneScript : MonoBehaviour
{
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    private static RuneScript _instance;
    
    [SerializeField]
    Sprite halfRuined;
    
    [SerializeField]
    Sprite ruined;
    
    private static float fixTime;
    public static int breakState;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        _instance = this;
        breakState = 1;
        Fix();
    }
    
    public static bool isBroken() {
        return breakState == 2;
    }

    void FixedUpdate()
    {
        float timeDiff = Time.time - fixTime;
        if (timeDiff > 15 && timeDiff < 20) {
            breakState = 1;
            spriteRenderer.sprite = halfRuined;
        }
        if (timeDiff > 20) {
            breakState = 2;
            spriteRenderer.sprite = ruined;
        }
    }
    
    public static void Fix() {
        if (breakState > 0) {
            breakState = 0;
            fixTime = Time.time;
            _instance.spriteRenderer.sprite = _instance.originalSprite;
        }
    }
}
