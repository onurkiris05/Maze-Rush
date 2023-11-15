using UnityEngine;

public class Placer : MonoBehaviour
{
    [SerializeField] private int objectCount;
    [SerializeField] private float spacing;
    [SerializeField] private Transform showOffPlace;
    [SerializeField] private GameObject showOffPrefab;

    private void Start() => Place();

    private void Place()
    {
        for (int i = 0; i < objectCount; i++)
        {
            var totalWidth = (objectCount - 1) * spacing;
            var offset = -totalWidth / 2f + i * spacing;
            var spawnPosition = new Vector3(showOffPlace.position.x + offset,
                showOffPlace.position.y, showOffPlace.position.z);

            Instantiate(showOffPrefab, spawnPosition, Quaternion.identity, transform);
        }
    }
}