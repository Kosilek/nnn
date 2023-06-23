using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPanel : MonoBehaviour
{
    [SerializeField] private GameObject loadScene;
    public void StartLevel(int x)
    {
       // PlayerPrefsSafe.SetInt(MeaningString.level, x);
    //    levelPanel.SetActive(false);
        Instantiate(loadScene, gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine(LoadScene.LoadAsync(x));
    }
}
