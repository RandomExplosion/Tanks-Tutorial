using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] [CreateAssetMenu]
public class PickupType : ScriptableObject
{
    public string m_name;
    public Range m_CountPerGame;
    public GameObject m_PickupPrefab;
    
    [System.Serializable]
    public class Range
    {
        public int max;
        public int min;
    }
}
