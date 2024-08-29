using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSkin : MonoBehaviour
{
    public SOSkin Skin;
    public CameraController controller;

    public void Spawner(int index)
    {
        if (controller.Player != null && controller.skin == null)
        {
            int temp = controller.Player.player.indexForLineRender - 1;
            float CountScore = controller.Player.player.CountScore;
            Skin prefab = Instantiate(Skin.ModelAllSkins[index].Prefab, controller.SpawnPos);
            prefab.Init(Skin.ModelAllSkins[index].modelSkins[temp], Skin.ModelAllSkins, controller);
            controller.skin = prefab;
            controller.cinemachine.m_TrackedObjectOffset = prefab.skin.CameraSetup;
            prefab.skin.CountScore = CountScore;
            StartCoroutine(Shield(prefab));
            if (MainUI.Instance.units.Contains(controller.Player))
            {
                MainUI.Instance.units.Remove(controller.Player);
            }    
            Destroy(controller.Player.gameObject);
            controller.Player = null;
            MainUI.Instance.skin = prefab;
            if (!MainUI.Instance.units.Contains(prefab))
            {
                MainUI.Instance.units.Add(prefab);
            }
        }
        else
        {
            int temp = controller.skin.skin.index - 1;
            float CountScore = controller.skin.skin.CountScore;
            Skin prefab = Instantiate(Skin.ModelAllSkins[index].Prefab, controller.SpawnPos);
            prefab.Init(Skin.ModelAllSkins[index].modelSkins[temp], Skin.ModelAllSkins, controller);
            prefab.skin.CountScore = CountScore;
            if (MainUI.Instance.units.Contains(controller.skin))
            {
                MainUI.Instance.units.Remove(controller.skin);
            }
            Destroy(controller.skin.gameObject);
            controller.skin = prefab;
            controller.cinemachine.m_TrackedObjectOffset = prefab.skin.CameraSetup;
            StartCoroutine(Shield(prefab));
            MainUI.Instance.skin = prefab;
            if (!MainUI.Instance.units.Contains(prefab))
            {
                MainUI.Instance.units.Add(prefab);
            }
        }
    }

    public IEnumerator Shield(Skin skin)
    {
        if (skin != null)
        {
            skin.gameObject.layer = 0;
        }
        yield return new WaitForSeconds(10);
        if (skin != null)
        {
            skin.gameObject.layer = 8;
        }
    }
}
