using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    private readonly static string NICK_NAME_KEY = "UserNickName";

    public static string GetSavedUserNickName()
    {
        if (!PlayerPrefs.HasKey(NICK_NAME_KEY))
            return string.Empty;
        else
            return PlayerPrefs.GetString(NICK_NAME_KEY);
    }

    public static void SaveUserNickName(string nickName)
    {
        PlayerPrefs.SetString(NICK_NAME_KEY, nickName);
    }
}
