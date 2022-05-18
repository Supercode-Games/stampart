using System.Collections;
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
            
         
        if(setCurrent)
        {
            if ((currentLevel % 3 == 0) && currentLevel!=0)
            {
                currentTheme++;
            }

            currentTheme = currentTheme % themeObjects.Count;
            PlayerPrefs.SetInt("current_theme", currentTheme);
        }

    }


   

}
