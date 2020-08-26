using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavour : MonoBehaviour
{
    [SerializeField] float enemySpeed;
    bool isRight = true;


    BoxCollider2D boxTrigger;
    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        boxTrigger = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void EnemyWalking()
    {
        if(isRight)
        rigidbody.velocity = new Vector2(enemySpeed, 0);
        else
        rigidbody.velocity = new Vector2(-enemySpeed, 0);

    }
    private void Die()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if(collision.gameObject.layer != 8) { return; }
        float currentScale = gameObject.transform.localScale.x;
        gameObject.transform.localScale = new Vector2(-currentScale, 3f);
        isRight = !isRight;
        Debug.Log("Changing");
    }
    // Update is called once per frame
    void Update()
    {
        EnemyWalking();
    }
}
