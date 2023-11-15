using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}