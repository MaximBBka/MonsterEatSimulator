using System.Collections;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public SOPlayers SOPlayers;
    public CameraController CameraController;
    public FindFreePoss FindFreePoss;

    private int index = 0;
    public Player Spawner(Player player = null)
    {
        Vector3 pos;

        Player prefab = null;
        if (player == null)
        {
            CameraController.Player = SOPlayers._modelPlayer[index].Prefab;           
            prefab = Instantiate(SOPlayers._modelPlayer[index].Prefab, CameraController.SpawnPos);
            prefab.Init(SOPlayers._modelPlayer[index]);
            
            if (MainUI.Instance != null)
            {
                if (!MainUI.Instance.units.Contains(prefab))
                {
                    MainUI.Instance.units.Add(prefab);
                }
                MainUI.Instance.player = prefab;
            }            
            StartCoroutine(Shield(prefab));
            if(AdsProvider.Instance != null)
            {
                prefab.Eat(AdsProvider.Instance.StartScoreAds);
                AdsProvider.Instance.StartScoreAds = 0;
            }            
            index++;
            return prefab;
        }
        if (SOPlayers._modelPlayer.Length <= index)
        {
            return player;
        }
        CameraController.Player = SOPlayers._modelPlayer[index].Prefab;
        prefab = Instantiate(SOPlayers._modelPlayer[index].Prefab, CameraController.SpawnPos);
        prefab.Init(SOPlayers._modelPlayer[index]);
        prefab.Eat(player.player.CountScore);
        if (!MainUI.Instance.units.Contains(prefab))
        {
            MainUI.Instance.units.Add(prefab);
        }
        MainUI.Instance.player = prefab;
        index++;
        return prefab;
    }
    public void DeSpawner(Player player)
    {
        if (player.player.index == index)
        {
            return;
        }
        if (MainUI.Instance.units.Contains(player))
        {
            MainUI.Instance.units.Remove(player);
        }
        Destroy(player.gameObject);
    }

    public IEnumerator Shield(Player player)
    {
        if(player !=  null)
        {
            player.gameObject.layer = 0;
        }       
        yield return new WaitForSeconds(10);
        if (player != null)
        {
            player.gameObject.layer = 8;
        }
    }
}
