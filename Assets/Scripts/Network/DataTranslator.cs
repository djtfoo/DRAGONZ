using UnityEngine;
using System;

public class DataTranslator : MonoBehaviour {

    private const string KILL_SYMBOL = "[KILLS]";
    private const string DEATH_SYMBOL = "[DEATHS]";

    private static string DataToValue(string data, string symbol)
    {
        string[] values = data.Split('/');
        foreach (string value in values)
        {
            if (value.StartsWith(symbol))
            {
                return value.Substring(symbol.Length);
            }
        }

        Debug.LogError(symbol + " not found in " + data);
        return "Error";
    }

	public static int DataToKills (string data)
    {
        return int.Parse(DataToValue(data, KILL_SYMBOL));
    }

    public static int DataToDeaths(string data)
    {
        return int.Parse(DataToValue(data, DEATH_SYMBOL));
    }
}
