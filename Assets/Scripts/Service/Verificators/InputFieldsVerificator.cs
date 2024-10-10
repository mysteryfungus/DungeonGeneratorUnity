using System;
using UnityEngine;

public class InputFieldsVerificator : MonoBehaviour
{
    [SerializeField] private int maxLevelOfHeros = 10; // потолок для вводимого уровня героев
    [SerializeField] private int minLevelOfHeros = 1;

    [SerializeField] private int maxCountOfRooms = 20; // потолок для количества комнат
    [SerializeField] private int minCountOfRooms = 20;

    [SerializeField] private int minSizeOfRoom = 10; // минимум для вводимого минимального размера комнаты
    [SerializeField] private int maxSizeOfRoom = 30; // потолок для вводимого максимального размера комнаты

    [SerializeField] private int maxCountOfHeros = 10; // потолок для максимального размера группы героев
    [SerializeField] private int minCountOfHeros = 10;

    private void OnValidate()
    {
        if (maxLevelOfHeros < minLevelOfHeros)
        {
            maxLevelOfHeros = minLevelOfHeros + 1;
        }
        if (maxCountOfRooms < minCountOfRooms)
        {
            maxCountOfRooms = minCountOfRooms + 1;
        }
        if (maxSizeOfRoom < minSizeOfRoom)
        {
            maxSizeOfRoom = minSizeOfRoom + 1;
        }
        if (maxCountOfHeros < minCountOfHeros)
        {
            maxCountOfHeros = minCountOfHeros + 1;
        }
    }

    public bool ValidateLevelOfHeros(string level, out int param)
    {
        if (ValidateTypeOfParam(tag, out param))
        {
            if (param >= minLevelOfHeros && param <= maxLevelOfHeros)
            {
                return true;
            }
            else return false;
        }
        else 
        {
            return false;
        }
    }

    public bool ValidateCountOfHeros(string count, out int param)
    {
        if (ValidateTypeOfParam(tag, out param))
        {
            if (param >= minCountOfHeros && param <= maxCountOfHeros)
            {
                return true;
            }
            else return false;
        }
        else
        {
            return false;
        }
    }

    public bool ValidateMaxSizeOfRooms(string size, out int param)
    {
        if (ValidateTypeOfParam(tag, out param))
        {
            if (param > maxSizeOfRoom)
            {
                return true;
            }
            else return false;
        }
        else
        {
            return false;
        }
    }

    public bool ValidateMinSizeOfRooms(string size, out int param)
    {
        if (ValidateTypeOfParam(tag, out param))
        {
            if (param < minSizeOfRoom)
            {
                return true;
            }
            else return false;
        }
        else
        {
            return false;
        }
    }

    public bool ValidateCountOfRooms(string count, out int param)
    {
        if (ValidateTypeOfParam(tag, out param))
        {
            if (param >= minCountOfRooms && param <= maxCountOfRooms)
            {
                return true;
            }
            else return false;
        }
        else
        {
            return false;
        }
    }

    private bool ValidateTypeOfParam(string param, out int newparam)
    {
        try
        {
            newparam = Convert.ToInt32(param);

            return true;
        }
        catch 
        { 
            newparam = 0;
            return false; 
        }
    }
}
