using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using static UnityEditor.Progress;

public static class JsonController
{
    public static CardDatabaseStructure.Root getCardJsonWithPath(string path)
    {
        string jsonString = new StreamReader(Application.streamingAssetsPath + path).ReadToEnd();

        // use below syntax to access JSON file
        CardDatabaseStructure.Root jsonFile = JsonConvert.DeserializeObject<CardDatabaseStructure.Root>(jsonString);
        return jsonFile;
        
    }

    public static void createCardJsonTempWithPath(string path, List<CardDatabaseStructure.ICardInfoInterface> cards)
    {
        StreamWriter sw = new StreamWriter(Application.streamingAssetsPath + path);
        sw.Write(JsonConvert.SerializeObject(cards));
        sw.Flush();
        sw.Close();
    }

    public static List<CardDatabaseStructure.ICardInfoInterface> readCardJsonTempWithPath(string path)
    {
        StreamReader sw = new StreamReader(Application.streamingAssetsPath + path);
        List<CardDatabaseStructure.ICardInfoInterface> cardInfos = new List<CardDatabaseStructure.ICardInfoInterface>();
        String cardJson_ = sw.ReadToEnd();
        sw.Close();
        Debug.Log(cardJson_);
        if (cardJson_.Length == 0)
        {
            return cardInfos;
        }
        cardInfos = JsonConvert.DeserializeObject<List<CardDatabaseStructure.ICardInfoInterface>>(cardJson_);

        return cardInfos;
    }

    public static EnemyDatabaseStructure.Root getEnemyJsonWithPath(string path)
    {
        string jsonString = new StreamReader(Application.streamingAssetsPath + path).ReadToEnd();

        // use below syntax to access JSON file
        EnemyDatabaseStructure.Root jsonFile = JsonConvert.DeserializeObject<EnemyDatabaseStructure.Root>(jsonString);
        return jsonFile;

    }

    public static BuffDebuffDatabaseStructure.Root getBuffDebuffJsonWithPath(string path)
    {
        string jsonString = new StreamReader(Application.streamingAssetsPath + path).ReadToEnd();

        // use below syntax to access JSON file
        BuffDebuffDatabaseStructure.Root jsonFile = JsonConvert.DeserializeObject<BuffDebuffDatabaseStructure.Root>(jsonString);
        return jsonFile;

    }

    static bool IsTextFileEmpty(string fileName)
    {
        var info = new FileInfo(fileName);
        if (info.Length == 0)
            return true;

        // only if your use case can involve files with 1 or a few bytes of content.
        if (info.Length < 6)
        {
            var content = File.ReadAllText(fileName);
            return content.Length == 0;
        }
        return false;
    }
}
