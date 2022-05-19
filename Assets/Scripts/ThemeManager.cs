using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public List<GameObject> themeObjects;
    public bool setCurrent;


    public void initialiseThemeBG()
    {
        foreach (var item in themeObjects)
        {
            item.SetActive(false);
        }

        int currentLevel = PlayerPrefs.GetInt("current_level", 0);
        int currentTheme = PlayerPrefs.GetInt("current_theme", 0);
        themeObjects[currentTheme].SetActive(true);


        if ((currentLevel % 1 == 0) && currentLevel!=0)
        {
            themeObjects[currentTheme].SetActive(false);

            currentTheme++;
            if(currentTheme == themeObjects.Count)
            {
                currentTheme = 0;
            }
            themeObjects[currentTheme].SetActive(true);
            PlayerPrefs.SetInt("current_theme", currentTheme);
        }
    }


   

}
