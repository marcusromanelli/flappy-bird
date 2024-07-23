using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsScoreStorage : MonoBehaviour, IScoreStorage
{
    private const string storeKey = "HighestScore";

    public List<int> Load()
    {
        var load = PlayerPrefs.GetString(storeKey);

        try
        {
            return JsonConvert.DeserializeObject<List<int>>(load);
        }catch(Exception e)
        {
            Debug.LogError(e.Message);
        }

        return null;
    }

    public void Save(List<int> scores)
    {
        var json = JsonConvert.SerializeObject(scores);

        PlayerPrefs.SetString(storeKey, json);
    }
}