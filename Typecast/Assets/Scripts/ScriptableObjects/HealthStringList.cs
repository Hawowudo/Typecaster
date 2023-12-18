using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealthStringList : ScriptableObject
{
    public List<string> MeatStrings;
    public List<string> VegetableStrings;
    public List<string> GrainsStrings;
    public List<string> SugarStrings;

    public List<Sprite> SugarSprites;
    public List<Sprite> GrainsSprites;
    public List<Sprite> MeatSprites;
    public List <Sprite> VegetableSprites;
}
