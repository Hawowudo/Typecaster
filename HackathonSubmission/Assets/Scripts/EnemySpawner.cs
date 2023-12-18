using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public HealthStringList HealthList;
    public List<EnemyBehavior> listOfAllEnemies;
    public Collider2D playerAttackCollider;
    public GameObject enemyBase;
    public int enemyCount, maxEnemyCount, enemiesDefeated;
    public bool canSpawn;
    public float currentInterval;
    public enum TypeOfFood
    {
        Sugary,
        Meat,
        Vegetable,
        Grains,
    }
    private void Start()
    {
        enemiesDefeated = 0;
        SpawnEnemiesAtAnInterval(2);
    }
    void SetAllEnemies()
    {
        listOfAllEnemies.RemoveAll(obj => obj == null);
        listOfAllEnemies = FindObjectsOfType<EnemyBehavior>().ToList();
    }
    public List<EnemyBehavior> GetListOfAllEnemies()
    {
        return listOfAllEnemies;
    }
    void EnemyInvulnerabilityManager()
    {
        foreach (EnemyBehavior enemy in listOfAllEnemies)
        {
            if(Physics2D.IsTouching(playerAttackCollider, enemy.GetComponent<Collider2D>()))
            {
                enemy.invulnerable = false;
            }
            else
            {
                enemy.invulnerable = true;
            }
        }
    }
    TypeOfFood GetRandomType()
    {
        System.Random Random = new System.Random();
        switch(Random.Next(0,4)) 
        {
            case 0:
                return TypeOfFood.Sugary;
            case 1:
                return TypeOfFood.Meat;
            case 2:
                return TypeOfFood.Vegetable;
            case 3: 
                return TypeOfFood.Grains;
            default:
                return TypeOfFood.Sugary;
        }
    }
    Sprite GetRandomSprite(TypeOfFood type)
    {
        System.Random random = new System.Random();
        switch(type)
        {
            case TypeOfFood.Sugary:
                return HealthList.SugarSprites[random.Next(0, HealthList.SugarSprites.Count - 1)];
            case TypeOfFood.Meat:
                return HealthList.MeatSprites[random.Next(0, HealthList.MeatSprites.Count - 1)];
            case TypeOfFood.Vegetable:
                return HealthList.VegetableSprites[random.Next(0, HealthList.VegetableSprites.Count -1)];
            case TypeOfFood.Grains:
                return HealthList.GrainsSprites[random.Next(0, HealthList.VegetableSprites.Count - 1)];
            default:
                Debug.LogWarning("no sprite found");
                return null;
        }
    }
    void SpawnEnemies()
    {
        if(canSpawn == false) return;
        if(enemyCount >= maxEnemyCount)
        {
            Debug.Log("Max enemy count reached");
            return;
        }
        System.Random random = new System.Random();
        GameObject newEnemy = Instantiate(enemyBase, GetRandomLocation(), Quaternion.identity, this.transform);
        EnemyBehavior newEnemyBehavior = newEnemy.GetComponent<EnemyBehavior>();
        newEnemyBehavior.SetType(GetRandomType());
        newEnemyBehavior.SetSprite(GetRandomSprite(newEnemyBehavior.enemyType));
        newEnemyBehavior.speed = (float)(random.Next(0,1) + random.NextDouble());
    }
    public void SpawnEnemiesAtAnInterval(float seconds)
    {
        currentInterval = seconds;
        CancelInvoke("SpawnEnemies");
        InvokeRepeating("SpawnEnemies", 0, seconds);
    }
    Vector2 GetRandomLocation()
    {
        System.Random random = new System.Random();
        Vector2 randomLocation = Vector2.zero;
        switch(random.Next(0,4))
        {
            case 0:
                randomLocation = new Vector2(playerAttackCollider.bounds.min.x, random.Next((int)playerAttackCollider.bounds.min.y + (int)playerAttackCollider.bounds.max.y));
                break;
            case 1:
                randomLocation = new Vector2(playerAttackCollider.bounds.max.x, random.Next((int)playerAttackCollider.bounds.min.y + (int)playerAttackCollider.bounds.max.y));
                break;
            case 2:
                randomLocation = new Vector2(random.Next((int)playerAttackCollider.bounds.min.x, (int)playerAttackCollider.bounds.max.x) , playerAttackCollider.bounds.min.y);
                break;
            case 3:
                randomLocation = new Vector2(random.Next((int)playerAttackCollider.bounds.min.x, (int)playerAttackCollider.bounds.max.x), playerAttackCollider.bounds.max.y);
                break;
        }
        return randomLocation;
    }
    void GiveBaseHealthString()
    {
        foreach(EnemyBehavior item in listOfAllEnemies)
        {
            if(item.baseHealthString == null)
            {
                item.SetBaseHealthString(GetRandomHealthString(item.enemyType));
            }
        }
    }
    string GetRandomHealthString(TypeOfFood type)
    {
        System.Random rand = new System.Random();
        switch (type)
        {
            case TypeOfFood.Sugary:
                return HealthList.SugarStrings[rand.Next(0, HealthList.SugarStrings.Count - 1)];
            case TypeOfFood.Meat:
                return HealthList.MeatStrings[rand.Next(0, HealthList.MeatStrings.Count - 1)];
            case TypeOfFood.Vegetable:
                return HealthList.VegetableStrings[rand.Next(0, HealthList.VegetableStrings.Count - 1)];
            case TypeOfFood.Grains:
                return HealthList.GrainsStrings[rand.Next(0, HealthList.GrainsStrings.Count - 1)];
        }
        return null;

    }
    void Update()
    {
        SetAllEnemies(); 
        GiveBaseHealthString();
        EnemyInvulnerabilityManager();
    }
}
