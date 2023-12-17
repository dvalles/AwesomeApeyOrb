using UnityEngine;

public static class MyMath {

    #region Bias
        
    //bias function
    public static float Bias(float x, float b)
    {
        return Mathf.Pow(x, Mathf.Log10(b) / Mathf.Log10(.5f));
    }

    //gain function
    public static float Gain(float x, float g)
    {
        if (x < .5)
            return Bias(1f - g, 2 * x) / 2f;
        else
            return 1 - Bias(1f - g, 2f - 2f * x) / 2f;
    }

    #endregion

    #region Gaussian Sampling
        
    //sample from a clamped gaussian
    public static float NextGaussian (float mean, float standard_deviation, float min, float max) {
        float x;
        do {
            x = NextGaussian(mean, standard_deviation);
        } while (x < min || x > max);
        return x;
    }

    //sample from a gaussian with mean and standard deviation
    public static float NextGaussian(float mean, float standard_deviation)
    {
        return mean + NextGaussian() * standard_deviation;
    }

    //sample from a normal gaussian
    public static float NextGaussian() {
        float v1, v2, s;
        do {
            v1 = 2.0f * Random.Range(0f,1f) - 1.0f;
            v2 = 2.0f * Random.Range(0f,1f) - 1.0f;
            s = v1 * v1 + v2 * v2;
        } while (s >= 1.0f || s == 0f);
    
        s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);
    
        return v1 * s;
    }

    #endregion

    #region Randoms
    
    public static bool RandomBool()
    {
        return System.Convert.ToBoolean(UnityEngine.Random.Range(0,2));
    }   

    #endregion

    //loop a number over a given set of numbers by shift
    public static int ShiftOverLoop(int num, int shift, int loopLowerBoundInc, int loopUpperBoundInc)
    {
        int lb = loopLowerBoundInc; //alias
        int ub = loopUpperBoundInc;

        //catch problems
        if (lb >= ub)
            throw new System.Exception("Lower Bound can't be higher or the same as upper bound");
        if (num < lb || num > ub)
            throw new System.Exception("Num must start within the loop");
        
        int n = ub - lb;
        shift = shift % (n+1); //loop shift
        if(shift > 0)
        {
            if (num+shift > ub)
                return (num+shift) - (n+1);
            return num+shift;
        }
        if(shift < 0)
        {
            if (num+shift < lb)
                return (num+shift) + (n+1);
            return (num+shift);
        }
        if(shift == 0)
            return num;
        return 0;
    }

    //round number to number of decimal points
    public static float RoundTo(this float num, int numDecimals)
    {
        num *= Mathf.Pow(10, numDecimals);
        num = Mathf.Floor(num);
        num /= Mathf.Pow(10, numDecimals);
        return num;
    }

    //grab the fractional part of a number
    public static float Frac(this float num)
    {
        float integer = Mathf.Floor(num); //4.5 -> 4
        return num - integer; //4.5 - 4 -> .5
    }

}
