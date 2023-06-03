using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSafe : MonoBehaviour
{
    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public static float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    public static string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public static void SetInt(string key, int meaning)
    {
        PlayerPrefs.SetInt(key, meaning);
    }

    public static void SetFloat(string key, float meaning)
    {
        PlayerPrefs.SetFloat(key, meaning);
    }

    public static void SetString(string key, string meaning)
    {
        PlayerPrefs.SetString(key, meaning);
    }
}
