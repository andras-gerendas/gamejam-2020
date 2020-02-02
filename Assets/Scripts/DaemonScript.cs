using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DaemonScript : MonoBehaviour
{
    private Sprite originalSprite;
    private bool isCarrying;
    private SpriteRenderer spriteRenderer;
    private float someScale;
    private int direction;
    private float _posX;
    private Rigidbody2D myRigid;
    private Vector2 origin;
    
    [SerializeField]
    float speed = 0.03f;

    [SerializeField]
    Sprite carrySoul;
        
    // Start is called before the first frame update
    void Start()
    {
         spriteRenderer = GetComponent<SpriteRenderer>();
         isCarrying = false;
         myRigid = GetComponent<Rigidbody2D>();
         originalSprite = spriteRenderer.sprite;
         origin = new Vector2(transform.position.x, transform.position.y);
         someScale = transform.localScale.x;
         direction = 1;
         _posX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void UpdateSprite() {
    }
    
    void FixedUpdate()
    {
        if (Global.Instance.isGameOver()) {
            return;
        }
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        if (!RuneScript.isBroken()) {
            if (currentPos != origin) {
                transform.position = origin;
            }
            isCarrying = false;
            spriteRenderer.sprite = originalSprite;
            transform.localScale = new Vector2(someScale, transform.localScale.y);
            direction = 1;
            return;
        }
        if (!isCarrying) {
            GameObject obj = FindClosestTarget("Soul");
            if (obj != null) {
                Vector2 soulPos = new Vector2(obj.transform.position.x, obj.transform.position.y);
                transform.position = Vector2.MoveTowards(currentPos, soulPos, speed);
            }
        } else {
            if (currentPos == origin) {
                isCarrying = false;
                spriteRenderer.sprite = originalSprite;
                speed *= 1.2f;
                Global.Instance.UpdateLives();
            } else {
                transform.position = Vector2.MoveTowards(currentPos, origin, speed);
            }
        }
        if (transform.position.x < _posX) {
            if (direction == 1) {
                transform.localScale = new Vector2(-someScale, transform.localScale.y);
                direction = -1;
            }
        } else if (transform.position.x > _posX) {
            if (direction == -1) {
                transform.localScale = new Vector2(someScale, transform.localScale.y);
                direction = 1;
            }
        }
        _posX = transform.position.x;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        myRigid.angularVelocity = 0f;
        myRigid.velocity = Vector3.zero;
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (!RuneScript.isBroken()) {
            return;
        }
        if (other.name == "Soul") {
            if (!isCarrying) {
               isCarrying = true;
               Destroy(other.gameObject);
               if (carrySoul != null) {
                   spriteRenderer.sprite = carrySoul;
               }
            }
        }
        myRigid.angularVelocity = 0f;
    }
    
    void OnTrigger2DExit(Collider2D other) {
        myRigid.angularVelocity = 0f;
        myRigid.velocity = Vector3.zero;
    }
    
    GameObject FindClosestTarget(string trgt)
    {
        Vector3 position = transform.position;
        GameObject[] objects = GameObject.FindGameObjectsWithTag(trgt);
        return objects.OrderBy(o => (o.transform.position - position).sqrMagnitude)
            .FirstOrDefault();
    }
}
