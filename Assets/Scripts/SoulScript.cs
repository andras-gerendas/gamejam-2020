using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulScript : MonoBehaviour
{
    [SerializeField]
    float speed = 0.03f;
    
    [SerializeField]
    float forceValue = 4.5f;
    
    Rigidbody2D myBody;
    
    public static int InstanceCount = 0;
    
    private int counter;
    private int direction;
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other == null) {
            return;
        }
    
        if (other.gameObject.name == "Soul" || other.gameObject.name == "Gate") {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        myBody.angularVelocity = 0f;
        myBody.velocity = Vector3.zero;
    }
    
    void OnCollision2DStay(Collision2D other)
    {
        myBody.angularVelocity = 0f;
    }
    
    void OnCollision2DExit(Collision2D other) {
        myBody.angularVelocity = 0f;
        myBody.velocity = Vector3.zero;
    }
    
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        SoulScript.InstanceCount++;
    }

    // Start is called before the first frame update
    void Start()
    {
        //myBody.AddForce (new Vector2 (forceValue * 50, 50));
        counter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Global.Instance.isGameOver()) {
            return;
        }
        if (counter % 20 == 0) {
            direction = Random.Range(0, 16);
            counter = 0;
        }
        float x = 0;
        float y = 0;
        
        switch(direction) {
            case 0:
                x -= speed;
                break;
            case 1:
                y -= speed;
                break;
            case 2:
                x += speed;
                break;
            case 3:
                y += speed;
                break;
            default:
                break;
        }
        
        transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
        counter++;
    }
    
    void OnDestroy() {
        SoulScript.InstanceCount--;
    }
}
