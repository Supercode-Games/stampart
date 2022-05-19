using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences : MonoBehaviour
{
    public static bool ThemeSelected
    {
        get
        {
            return bool.Parse(PlayerPrefs.GetString("ThemeSelected", bool.FalseString));
        }
        set
        {
            PlayerPrefs.SetString("ThemeSelected", value.ToString());
        }
    }

    public static int ThemeValue
    {
        get
        {
            return PlayerPrefs.GetInt("ThemeValue", 0);
        }
        set
        {
            PlayerPrefs.SetInt("ThemeValue", value);
        }
    }
}
