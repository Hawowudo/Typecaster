using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehavior : MonoBehaviour
{
    public Slider meatSlider;
    public Slider sugarSlider;
    public Slider vegetableSlider;
    public Slider GrainSlider;
    // Start is called before the first frame update
    public void SetMaxValue(int type, float value)
    {
        switch (type)
        {
            case 1:
                meatSlider.maxValue = value;
                break;
            case 2:
                sugarSlider.maxValue = value;
                break;
            case 3:
                vegetableSlider.maxValue = value;
                break;
            case 4:
                GrainSlider.maxValue = value;
                break;
            default:
                break;

        }
    }
    public void SetCurrentValues(float meat, float sugar, float vegetable, float grain)
    {
        meatSlider.value = meat;
        sugarSlider.value = sugar;
        vegetableSlider.value = vegetable;
        GrainSlider.value = grain;
    }
    void ImbalanceDetection()
    {
        if (meatSlider.value <= 0)
        {
            FindAnyObjectByType<UIController>().enableGameOver();
        }
        if (sugarSlider.value <= 0)
        {
            FindAnyObjectByType<UIController>().enableGameOver();
        }
        if (vegetableSlider.value <= 0)
        {
            FindAnyObjectByType<UIController>().enableGameOver();
        }
        if (GrainSlider.value <= 0)
        {
            FindAnyObjectByType<UIController>().enableGameOver();
        }
    }
    // Update is called once per frame
    void Update()
    {
        ImbalanceDetection();
    }
}
