using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFood : MonoBehaviour
{
    public SOFood SOFood;
    public float minX, maxX, minZ, maxZ;
    public int Count;
    public int MaxFood;
    public int StartRareFood;
    public int StartMageFood;

    public Coroutine Coroutine;

    private void Start()
    {
        Coroutine = StartCoroutine(SpawnObjects());
        for(int i = 0; i < StartRareFood; i++)
        {
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            int temp = Random.Range(0, 2);
            Food prefab = Instantiate(SOFood.ModelFoods[temp].prefab, new Vector3(x, 0.4f, z), Quaternion.identity);
            prefab.Init(SOFood.ModelFoods[temp], this);
            prefab.transform.localScale = Vector3.one * Random.Range(6, 9);
        }
        for (int i = 0; i < StartMageFood; i++)
        {
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            int temp = Random.Range(2, 4);
            Food prefab = Instantiate(SOFood.ModelFoods[temp].prefab, new Vector3(x, 0.4f, z), Quaternion.identity);
            prefab.Init(SOFood.ModelFoods[temp], this);
            prefab.transform.localScale = Vector3.one * Random.Range(6, 9);
        }
    }
    private void Update()
    {
        if(Coroutine == null && Count < MaxFood)
        {
            Coroutine = StartCoroutine(SpawnObjects());
        }
    }
    public IEnumerator SpawnObjects()
    {
        while (Count != MaxFood)
        {
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);

            yield return new WaitForSeconds(0.1f);

            int Percentage = Random.Range(0, 101);
            if(Percentage >= 20)
            {
                int temp = Random.Range(0, 2);
                Food prefab = Instantiate(SOFood.ModelFoods[temp].prefab, new Vector3(x, 0.4f, z), Quaternion.identity);
                prefab.Init(SOFood.ModelFoods[temp], this);
                prefab.transform.localScale = Vector3.one * Random.Range(6, 9);
                Count++;
            }
            if(Percentage >= 5 &&  Percentage < 20)
            {
                int temp = Random.Range(2, 4);
                Food prefab = Instantiate(SOFood.ModelFoods[temp].prefab, new Vector3(x, 0.4f, z), Quaternion.identity);
                prefab.Init(SOFood.ModelFoods[temp], this);
                prefab.transform.localScale = Vector3.one * Random.Range(6, 9);
                Count++;
            }
            if(Percentage >= 0 && Percentage < 5)
            {
                int temp = Random.Range(4, 6);
                Food prefab = Instantiate(SOFood.ModelFoods[temp].prefab, new Vector3(x, 0.4f, z), Quaternion.identity);
                prefab.Init(SOFood.ModelFoods[temp], this);
                prefab.transform.localScale = Vector3.one * Random.Range(6, 9);
                Count++;
            }
        }
        Coroutine = null;
    }    
}
