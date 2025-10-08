using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Персонаж, за которым будет следовать камера
    public float followSpeed = 5f; // Скорость, с которой камера будет следовать за персонажем
    public float offsetY = 3f; // Подъем по оси Y
    public float offsetZ = -5f; // Смещение по оси Z

    void LateUpdate()
    {
        // Если целевого объекта нет, заканчиваем работу
        if (target == null) return;

        // Рассчитываем позицию камеры
        Vector3 desiredPosition = target.position + new Vector3(0, offsetY, offsetZ);

        // Медленно перемещаем камеру к новому положению
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Наводим камеру на персонажа
        transform.LookAt(target.position);
    }
}
