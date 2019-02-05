using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharMeta
{
  //The name of the character (for reference in the glossary)
  public string name;
  //The lvl of the character
  public int lvl;
  //Any additional meta we might want to add we can store here
  public string meta;
}
