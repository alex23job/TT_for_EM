using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCameraFollower : MonoBehaviour
{
    public Transform target; // Персонаж, за которым будет следовать камера
    public float followSpeed = 5f; // Скорость, с которой камера будет следовать за персонажем
    public float isoOffsetX = 10f; // Смещение по оси X
    public float isoOffsetY = 5f; // Подъем по оси Y
    public float isoOffsetZ = -10f; // Смещение по оси Z

    void LateUpdate()
    {
        // Если целевого объекта нет, заканчиваем работу
        if (target == null) return;

        // Рассчитываем позицию камеры
        Vector3 desiredPosition = target.position + new Vector3(isoOffsetX, isoOffsetY, isoOffsetZ);

        // Медленно перемещаем камеру к новому положению
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Наводим камеру на персонажа
        transform.LookAt(target.position);
    }
}
