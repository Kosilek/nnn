using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    private const float anchorMinXStart = 0.04f;
    private const float anchorMaxXStart = 0.12f;
    private const float anchorMinXEnd = 0.88f;
    private const float anchorMaxXEnd = 0.96f;
    private const float anchorSizeX = 0.12f;
    private float anchorSizeY = 0.25f;
    private float anchorDistY = 0.1f;
    private float distY = 0.05f;
    [SerializeField] private Font font;
    [SerializeField] private GameObject loadScene;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject content;
    [SerializeField] private int level;
    private float anchorMinX = 0.04f;
    private float anchorMinY;
    private float anchorMaxX = 0.12f;
    private float anchorMaxY;
    private float localScaleY;
    private void Start()
    {
        ExtensionParetPanel();
        CoordinateCalculationY();
        CreateLevel(level);
    }

    private void ExtensionParetPanel()
    {
        localScaleY = Mathf.Round(level / 24 + 1f);
        content.GetComponent<RectTransform>().localScale = new Vector3(1, localScaleY, 1);
    }

    private void CoordinateCalculationY()
    {
        distY /= localScaleY;
        anchorSizeY /= localScaleY;
        anchorDistY /= localScaleY;
        anchorMaxY = 1 - distY;
        anchorMinY = anchorMaxY - anchorSizeY;
    }

    private void CreateLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            CreateButton((i + 1).ToString(), (i + 1).ToString(), content.transform, i, anchorMinX, anchorMinY, anchorMaxX, anchorMaxY, true);
            anchorMinX += anchorSizeX;
            anchorMaxX += anchorSizeX;
            if (anchorMinX > anchorMinXEnd && anchorMaxX > anchorMaxXEnd)
            {
                anchorMinY -= (anchorSizeY + anchorDistY);
                anchorMaxY -= (anchorSizeY + anchorDistY);
                anchorMinX = anchorMinXStart;
                anchorMaxX = anchorMaxXStart;
            }
        }
    }

    public void StartLevel(int x)
    {
        PlayerPrefsSafe.SetInt(MeaningString.level, x);
        levelPanel.SetActive(false);
        Instantiate(loadScene, gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine(LoadScene.LoadAsync(MeaningString.sceneLevel));
    }

    private void CreateButton(string nameButton, string textButton, Transform spawnBlock, int level, float anchorMinX, float anchorMinY, float anchorMaxX, float anchorMaxY, bool WinLastLevel)
    {
        GameObject newButton = new GameObject(nameButton, typeof(Image), typeof(Button), typeof(LayoutElement));
        newButton.transform.SetParent(spawnBlock.transform);
        newButton.GetComponent<RectTransform>().position = new Vector3(0f, 0f, 0f);
        newButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        RectTransform rtButton = newButton.GetComponent<RectTransform>();
        rtButton.anchorMin = new Vector2(anchorMinX, anchorMinY);
        rtButton.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
        rtButton.anchoredPosition = new Vector2(0f, 0f);
        rtButton.sizeDelta = new Vector2(0f, 0f);
        CreateText($"Text{nameButton}", textButton, newButton.transform);
        newButton.GetComponent<Button>().onClick.AddListener(delegate { StartLevel(level); });
    }

    private void CreateText(string nameText, string textText, Transform spawnBlock)
    {
        GameObject newText = new GameObject(nameText, typeof(Text));
        newText.transform.SetParent(spawnBlock);
        newText.GetComponent<Text>().text = textText;
        newText.GetComponent<Text>().font = font;
        newText.GetComponent<Text>().resizeTextForBestFit = true;
        newText.GetComponent<Text>().resizeTextMaxSize = 100;
        newText.GetComponent<Text>().resizeTextMinSize = 14;
        RectTransform rt = newText.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.anchoredPosition = new Vector2(0, 0);
        rt.sizeDelta = new Vector2(0, 0);
        newText.GetComponent<Text>().color = Color.black;
        newText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
    }
}
