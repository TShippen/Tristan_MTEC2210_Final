using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
   
    public static float RemapRange(float oldValue, float oldMin, float oldMax, float newMin, float newMax)
    {
        float newValue = 0;
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        newValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;

        return newValue;
    }

    // a quick way to multiply a value by Time.deltaTime
    // this felt more visually organized than multiplying every time
    public static float FrameDependant(float value)
    {
        return value * Time.deltaTime;
    }


}
