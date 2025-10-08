using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCameraFollower : MonoBehaviour
{
    public Transform target; // ��������, �� ������� ����� ��������� ������
    public float followSpeed = 5f; // ��������, � ������� ������ ����� ��������� �� ����������
    public float isoOffsetX = 10f; // �������� �� ��� X
    public float isoOffsetY = 5f; // ������ �� ��� Y
    public float isoOffsetZ = -10f; // �������� �� ��� Z

    void LateUpdate()
    {
        // ���� �������� ������� ���, ����������� ������
        if (target == null) return;

        // ������������ ������� ������
        Vector3 desiredPosition = target.position + new Vector3(isoOffsetX, isoOffsetY, isoOffsetZ);

        // �������� ���������� ������ � ������ ���������
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // ������� ������ �� ���������
        transform.LookAt(target.position);
    }
}
