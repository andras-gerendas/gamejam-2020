using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperScript : MovableBase
{
    [SerializeField]
    float speed = 0.001f;
    
    [SerializeField]
    Sprite useHammer;
    
    [SerializeField]
    Sprite useHammerUp;
    
    [SerializeField]
    Sprite useBrush;
    
    [SerializeField]
    Sprite useBrushUp;
    
    [SerializeField]
    Sprite carrySoul;
    
    [SerializeField]
    Sprite carrySoulUp;
    
    private bool isCarrying;
    private int currentTool = 0; //0 - scythe, 1 - brush, 2 - hammer
    private int timeout = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        isCarrying = false;
    }
    
    void Update()
    {
    }
    
    void CollisionHandler(Collision2D other) {
        if (other.gameObject.name == "Soul") {
            if (!isCarrying && currentTool == 0) {
               isCarrying = true;
               Destroy(other.gameObject);
               if (carrySoul != null) {
                   downSprite = carrySoul;
                   upSprite = carrySoulUp;
                   UpdateFacing();
               }
            }
        }
        
        if (other.gameObject.name == "Gate") {
            if (GateScript.breakState < 2) {
                if (isCarrying) {
                    isCarrying = false;
                    Global.Instance.UpdateScore();
                    RestoreSprite();
                }
            }

            if (GateScript.breakState > 0 && currentTool == 2) {
                GateScript.Fix();
            }
        }    
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        myRigid.angularVelocity = 0f;
        myRigid.velocity = Vector3.zero;
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Runes" && currentTool == 1) {
            RuneScript.Fix();
        }
    }
    
    void OnCollisionStay2D(Collision2D other)
    {
        CollisionHandler(other);
        myRigid.angularVelocity = 0f;
    }
    
    void OnCollision2DExit(Collision2D other) {
        myRigid.angularVelocity = 0f;
        myRigid.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Global.Instance.isGameOver()) {
            return;
        }
        if (Input.GetKey ("w") || Input.GetKey(KeyCode.UpArrow)) {
            MoveUp();
        }
        if (Input.GetKey ("s") || Input.GetKey(KeyCode.DownArrow)) {
            MoveDown ();
        }
        if (Input.GetKey ("a") || Input.GetKey(KeyCode.LeftArrow)) {
            MoveLeft ();
        }
        if (Input.GetKey ("d") || Input.GetKey(KeyCode.RightArrow)) {
               MoveRight ();
        }
        if (Input.GetKey(KeyCode.Tab) || Input.GetKey(KeyCode.Space)) {
            if (timeout == 8) {
                if (isCarrying) {
                    isCarrying = false;
                }
                SwitchTool();
                timeout = 0;
            }
        }
        if (timeout < 8) {
            timeout++;
        }
    }
    
    void SwitchTool() {
        currentTool++;
        if (currentTool == 3) {
            currentTool = 0;
        }
        switch (currentTool) {
            case 0:
                downSprite = originalDownSprite;
                upSprite = originalUpSprite;
                break;
            case 1:
                downSprite = useBrush;
                upSprite = useBrushUp;
                break;
            default:
                downSprite = useHammer;
                upSprite = useHammerUp;
                break;
        }
        UpdateFacing();
    }
    
    void Move(float x, float y) {
        transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
        UpdateSprite();
    }
    
    void MoveUp()
    {
        Move(0, speed);
    }
 
    void MoveDown()
    {
        Move(0, -speed);
    }
    
    void MoveLeft()
    {
        Move(-speed, 0);
    }
 
    void MoveRight()
    {
        Move(speed, 0);
    }
}
