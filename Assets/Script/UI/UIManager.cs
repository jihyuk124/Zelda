using UnityEngine;
using System.Collections.Generic;
using System;
using System.Resources;
using UnityEditor.PackageManager.UI;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    private Dictionary<Type, HUDBase> hudDictionary = new Dictionary<Type, HUDBase>();
    private Dictionary<Type, WindowBase> windowDictionary = new Dictionary<Type, WindowBase>();


}