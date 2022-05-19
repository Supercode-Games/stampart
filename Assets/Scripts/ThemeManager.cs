using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public List<GameObject> themeObjects;

    public void initialiseThemeBG()
    {
        int currentLevel = PlayerPrefs.GetInt("current_level", 0);

        if ((currentLevel % 3 == 0) && currentLevel != 0 && !Preferences.ThemeSelected)
        {
            Preferences.ThemeSelected = true;
            Preferences.ThemeValue++;

            if(Preferences.ThemeValue == themeObjects.Count)
            {
                Preferences.ThemeValue = 0;
            }
        }
        else if((currentLevel % 3 != 0) && Preferences.ThemeSelected)
        {
            Preferences.ThemeSelected = false;
        }
        setTheme();

    }
    public void setTheme()
    {
        foreach (var item in themeObjects)
        {
            item.SetActive(false);
        }

        themeObjects[Preferences.ThemeValue].SetActive(true);
    }
}
