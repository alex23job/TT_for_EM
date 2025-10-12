using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera startCamera; // Стартовая камера
    [SerializeField] private Camera isoCamera; // Изометрическая камера
    [SerializeField] private Camera thirdPersonCamera; // Камера от третьего лица
    public KeyCode switchKey = KeyCode.Tab; // Клавиша для переключения

    private bool useIsoCamera = true; // Флаг, указывающий, какая камера активна

    void Start()
    {
        startCamera.enabled = true;
        // Изначально активируем изометрическую камеру
        isoCamera.enabled = false;
        thirdPersonCamera.enabled = false;
    }

    void Update()
    {
        // Обрабатываем нажатие клавиши Tab
        if (Input.GetKeyDown(switchKey))
        {
            useIsoCamera = !useIsoCamera; // Меняем флаг

            // Переключаем активность камер
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
        // Изначально активируем изометрическую камеру
        isoCamera.enabled = true;
        thirdPersonCamera.enabled = false;
    }
}
