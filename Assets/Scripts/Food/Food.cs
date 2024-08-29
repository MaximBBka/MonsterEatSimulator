using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public ModelFood modelFood;
    public SpawnerFood spawnerFood;
    public void Init(ModelFood _modelFood, SpawnerFood spawner)
    {
        modelFood = _modelFood;
        spawnerFood = spawner;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ITryEat>(out ITryEat eat))
        {
            eat.Eat(modelFood.totalCount);
            //player.player.CountScore += modelFood.totalCount;
            spawnerFood.Count--;
            Destroy(gameObject);
        }
    }
    public void Eaten()
    {
        spawnerFood.Count--;
        Destroy(gameObject);        
    }
}
