using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static bool endOfLoading = false;
    public static IEnumerator LoadAsync(string level)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {

            if (asyncLoad.progress >= 0.9f && !asyncLoad.allowSceneActivation)
            {
                endOfLoading = true;
                if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
