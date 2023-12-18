using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Values playerValues;
    public FoodBalanceScript foodBalanceScript;
    public Collider2D playerCollider;
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.GetComponent<EnemyBehavior>())
            {
                foodBalanceScript.GetDamaged(
                        collision.gameObject.GetComponent<EnemyBehavior>()
                        .enemyType,
                        playerValues.damage);
                collision.gameObject.GetComponent<EnemyBehavior>().Die();
            }
        }
    }
}
