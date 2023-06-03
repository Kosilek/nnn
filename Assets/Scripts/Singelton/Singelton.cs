using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                    Debug.LogError($"Simple singleton of class : {typeof(T)} does not exists");
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (!Application.isPlaying)
            return;

        T[] check = FindObjectsOfType<T>();
        if (check.Length > 0)
        {
            foreach (T searched in check)
            {
                if (searched != this)
                {
                    Destroy(this.gameObject);
                    return;
                }
            }
        }

        //   DontDestroyOnLoad(this.gameObject);

        if (!_instance)
            _instance = this as T;
    }
}
