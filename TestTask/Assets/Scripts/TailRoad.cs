using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailRoad : MonoBehaviour
{
    /// <summary>
    /// Вычисление номера строки для этой клетки с дорогой
    /// </summary>
    /// <param name="ofsY">смещение по Z нулевой строки</param>
    /// <returns>номер строки</returns>
    public int NumberRow(float ofsY)
    {
        float deltaZ = (ofsY - transform.position.z) / 2;
        return Mathf.RoundToInt(deltaZ);
    }

    /// <summary>
    /// Вычисление номера столбца для этой клетки с дорогой
    /// </summary>
    /// <param name="ofsX">смещение по X нулевого столбца</param>
    /// <returns>номер столбца</returns>
    public int NumberCol(float ofsX)
    {
        float deltaX = (transform.position.x - ofsX) / 2;
        return Mathf.RoundToInt(deltaX);
    }

    public int NumberTail(float ofsX, float ofsY, int countCol)
    {
        return countCol * NumberRow(ofsY) + NumberCol(ofsX);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
