using System.IO;
using UnityEngine;

class Load:MonoBehaviour
{
    static public T LoadingJSON <T>()
    {
        if (File.Exists(Company.path))
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(Company.path));
        }
        else
            return default(T);
    }
}


