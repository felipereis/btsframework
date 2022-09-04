using System.Collections.Generic;
using System.Linq;
using System;

public interface IRepository<T> where T : class
{
    void Insert(T obj);
    void Remove(T obj);
    List<T> List(Func<T, bool> expression = null);
    void ApiRequest(String con);
}


public class Repository<T> : IRepository<T> where T : class
{
    List<T> lt;

    public Repository()
    {
        lt = new List<T>();
    }

    public void Insert(T obj)
    {
        lt.Add(obj);
    }

    public void Remove(T obj)
    {
        lt.Remove(obj);
    }

    public List<T> List(Func<T, bool> expression = null)
    {
        return expression != null ? lt.Where(expression).ToList() : lt;
    }

    public void ApiRequest(String con)
    {
        // Waiting for the new rebase to be ready
    }
}