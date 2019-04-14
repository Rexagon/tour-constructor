using System;
using System.Linq;
using System.Collections.Generic;

public class Tour
{
    public Guid id { private set; get; } = Guid.NewGuid();

    public string name { set; get; }
    public string description { set; get; }
    public string coverImageUrl { set; get; }

    public TourState firstState { private set; get; }

    public HashSet<TourState> states { private set; get; }

    public Tour(string name)
    {
        this.name = name;
    }

    public void SetFirstState(TourState state)
    {
        if (states == null)
            return;

        if (Contains(state))
            firstState = state;
    }

    public void AddState(TourState state)
    {
        if (Contains(state))
            return;

        states.Add(state);
    }

    public void RemoveState(TourState state)
    {
        if (states.Count <= 1)
            return;

        states.Remove(state);

        if (firstState == state)
            firstState = states.First();
    }

    public bool Contains(TourState state)
    {
        return states != null && states.Contains(state);
    }
}
