using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBuff : MonoBehaviour
{
    public BaseBuffUp[] BaseBuffUp;
    public int Count;
    public int MaxCount;
    public float minX, maxX, minZ, maxZ;
    public Coroutine Coroutine;

    private void Start()
    {
        Coroutine = StartCoroutine(SpawnObjects());

    }
    private void Update()
    {
        if (Coroutine == null && Count < MaxCount)
        {
            Coroutine = StartCoroutine(SpawnObjects());
        }
    }
    public IEnumerator SpawnObjects()
    {
        while (Count != MaxCount)
        {
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);

            yield return new WaitForSeconds(3f);
            int tempRandom = Random.Range(0, BaseBuffUp.Length);
            BaseBuffUp prefab = Instantiate(BaseBuffUp[tempRandom], new Vector3(x, BaseBuffUp[tempRandom].Scale / 2, z), Quaternion.identity);
            prefab.Init(DownCount);
            Count++;
        }
        Coroutine = null;
    }
    private void DownCount()
    {
        Count--;
    }
}
