using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBase : MonoBehaviour
{
    [SerializeField]
    protected Sprite otherSprite;
    
    protected SpriteRenderer spriteRenderer;
    protected Sprite originalDownSprite;
    protected Sprite originalUpSprite;
    protected Sprite downSprite;
    protected Sprite upSprite;
    protected float someScaleX;
    protected float someScaleY;
    protected int directionX;
    protected int directionY;
    protected float _posX;
    protected float _posY;
    protected Rigidbody2D myRigid;

    // Start is called before the first frame update
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myRigid = GetComponent<Rigidbody2D>();
        originalDownSprite = spriteRenderer.sprite;
        originalUpSprite = otherSprite;
        downSprite = originalDownSprite;
        upSprite = otherSprite;
        someScaleX = transform.localScale.x;
        someScaleY = transform.localScale.y;
        directionX = 1;
        directionY = -1;
        _posX = transform.position.x;
        _posY = transform.position.y;
    }
    
    public void UpdateFacing() {
        if (directionY == 1) {
            spriteRenderer.sprite = upSprite;
            transform.localScale = new Vector2(directionX == 1 ? -someScaleX : someScaleX, transform.localScale.y);
        } else {
            spriteRenderer.sprite = downSprite;
            transform.localScale = new Vector2(directionX == -1 ? -someScaleX : someScaleX, transform.localScale.y);
        }
    }
    
    public void RestoreSprite()
    {
        upSprite = originalUpSprite;
        downSprite = originalDownSprite;
        UpdateFacing();
    }
    
    public void UpdateSprite()
    {
        if (transform.position.x < _posX) {
            if (directionX == 1) {
                transform.localScale = new Vector2(directionY == -1 ? -someScaleX : someScaleX, transform.localScale.y);
                directionX = -1;
            }
        } else if (transform.position.x > _posX) {
            if (directionX == -1) {
                transform.localScale = new Vector2(directionY == -1 ? someScaleX : -someScaleX, transform.localScale.y);
                directionX = 1;
            }
        }
        if (transform.position.y < _posY) {
            if (directionY == 1) {
                directionY = -1;
                spriteRenderer.sprite = downSprite;
            }
            UpdateFacing();
        } else if (transform.position.y > _posY) {
            if (directionY == -1) {
                directionY = 1;
                spriteRenderer.sprite = upSprite;
            }
            UpdateFacing();
        }
        _posX = transform.position.x;
        _posY = transform.position.y;
    }
}
