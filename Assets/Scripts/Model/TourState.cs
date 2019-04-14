using System;
using System.Collections.Generic;
using UnityEngine;

public class TourState : IEqualityComparer<TourState>
{
    public enum Type
    {
        Image,
        Video,
        Stream
    }

    public Guid id { private set; get; } = Guid.NewGuid();
    public string title { set; get; }
    public string url { set; get; }
    public Type type { set; get; }
    public Vector3 viewDirection { set; get; } = new Vector2(0.0f, 0.0f);
    public Quaternion rotation { set; get; }  = Quaternion.identity;

    public TourStateLink[] links { private set; get; }

    public TourState(string title, string url, Type type)
    {
        this.title = title;
        this.url = url;
        this.type = type;
    }

    public bool Equals(TourState other)
    {
        return this == other || id == other.id;
    }

    public bool Equals(TourState x, TourState y)
    {
        if (x == null && y == null)
            return true;

        if (x == null || y == null)
            return false;

        if (x == y || x.id == y.id)
            return true;

        return false;
    }

    public int GetHashCode(TourState obj)
    {
        return id.GetHashCode();
    }
}
