using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera2 : MonoBehaviour
{
    public Transform target; // ��������, �� ������� ����� ��������� ������
    public float followSpeed = 5f; // ��������, � ������� ������ ����� ��������� �� ����������
    public float offsetY = 3f; // ������ �� ��� Y
    public float offsetZ = -5f; // �������� �� ��� Z
    public float smoothness = 0.1f; // ������� �����������
    public float collisionCheckDistance = 1f; // ���������� ��� �������� ������������
    public LayerMask obstacleLayer; // ����, �� ������� ��������� �����������

    /*void LateUpdate()
    {
        // ���� �������� ������� ���, ����������� ������
        if (target == null) return;

        // ������������ ������� ������
        //Vector3 targetDir = target.TransformDirection(Vector3.back); // �����������, ��������������� �������� ���������
        Vector3 targetDir = target.TransformDirection(Vector3.forward); // ����������� �������� ���������
        Vector3 desiredPosition = target.position + targetDir * offsetZ + Vector3.up * offsetY;

        // �������� ���������� ������ � ������ ���������
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothness);

        // ������� ������ �� ���������
        transform.LookAt(target.position);
    }*/

    private Vector3 velocity = Vector3.zero; // ��� SmoothDamp
    void LateUpdate()
    {
        // ���� �������� ������� ���, ����������� ������
        if (target == null) return;

        // ������������ ������� ������
        Vector3 targetDir = target.TransformDirection(Vector3.forward); // �����������, ��������������� �������� ���������
        Vector3 desiredPosition = target.position + targetDir * offsetZ + Vector3.up * offsetY;

        // �������� �� ������������
        RaycastHit hit;
        if (Physics.SphereCast(target.position, 0.5f, targetDir, out hit, collisionCheckDistance, obstacleLayer))
        {
            // ���� ����������� �������, ������������ ������� ������
            desiredPosition = hit.point + hit.normal * 0.5f;
        }

        // �������� ���������� ������ � ������ ���������
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // ������� ������ �� ���������
        transform.LookAt(target.position);
    }
}
