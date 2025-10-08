using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera2 : MonoBehaviour
{
    public Transform target; // Персонаж, за которым будет следовать камера
    public float followSpeed = 5f; // Скорость, с которой камера будет следовать за персонажем
    public float offsetY = 3f; // Подъем по оси Y
    public float offsetZ = -5f; // Смещение по оси Z
    public float smoothness = 0.1f; // Степень сглаживания
    public float collisionCheckDistance = 1f; // Расстояние для проверки столкновений
    public LayerMask obstacleLayer; // Слой, на котором находятся препятствия

    /*void LateUpdate()
    {
        // Если целевого объекта нет, заканчиваем работу
        if (target == null) return;

        // Рассчитываем позицию камеры
        //Vector3 targetDir = target.TransformDirection(Vector3.back); // Направление, противоположное движению персонажа
        Vector3 targetDir = target.TransformDirection(Vector3.forward); // Направление движения персонажа
        Vector3 desiredPosition = target.position + targetDir * offsetZ + Vector3.up * offsetY;

        // Медленно перемещаем камеру к новому положению
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothness);

        // Наводим камеру на персонажа
        transform.LookAt(target.position);
    }*/

    private Vector3 velocity = Vector3.zero; // Для SmoothDamp
    void LateUpdate()
    {
        // Если целевого объекта нет, заканчиваем работу
        if (target == null) return;

        // Рассчитываем позицию камеры
        Vector3 targetDir = target.TransformDirection(Vector3.forward); // Направление, противоположное движению персонажа
        Vector3 desiredPosition = target.position + targetDir * offsetZ + Vector3.up * offsetY;

        // Проверка на столкновение
        RaycastHit hit;
        if (Physics.SphereCast(target.position, 0.5f, targetDir, out hit, collisionCheckDistance, obstacleLayer))
        {
            // Если препятствие найдено, корректируем позицию камеры
            desiredPosition = hit.point + hit.normal * 0.5f;
        }

        // Медленно перемещаем камеру к новому положению
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Наводим камеру на персонажа
        transform.LookAt(target.position);
    }
}
