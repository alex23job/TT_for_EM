using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera isoCamera; // �������������� ������
    [SerializeField] private Camera thirdPersonCamera; // ������ �� �������� ����
    public KeyCode switchKey = KeyCode.Tab; // ������� ��� ������������

    private bool useIsoCamera = true; // ����, �����������, ����� ������ �������

    void Start()
    {
        // ���������� ���������� �������������� ������
        isoCamera.enabled = true;
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
}
