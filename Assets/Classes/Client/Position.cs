using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public Vector3 positionHuman;
    [HideInInspector] public bool Human { get; set; }

    public Vector3 DocumentPosition()
    {
        int Numbtable = int.Parse(gameObject.name.Replace("(Clone)", "").Replace("Table", ""));
        Vector3 position = new Vector3();
        switch (Numbtable)
        {
            case 1:
            {
                    if (gameObject.GetComponent<SpriteRenderer>().flipX)
                        position = new Vector3(0.55f, 0.43f, -1f);
                    else
                        position = new Vector3(-0.55f, 0.43f, -1f);
                    break;
            }
            case 2:
            {
                    if(gameObject.GetComponent<SpriteRenderer>().flipX)
                    position = new Vector3(0.75f, 0.44f, -1f);
                    else
                        position = new Vector3(-0.70f, 0.44f, -1f);
                    break;
            }
        }       

        return position;
    }
   
}
