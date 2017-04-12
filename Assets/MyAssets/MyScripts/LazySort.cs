using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazySort : MonoBehaviour
{
    private GameObject tempGameObj;

    public void SortGameObjects(GameObject[] array, int arraySize)
    {
        for (int i = 0; i < arraySize - 1; i++)
        {
            for (int j = i + 1; j < arraySize; j++)
            {
                if (string.Compare(array[i].name, array[j].name) > 0)
                {
                    tempGameObj = array[i];
                    array[i] = array[j];
                    array[j] = tempGameObj;
                }
            }
        }
        return;
    }
}
