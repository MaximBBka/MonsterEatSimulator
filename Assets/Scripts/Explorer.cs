using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explorer : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void GoTo(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Run()
    {
        Time.timeScale = 1;
    }
    public void AdsBonus(int index)
    {
        if (AdsProvider.Instance != null)
        {
            if(AdsProvider.Instance.reawrdId != -1)
            {
                AdsProvider.Instance.reawrdId = -1;
            }
            AdsProvider.Instance.RewardedAbyliti(index);
        }
    }
}
