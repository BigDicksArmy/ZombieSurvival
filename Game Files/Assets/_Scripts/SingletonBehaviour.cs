using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for implementing a singleton, just derive the class you want to be a singleton from this class
//iz not workin :(
public class SingletonBehaviour : MonoBehaviour
{
    #region Singleton
    private static SingletonBehaviour _instance;

    public static SingletonBehaviour Instance
    {
        get
        { return _instance; }

        private set
        { _instance = value; }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("More than one instance of a singleton detected");
            return;
        }
        _instance = this;
    }
    #endregion
}
