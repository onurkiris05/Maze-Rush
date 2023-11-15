using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler : StaticInstance<ObjectPooler>
{
    [SerializeField] private List<PoolClass> poolObjects;
    private Dictionary<string, ObjectPool<GameObject>> _gameObjectDict;

    [Serializable]
    public class PoolClass
    {
        [Tooltip("Give a tag to the pool for calling it")]
        public string tag;

        [Tooltip("Prefab of the GameObject to be pooled")]
        [ShowAssetPreview]
        public GameObject prefab;

        [Tooltip("The size (count) of the pool")]
        public int softCap, hardCap;

        [Tooltip("Whether the GameObject create at Start")]
        public bool createAtStart;
    }

    #region UNITY EVENTS

    private void Awake()
    {
        Initialize();
        CreateAtStart();
    }

    #endregion


    #region PUBLIC METHODS

    /// <summary>
    /// Spawns the pooled GameObject to given position
    /// </summary>
    /// <param name="poolTag">Tag of the GameObject to be spawned</param>
    /// <param name="position">Set the world position of the GameObject</param>
    /// <returns>The GameObject found matching the tag specified</returns>
    public GameObject Spawn(string poolTag, Vector3 position)
    {
        var pooledObject = _gameObjectDict[poolTag].Get();
        var t = pooledObject.transform;
        t.position = position;
        pooledObject.gameObject.SetActive(true);
        return pooledObject;
    }

    /// <summary>
    /// Spawns the pooled GameObject to given position and rotation
    /// </summary>
    /// <param name="poolTag">Tag of the GameObject to be spawned</param>
    /// <param name="position">Set the world position of the GameObject</param>
    /// <param name="rotation">Set the rotation of the GameObject</param>
    /// <returns>The GameObject found matching the tag specified</returns>
    public GameObject Spawn(string poolTag, Vector3 position, Quaternion rotation)
    {
        var pooledObject = _gameObjectDict[poolTag].Get();
        var t = pooledObject.transform;
        t.position = position;
        t.rotation = rotation;
        pooledObject.gameObject.SetActive(true);
        return pooledObject;
    }

    /// <summary>
    /// Spawns the pooled GameObject and parents the GameObject to given Transform
    /// </summary>
    /// <param name="poolTag">Tag of the GameObject to be spawned</param>
    /// <param name="parent">Parent that will be assigned to the GameObject</param>
    /// <returns>The GameObject found matching the tag specified</returns>
    public GameObject Spawn(string poolTag, Transform parent)
    {
        var pooledObject = _gameObjectDict[poolTag].Get();
        var t = pooledObject.transform;
        t.transform.parent = parent;
        t.localPosition = Vector3.zero;
        pooledObject.gameObject.SetActive(true);
        return pooledObject;
    }

    /// <summary>
    /// Spawns the pooled GameObject to given position and parents the GameObject to given Transform
    /// </summary>
    /// <param name="poolTag">Tag of the GameObject to be spawned</param>
    /// <param name="position">Set the world position of the GameObject</param>
    /// <param name="parent">Parent that will be assigned to the GameObject</param>
    /// <returns>The GameObject found matching the tag specified</returns>
    public GameObject Spawn(string poolTag, Vector3 position, Transform parent)
    {
        var pooledObject = _gameObjectDict[poolTag].Get();
        var t = pooledObject.transform;
        t.position = position;
        t.parent = parent;
        pooledObject.gameObject.SetActive(true);
        return pooledObject;
    }

    /// <summary>
    /// Spawns the pooled GameObject to given position and rotation and parents the GameObject to given Transform
    /// </summary>
    /// <param name="poolTag">Tag of the GameObject to be spawned</param>
    /// <param name="position">Set the world position of the GameObject</param>
    /// <param name="rotation">Set the rotation of the GameObject</param>
    /// <param name="parent">Parent that will be assigned to the GameObject</param>
    /// <returns>The GameObject found matching the tag specified</returns>
    public GameObject Spawn(string poolTag, Vector3 position, Quaternion rotation, Transform parent)
    {
        var pooledObject = _gameObjectDict[poolTag].Get();
        var t = pooledObject.transform;
        t.position = position;
        t.rotation = rotation;
        t.parent = parent;
        pooledObject.SetActive(true);
        return pooledObject;
    }

    public void ReleasePooledObject(string poolTag, GameObject pooledObject)
    {
        _gameObjectDict[poolTag].Release(pooledObject);
    }

    #endregion


    #region PRIVATE METHODS

    private void CreateAtStart()
    {
        for (int i = 0; i < poolObjects.Count; i++)
        {
            var p = poolObjects[i];
            if (p.createAtStart)
            {
                for (int j = 0; j < p.softCap; j++)
                {
                    _gameObjectDict[p.tag].Get();
                }
            }
        }
    }

    private void Initialize()
    {
        _gameObjectDict = new Dictionary<string, ObjectPool<GameObject>>();
        var listCount = poolObjects.Count;
        for (int i = 0; i < listCount; i++)
        {
            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreateFunction(i), OnGameObjectGet,
                OnGameObjectRelease, OnGameObjectDestroy, true,
                poolObjects[i].softCap, poolObjects[i].hardCap);
            string prefabName = poolObjects[i].tag;
            _gameObjectDict.Add(prefabName, pool);
        }
    }

    private void OnGameObjectGet(GameObject pooledObject)
    {
        pooledObject.transform.parent = transform;
        pooledObject.gameObject.SetActive(false);
    }

    private Func<GameObject> CreateFunction(int i)
    {
        return new Func<GameObject>(() =>
        {
            var pooledObject = Instantiate(this.poolObjects[i].prefab);
            return pooledObject;
        });
    }

    private void OnGameObjectRelease(GameObject pooledObject)
    {
        pooledObject.SetActive(false);
    }

    private void OnGameObjectDestroy(GameObject pooledObject)
    {
        Destroy(pooledObject);
    }

    #endregion
}