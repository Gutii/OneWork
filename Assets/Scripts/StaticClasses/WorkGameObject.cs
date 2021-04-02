using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class WorkGameObject:MonoBehaviour
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


    public static GameObject DocumentTransform(List<GameObject> Documents, Vector3 position, bool flipX)
    {
        int countDocument = Documents.Count-1;
        Transform transform = Documents[countDocument].transform;
        if (countDocument > 3)
        {
            transform.localScale = new Vector3(0.93f, 1.58f, 1);
            if (countDocument == 4)
                if (flipX)
                    transform.localPosition = new Vector3(position.x - 0.22f, position.y, 0);
                else
                    transform.localPosition = new Vector3(position.x + 0.22f, position.y, 0);
            else
                if (flipX)
                transform.localPosition = countDocument >= 1 ?
                    new Vector3(position.x - 0.22f, 0.05f + Documents[Documents.Count - 2].transform.localPosition.y, 0) : new Vector3(position.x - 0.22f, position.y, 0);
            else
                transform.localPosition = Documents.Count >= 1 ?
                     new Vector3(position.x + 0.22f, 0.05f + Documents[Documents.Count - 2].transform.localPosition.y, 0) : new Vector3(position.x + 0.22f, position.y, 0);

        }
        else
        {
            transform.localScale = new Vector3(1.29f, 2.18f, 1);
            transform.localPosition = countDocument >= 1 ?
            new Vector3(position.x, 0.07f + Documents[Documents.Count - 2].transform.localPosition.y, -1) : position;
        }

        return Documents[countDocument];
    }

}
