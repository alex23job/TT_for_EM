using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // ��������, �� ������� ����� ��������� ������
    public float followSpeed = 5f; // ��������, � ������� ������ ����� ��������� �� ����������
    public float offsetY = 3f; // ������ �� ��� Y
    public float offsetZ = -5f; // �������� �� ��� Z

    void LateUpdate()
    {
        // ���� �������� ������� ���, ����������� ������
        if (target == null) return;

        // ������������ ������� ������
        Vector3 desiredPosition = target.position + new Vector3(0, offsetY, offsetZ);

        // �������� ���������� ������ � ������ ���������
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // ������� ������ �� ���������
        transform.LookAt(target.position);
    }
}
