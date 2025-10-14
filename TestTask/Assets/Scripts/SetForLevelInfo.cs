using System;
using System.Text;
//using UnityEditor.PackageManager;
using UnityEngine;

public class SetForLevelInfo : MonoBehaviour
{
    private SetsInfo m_SetInfo;
    private SetTailsInfo setTails;
    private bool isError = false;

    public bool IsError { get => m_SetInfo.IsError; }
    public int ID { get => m_SetInfo.ID; }
    public int AngleY { get => m_SetInfo.AngleY; }
    public int PosX { get => m_SetInfo.PosX; }
    public int PosY { get => m_SetInfo.PosY; }

    private void Awake()
    {
        setTails = gameObject.GetComponent<SetTailsInfo>();
        //setID = setTails.ID;
        m_SetInfo = new SetsInfo();
    }


    public int[] GetTailsTable()
    {
        if (isError || AngleY == 0)
        {
            return setTails.Tails;
        }
        int i, j;
        int[] res = new int[16];
        if (AngleY == 90)
        {
            for (i = 0; i < 16; i++)
            {
                j = 4 * (3 - i % 4) + (i / 4);
                res[i] = setTails.Tails[j];
            }
        }
        if (AngleY == 180)
        {
            for (i = 0; i < 16; i++)
            {
                res[i] = setTails.Tails[15 - i];
            }
        }
        if (AngleY == 270)
        {
            for (i = 0; i < 16; i++)
            {
                j = 4 * (i % 4) + (3 - i / 4);

                res[i] = setTails.Tails[j];
            }
        }
        return res;
    }

    public void SetParams(string csv, string sep = "=")
    {
        if (string.IsNullOrEmpty(csv)) { isError = true; return; }

        m_SetInfo = new SetsInfo(csv, sep);      
    }

    public void SetParams(SetsInfo info)
    {
        m_SetInfo = new SetsInfo(info.ID, info.AngleY, info.PosX, info.PosY);
    }

    public void SetPositionAndRotation(float startX, float ofsX, float startZ, float ofsZ, float y)
    {
        transform.position = new Vector3(startX + ofsX * PosX, y, startZ - ofsZ * PosY);
        transform.rotation = Quaternion.Euler(0, AngleY, 0);
    }
}

[Serializable]
public class SetsInfo
{
    private bool isError = false;
    private int setID;
    private int angleY;
    private int posX, posY;
    public bool IsError { get => isError; }
    public int ID { get => setID; }
    public int AngleY { get => angleY; }
    public int PosX { get => posX; }
    public int PosY { get => posY; }

    public SetsInfo() { }
    public SetsInfo(int id, int angle, int x, int y)
    {
        setID = id;
        angleY = angle;
        posX = x;
        posY = y;
    }

    public SetsInfo(string csv, string sep = "=")
    {
        string[] parts = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 4)
        {
            if (int.TryParse(parts[0], out setID) == false) { setID = -1; isError = true; }
            if (int.TryParse(parts[1], out angleY) == false) { angleY = 0; isError = true; }
            if (int.TryParse(parts[2], out posX) == false) { posX = -1; isError = true; }
            if (int.TryParse(parts[3], out posY) == false) { posY = -1; isError = true; }
        }
    }

    public string ToCsvString(string sep = "=")
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{setID}{sep}");
        sb.Append($"{angleY}{sep}");
        sb.Append($"{posX}{sep}");
        sb.Append($"{posY}{sep}");
        return sb.ToString();
    }

    public override string ToString()
    {
        return $"Set id={setID} angle={angleY} pos=({posX}, {posY})";
    }
}
