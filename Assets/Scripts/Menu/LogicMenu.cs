using System.Collections;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using static System.Net.Mime.MediaTypeNames;

public class LogicMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField InputField;
    public void InputTotal()
    {
        YandexGame.savesData.newPlayerName = InputField.text;
        YandexGame.SaveProgress();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}