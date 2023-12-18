using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodBalanceScript : MonoBehaviour
{
    public float SugarLevel, VegetableLevel, GrainLevel, MeatLevel = 0;
    public SliderBehavior sliderBehavior;
    public float maxMeatValues;
    public float maxSugarValues;
    public float maxVegetableValues;
    public float maxGrainValues;
    public int maxValue;
    public int baseValues;
    public void GetDamaged(EnemySpawner.TypeOfFood type, float damage)
    {
        switch (type)
        {
            case EnemySpawner.TypeOfFood.Sugary:
                if(SugarLevel + damage < 0)
                {
                    return;
                }
                SugarLevel += damage;
                break;
            case EnemySpawner.TypeOfFood.Meat:
                if (MeatLevel + damage < 0)
                {
                    return;
                }
                MeatLevel += damage;
                break;
            case EnemySpawner.TypeOfFood.Grains:
                if (GrainLevel + damage < 0)
                {
                    return;
                }
                GrainLevel += damage;
                break;
            case EnemySpawner.TypeOfFood.Vegetable:
                if (VegetableLevel + damage < 0)
                {
                    return;
                }
                VegetableLevel += damage;
                break;
        }
    }
    private void Start()
    {
        sliderBehavior = FindAnyObjectByType<SliderBehavior>();
        SetMaxValues();
    }
    private void FixedUpdate()
    {
        sliderBehavior.SetCurrentValues(
            maxMeatValues - MeatLevel, 
            maxSugarValues - SugarLevel , 
            maxVegetableValues - VegetableLevel, 
            maxGrainValues -  GrainLevel
            );
    }
    void SetMaxValues()
    {
        System.Random random = new System.Random();
        maxMeatValues = random.Next(baseValues, maxValue);
        maxSugarValues = random.Next(baseValues, maxValue);
        maxVegetableValues = random.Next(baseValues, maxValue);
        maxGrainValues = random.Next(baseValues, maxValue);
    }
    private void Update()
    {
        sliderBehavior.SetMaxValue(1, maxMeatValues);
        sliderBehavior.SetMaxValue(2, maxSugarValues);
        sliderBehavior.SetMaxValue(3, maxVegetableValues);
        sliderBehavior.SetMaxValue(4, maxGrainValues);
    }

}
