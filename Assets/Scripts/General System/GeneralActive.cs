using System.Collections;
using System.Collections.Generic;
using System;

public class GeneralActive
{
    Random rnd = new Random();

    public int Rand()
    {
        int value;
        value = rnd.Next(1, 5);
        return value;
    }

    public int Rand(int max)
    {
        int value;
        value = rnd.Next(0, max);
        return value;
    }

    public int Rand(int min, int max)
    {
        int value;
        value = rnd.Next(min, max);
        return value;
    }

    public float Rand(int level, int minValue, int maxValue)
    {
        int value;
        value = rnd.Next(minValue * level, maxValue * level);
        return (float)value;
    }
}
