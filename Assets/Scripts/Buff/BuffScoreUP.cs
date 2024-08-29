using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffScoreUP : BaseBuffUp
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform Canvas;
    public int RandomTotal;
    public override void Start()
    {
        RandomTotal = Random.Range(400, 1500);
        base.Start();
        text.text = $"{RandomTotal}";
    }
    public override void Update()
    {
        CinemachineBrain cinemachineBrain = FindObjectOfType<CinemachineBrain>();

        // ���������, ������ �� CinemachineBrain
        if (cinemachineBrain != null)
        {
            // �������� ������ �������� ����������� ������
            CinemachineVirtualCamera activeVirtualCamera = (CinemachineVirtualCamera)cinemachineBrain.ActiveVirtualCamera;
            Canvas.transform.LookAt(activeVirtualCamera.VirtualCameraGameObject.transform.position);
        }
    }
    public override BaseBuff Setup()
    {
        return new BuffScore(RandomTotal);
    }
}
