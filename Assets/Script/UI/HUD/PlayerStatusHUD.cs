using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : HUDBase
{
    // Component
    [SerializeField] GameObject[] hpObj;
    
    // Data
    const int MAX_HP_COUNT = 3;
}
