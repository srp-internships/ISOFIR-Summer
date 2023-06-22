﻿namespace TestNinja.Fundamentals;

public class Stack<T>
{
    private readonly List<T> _list = new();

    public int Count => _list.Count;

    public void Push(T obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        _list.Add(obj);
    }

    public T Pop()
    {
        if (_list.Count == 0)
            throw new InvalidOperationException();

        var result = _list[^1];
        _list.RemoveAt(_list.Count - 1);

        return result;
    }


    public T Peek()
    {
        if (_list.Count == 0)
            throw new InvalidOperationException();

        return _list[^1];
    }
}