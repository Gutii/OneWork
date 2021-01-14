using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkGameObject:MonoBehaviour
{
    public static GameObject CreateObject(GameObject[] RandomGameObjects, Vector3 localposition, Transform transformParent, float xscale = 100f,float yscale = 100f,float zscale = 1f)
    {
        GameObject @object = new GameObject();
        Vector3 scale = new Vector3(xscale, yscale, zscale);
        int random = Random.Range(0, RandomGameObjects.Length);

        @object = Instantiate(RandomGameObjects[random]);
        Transform transformT = @object.transform;
        transformT.SetParent(transformParent);
        transformT.localScale = scale;
        transformT.localPosition = localposition;

        return @object;
    }

   
}
