using System.Collections;
using UnityEngine;

[System.Serializable]
public class VFXData
{
    public string Name;
}

public class VFXSpawner : StaticInstance<VFXSpawner>
{
    [Header("Settings")]
    [SerializeField] private VFXData[] particleSystems;


    public void PlayVFX(string vfxName, Vector3 vfxPos)
    {
        var selectedVFX = GetItem(vfxName);
        if (selectedVFX == null) return;

        StartCoroutine(ProcessVFX(selectedVFX, vfxPos));
    }

    public void PlayVFX(string vfxName, Vector3 vfxPos, Transform parent)
    {
        var selectedVFX = GetItem(vfxName);
        if (selectedVFX == null) return;

        StartCoroutine(ProcessVFX(selectedVFX, vfxPos, parent));
    }

    public void PlayVFX(string vfxName, Vector3 vfxPos, Vector3 lookAt)
    {
        var selectedVFX = GetItem(vfxName);
        if (selectedVFX == null) return;

        StartCoroutine(ProcessVFX(selectedVFX, vfxPos, null, lookAt));
    }

    public void PlayVFX(string vfxName, Vector3 vfxPos, Transform parent, Vector3 lookAt)
    {
        var selectedVFX = GetItem(vfxName);
        if (selectedVFX == null) return;

        StartCoroutine(ProcessVFX(selectedVFX, vfxPos, parent, lookAt));
    }

    private IEnumerator ProcessVFX(
        VFXData vfxData,
        Vector3 vfxPos,
        Transform parent = default,
        Vector3 vfxRot = default)
    {
        var vfxObject = ObjectPooler.Instance.Spawn(vfxData.Name, vfxPos, transform);

        if (vfxObject.TryGetComponent(out ParticleSystem particle))
        {
            if (vfxRot != default)
                particle.transform.localRotation = Quaternion.Euler(vfxRot);

            if (parent != null)
                particle.transform.SetParent(parent);

            yield return Helpers.BetterWaitForSeconds(particle.main.duration);
            ObjectPooler.Instance.ReleasePooledObject(vfxData.Name, vfxObject);
        }
    }

    private VFXData GetItem(string name)
    {
        foreach (var vfxData in particleSystems)
        {
            if (vfxData.Name == name)
                return vfxData;
        }

        Debug.LogError($"{gameObject.name} - Particle system name not found!!!");
        return null;
    }
}