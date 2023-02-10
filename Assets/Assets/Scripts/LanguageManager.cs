using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LanguageManager
{
    public static CardDatabaseStructure.Root getCardDatabaseWithLanguage(bool is_upgraded)
    {
        string languageExtension = "";
        switch (GameManager.Instance.gameLanguage)
        {
            case Language.tr:
                languageExtension = "tr";
                break;
            case Language.en:
                languageExtension = "en";
                break;
            default:
                languageExtension = "en";
                break;
        }
        String url = "";
        if (is_upgraded)
        {
            url = Constants.URLConstants.upgradedCardDatabaseJsonBaseUrl;
        }
        else
        {
            url = Constants.URLConstants.cardDatabaseJsonBaseUrl;
        }
        string cardDtabaseUrl = String.Format(url, languageExtension);

        return JsonController.getCardJsonWithPath(cardDtabaseUrl);
    }
}
// JsonController.getJsonWithPath(@"/Assets/Database/CardDatabase.json")
