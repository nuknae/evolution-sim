using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomMath
{
    public static float GaussianRandom(float mean, float stdDev, float min, float max)
    {
        double u, v, S;
        float fac, ret;
        


        
            do
            {
                u = 2.0f * Random.value - 1.0f;
                v = 2.0f * Random.value - 1.0f;
                S = u * u + v * v;
            }
            while (S >= 1.0f);




            fac = Mathf.Sqrt((float)(-2.0f * Mathf.Log((float)S) / S));

            ret = ((float)u * fac) * stdDev + mean;

        if (ret <= min)
            return min;
        else if (ret >= max)
            return max;

        return ret;
    }


}
