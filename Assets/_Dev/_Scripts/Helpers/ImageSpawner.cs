using System.Collections;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.UI;

[System.Serializable]
public class ImageData
{
    public string Name;
    [ShowAssetPreview]
    public Sprite ImageToSpawn;
}

public class ImageSpawner : StaticInstance<ImageSpawner>
{
    [Header("Settings")]
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private ImageData[] imageDatas;


    #region SpawnAndMove

    public void SpawnAndMove(string imageName, Vector3 worldPosition, RectTransform targetRectTransform, int count = 1)
    {
        var selectedImageData = GetItem(imageName);
        if (selectedImageData == null) return;

        StartCoroutine(ProcessSpawnAndMove(selectedImageData, worldPosition, targetRectTransform, count));
    }

    private IEnumerator ProcessSpawnAndMove(ImageData imageData, Vector3 worldPos, RectTransform targetRect, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var imageObject = ObjectPooler.Instance.Spawn("Image", parentCanvas.transform);

            if (imageObject.TryGetComponent(out Image image))
            {
                var screenPosition = Camera.main.WorldToScreenPoint(worldPos);
                image.transform.position = screenPosition;
                image.sprite = imageData.ImageToSpawn;

                image.transform.DOMove(targetRect.position, 0.7f).SetEase(Ease.InBack)
                    .OnComplete(() => ObjectPooler.Instance.ReleasePooledObject("Image", image.gameObject));

                yield return Helpers.BetterWaitForSeconds(0.12f);
            }
        }
    }

    private ImageData GetItem(string name)
    {
        foreach (var imageData in imageDatas)
        {
            if (imageData.Name == name)
                return imageData;
        }

        Debug.LogError($"{gameObject.name} - Image name not found!!!");
        return null;
    }

    #endregion
}