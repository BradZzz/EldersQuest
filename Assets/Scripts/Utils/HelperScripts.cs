using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperScripts
{
    //Purple
    public static Color THEME1 = new Color(.78f, .14f, .53f);
    public static Color THEME1sub = new Color(.82f, .29f, .61f);
    
    //Orange
    public static Color THEME2 = new Color(1, .70f, .17f);
    public static Color THEME2sub = new Color(1, .77f, .35f);
    
    //Blue
    public static Color THEME3 = new Color(.17f, .33f, .67f);
    public static Color THEME3sub = new Color(.30f, .43f, .72f);

    public static Color GetColorByFactionBold(Unit.FactionType fact){
        switch(fact){
            case Unit.FactionType.Cthulhu: return THEME1;
            case Unit.FactionType.Egypt: return THEME2;
            case Unit.FactionType.Human: return THEME3;
            default: return Color.white;
        }
    }

    public static Color GetColorByFaction(Unit.FactionType fact){
        switch(fact){
            case Unit.FactionType.Cthulhu: return THEME1sub;
            case Unit.FactionType.Egypt: return THEME2sub;
            case Unit.FactionType.Human: return THEME3sub;
            default: return Color.white;
        }
    }

    private static System.Random rng = new System.Random();  

    public static void Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}
