using System.Collections;
using UnityEngine;

class AnimateMoney:MonoBehaviour
{
    public void Start()
    {
       StartCoroutine(PlayElement());
    }

    public IEnumerator PlayElement()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}

