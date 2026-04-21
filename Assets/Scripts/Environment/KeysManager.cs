using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class KeysManager : MonoBehaviour
{
    private List<int> keys = new();

    public void AddKey(int key)
    {
        keys.Add(key);
    }

    public bool HasKey(int key)
    {
        foreach (int i in keys)
        {
            if (i == key)
                return true;
        }
        return false;
    }
}
