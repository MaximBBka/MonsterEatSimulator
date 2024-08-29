using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public SOEnemy SOEnemy;
    public float minX, maxX, minZ, maxZ;
    public int CountEnemy1 = 1;
    public int CountEnemy2 = 1;
    public int CountEnemy3 = 1;
    public int CountEnemy4 = 1;
    public int CountEnemy5 = 1;
    public int CountEnemy6 = 1;
    public int MaxEnemy1 = 3;
    public int MaxEnemy2 = 3;
    public int MaxEnemy3 = 3;
    public int MaxEnemy4 = 3;
    public int MaxEnemy5 = 3;
    public int MaxEnemy6 = 3;
    public int AllMaxEnemy = 9;
    public int AllCountEnemy = 3;
    public FindFreePoss FindFreePoss;

    private void Start()
    {
        StartCoroutine(UpdateSpawnObjects());
    }
    public IEnumerator SpawnObjects(float time, ModelEnemy enemy, float score)
    {
        yield return new WaitForSeconds(time);
        Enemy prefab = Instantiate(enemy.Prefab, FindFreePoss.FreePoss(enemy.Scale * 2f), Quaternion.identity);
        prefab.Init(enemy, this);
        prefab.Eat(score);
        prefab.index = prefab.model.index;
        prefab.indexBirth = prefab.index - 1;
        prefab.textName.text = $"{prefab.Nickname[Random.Range(0, prefab.Nickname.Length)]}";
        if (!MainUI.Instance.units.Contains(prefab))
        {
            MainUI.Instance.units.Add(prefab);
        }
    }
    public IEnumerator UpdateSpawnObjects()
    {
        while (true)
        {
            if (AllMaxEnemy > AllCountEnemy)
            {
                if (MaxEnemy1 > CountEnemy1)
                {
                    CountEnemy1++;
                    AllCountEnemy++;
                    yield return SpawnObjects(1, SOEnemy.modelEnemies[0], Random.Range(0, 1500));
                }
                if (MaxEnemy2 > CountEnemy2)
                {
                    CountEnemy2++;
                    AllCountEnemy++;
                    yield return SpawnObjects(1, SOEnemy.modelEnemies[1], Random.Range(2001, 3000));
                }
                if (MaxEnemy3 > CountEnemy3)
                {
                    CountEnemy3++;
                    AllCountEnemy++;
                    yield return SpawnObjects(1, SOEnemy.modelEnemies[2], Random.Range(5001, 7000));
                }
                if (MaxEnemy4 > CountEnemy4)
                {
                    CountEnemy4++;
                    AllCountEnemy++;
                    yield return SpawnObjects(1, SOEnemy.modelEnemies[3], Random.Range(9001, 12000));
                }
                if (MaxEnemy5 > CountEnemy5)
                {
                    CountEnemy5++;
                    AllCountEnemy++;
                    yield return SpawnObjects(1, SOEnemy.modelEnemies[4], Random.Range(15001, 20000));
                }
                if (MaxEnemy6 > CountEnemy6)
                {
                    CountEnemy5++;
                    AllCountEnemy++;
                    yield return SpawnObjects(1, SOEnemy.modelEnemies[5], Random.Range(25001, 30000));
                }
            }
            yield return null;
        }
    }
    public Enemy RespawnEnemy(Enemy enemy)
    {
        Enemy prefab;
        if (SOEnemy.modelEnemies.Length <= enemy.index)
        {
            return enemy;
        }
        prefab = Instantiate(SOEnemy.modelEnemies[enemy.index].Prefab, enemy.transform.position, Quaternion.identity);
        prefab.Init(SOEnemy.modelEnemies[enemy.index], this);
        prefab.index = prefab.model.index;
        prefab.Eat(enemy.Eaten());
        if (!MainUI.Instance.units.Contains(prefab))
        {
            MainUI.Instance.units.Add(prefab);
        }
        prefab.textName.text =  $"{enemy.textName.text}";
        return prefab;
    }
    public void DeSpawner(Enemy enemy)
    {
        if (enemy.model.index == SOEnemy.modelEnemies[SOEnemy.modelEnemies.Length - 1].index)
        {
            return;
        }
        if (MainUI.Instance.units.Contains(enemy))
        {
            MainUI.Instance.units.Remove(enemy);
        }
        Destroy(enemy.gameObject);
    }
}
