using UnityEngine;
using DG.Tweening;
using TMPro;
using Zenject;

[System.Serializable]
public class TextData
{
    public string Name;
    public Color Color;
    public float FontSize;
}

public class TextSpawner : StaticInstance<TextSpawner>
{
    [Header("Settings")]
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private TextData[] textDatas;


    #region SpawnAndMove

    public void SpawnAndMove(string textName, string msg, Vector3 worldPosition, RectTransform targetRectTransform)
    {
        var selectedTextData = GetItem(textName);
        if (selectedTextData == null) return;

        ProcessSpawnAndMove(selectedTextData, msg, worldPosition, targetRectTransform);
    }

    private void ProcessSpawnAndMove(TextData textData, string msg, Vector3 worldPos, RectTransform targetRect)
    {
        var textObject = ObjectPooler.Instance.Spawn("Text", parentCanvas.transform);
        if (textObject.TryGetComponent(out TextMeshProUGUI text))
        {
            text.text = msg;
            text.color = textData.Color;
            text.fontSize = textData.FontSize;

            var screenPosition = Camera.main.WorldToScreenPoint(worldPos);
            text.transform.position = screenPosition;

            text.transform.DOMove(targetRect.position, 0.7f).SetEase(Ease.InBack)
                .OnComplete(() => ObjectPooler.Instance.ReleasePooledObject("Text", text.gameObject));
        }
    }

    #endregion

    #region SpawnAndFade

    public void SpawnAndFade(string textName, string msg, Vector3 worldPosition, float fadeDuration)
    {
        var selectedTextData = GetItem(textName);
        if (selectedTextData == null) return;

        ProcessSpawnAndFade(selectedTextData, msg, worldPosition, fadeDuration);
    }

    private void ProcessSpawnAndFade(TextData textData, string msg, Vector3 worldPos, float fadeDuration)
    {
        var textObject = ObjectPooler.Instance.Spawn("Text", parentCanvas.transform);

        if (textObject.TryGetComponent(out TextMeshProUGUI text))
        {
            text.text = msg;
            text.color = textData.Color;
            text.fontSize = textData.FontSize;

            var screenPosition = Camera.main.WorldToScreenPoint(worldPos);
            text.transform.position = screenPosition;

            text.DOFade(0, fadeDuration).SetEase(Ease.InExpo);
            text.transform.DOMoveY(text.transform.position.y + 100f, fadeDuration).SetEase(Ease.Linear)
                .OnComplete(() => ObjectPooler.Instance.ReleasePooledObject("Text", text.gameObject));
        }
    }

    #endregion

    private TextData GetItem(string name)
    {
        foreach (var textData in textDatas)
        {
            if (textData.Name == name)
                return textData;
        }

        Debug.LogError($"{gameObject.name} - Text name not found!!!");
        return null;
    }
}