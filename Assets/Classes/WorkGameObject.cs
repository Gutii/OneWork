using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkGameObject:MonoBehaviour
{
    public static GameObject CreateObject(GameObject[] RandomGameObjects, Vector3 localposition, Transform transformParent, float xscale = 100f,float yscale = 100f,float zscale = 1f)
    {        
        Vector3 scale = new Vector3(xscale, yscale, zscale);
        int random = Random.Range(0, RandomGameObjects.Length);

        GameObject @object = Instantiate(RandomGameObjects[random]);
        Transform transformT = @object.transform;
        transformT.SetParent(transformParent);
        transformT.localScale = scale;
        transformT.localPosition = localposition;

        return @object;
    }

    public static GameObject CreateObject(GameObject Prefab, Vector3 localposition, Transform transformParent, float xscale = 100f, float yscale = 100f, float zscale = 1f)
    {
        Vector3 scale = new Vector3(xscale, yscale, zscale);
        GameObject @object = Instantiate(Prefab);
        Transform transformT = @object.transform;
        transformT.SetParent(transformParent);
        transformT.localScale = scale;
        transformT.localPosition = localposition;

        return @object;
    }

    public static void CreatePanel(GameObject Prefab)
    {
        Vector3 scale = new Vector3(1, 1, 1);
        GameObject @object = Instantiate(Prefab);
        var transformT = @object.GetComponent<RectTransform>();
        transformT.SetParent(GameObject.Find("Canvas").transform);
        transformT.localScale = new Vector3(1, 1, 1);
        transformT.sizeDelta = new Vector2();
        transformT.localPosition = new Vector3(0, 0, -10);
    }

    public static void DestroyChildren(GameObject gameObject)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            Destroy(gameObject.transform.GetChild(i).gameObject);
    }
}
