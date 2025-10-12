using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera startCamera; // ��������� ������
    [SerializeField] private Camera isoCamera; // �������������� ������
    [SerializeField] private Camera thirdPersonCamera; // ������ �� �������� ����
    public KeyCode switchKey = KeyCode.Tab; // ������� ��� ������������

    private bool useIsoCamera = true; // ����, �����������, ����� ������ �������

    void Start()
    {
        startCamera.enabled = true;
        // ���������� ���������� �������������� ������
        isoCamera.enabled = false;
        thirdPersonCamera.enabled = false;
    }

    void Update()
    {
        // ������������ ������� ������� Tab
        if (Input.GetKeyDown(switchKey))
        {
            useIsoCamera = !useIsoCamera; // ������ ����

            // ����������� ���������� �����
            isoCamera.enabled = useIsoCamera;
            thirdPersonCamera.enabled = !useIsoCamera;
            print($"useIsoCamera = {useIsoCamera}");
        }
    }

    public void BeginGame()
    {
        isoCamera.enabled = false;
        thirdPersonCamera.enabled = false;
        Invoke("OnCamera", 2f);
    }

    private void OnCamera()
    {
        // ���������� ���������� �������������� ������
        isoCamera.enabled = true;
        thirdPersonCamera.enabled = false;
    }
}
