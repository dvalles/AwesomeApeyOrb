using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Makes sure pools implement certain methods
 */

public interface IPool<T>
{
    //check if the object is in use
    bool InUse(T obj);

    //set the object up if necessary
    void SetUp(T obj);
}
