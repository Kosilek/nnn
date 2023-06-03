using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CntrLoadScene : MonoBehaviour
{
    public Text text;
    public float timer;
    private string[] textLoad = { "Loading.", "Loading..", "Loading..." };
    private string finishLoad = "Нажмите любую кнопку";

    private void FixedUpdate()
    {
        if (!LoadScene.endOfLoading)
        {
            timer += Time.deltaTime;
            if (timer > 0f && timer <= 0.5f)
            {
                text.text = textLoad[0];
            }
            else if (timer > 0.5f && timer <= 1f)
            {
                text.text = textLoad[1];
            }
            else if (timer >= 1 && timer < 1.5f)
            {
                text.text = textLoad[2];
            }
            else if (timer >= 1.5f)
            {
                timer = 0;
            }
        }
        else if (LoadScene.endOfLoading)
        {
            text.text = finishLoad;
        }
    }
}
