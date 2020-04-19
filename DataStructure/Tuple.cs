using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuple<T>
{
    public T Value;
}

public class Tuple<T,U>
{
    public T First;
    public U Second;

    public Tuple(T t, U u)
    {
        First = t;
        Second = u;
    }
}

public class Tuple<T, U, V>
{
    public T First;
    public U Second;
    public V Third;

    public Tuple(T t,U u,V v)
    {
        First = t;
        Second = u;
        Third = v;
    }
}
