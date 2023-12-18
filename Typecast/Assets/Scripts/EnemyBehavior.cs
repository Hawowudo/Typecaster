using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Values enemyValues;
    public EnemySpawner.TypeOfFood enemyType;
    public string baseHealthString, healthString;
    public char[] healthStringArray;

    public bool invulnerable, knockback;
    public float health, speed, damage;
    public Transform target;
    private TextMeshProUGUI enemyText;
    private Vector2 direction;
    public Rigidbody2D enemyRigidBody;
    private void Start()
    {
        target = FindAnyObjectByType<PlayerBehavior>().transform;
        baseHealthString = null;
        speed += enemyValues.speed;
        damage += enemyValues.damage;
        health += enemyValues.health;
        enemyText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        direction = (target.position - this.transform.position ).normalized;
        SetText();
    }
    private void FixedUpdate()
    {
        MovementBehavior();
    }
    private void SetText()
    {
        enemyText.text = healthString;
    }

    private void MovementBehavior()
    {
        if(knockback == true)
        {
            enemyRigidBody.AddForce(-direction * speed,ForceMode2D.Impulse);
            return;
        }
        enemyRigidBody.velocity = direction * speed;
    }
    public void SetBaseHealthString(string newString)
    {
        baseHealthString = newString.ToUpper(); 
        ResetHealthString();
    }
    public void SetSprite(Sprite newSprite)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = newSprite;
    }
    public void SetType(EnemySpawner.TypeOfFood newType)
    {
        enemyType = newType;
    }
    private void ResetBaseHealthString()
    {
        baseHealthString = null;
    }
    private void ResetHealthString()
    {
        healthString = baseHealthString;
        healthStringArray = healthString.ToCharArray();
    }
    public void RemoveCharacter(string text)
    {
        if(healthString == null || invulnerable == true)
        {
            return;
        }
        if(healthStringArray[0].ToString() == text.ToUpper())
        {
            healthString = healthString.Remove(0, 1);
            healthStringArray = healthString.ToCharArray();
            if(healthString.Length <= 0)
            {
                health--; 
                if (health <= 0)
                {
                    FindAnyObjectByType<FoodBalanceScript>().GetDamaged(enemyType, -FindAnyObjectByType<PlayerBehavior>().playerValues.damage);
                    FindAnyObjectByType<EnemySpawner>().enemiesDefeated++;
                    Die();
                }
                else
                {
                    ResetBaseHealthString();
                    StartCoroutine(Knockback());
                }
            }
           
        }
        else
        {
            ResetHealthString();
        }
    }
    IEnumerator Knockback()
    {
        knockback = true;
        yield return new WaitForSeconds(1);
        knockback = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Die();
            }
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
