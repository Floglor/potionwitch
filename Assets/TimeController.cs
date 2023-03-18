using System.Collections.Generic;
using Misc;
using UnityEngine;

public class TimeController : MonoBehaviour, ISubject
{
    private List<IObserver> _observers = new List<IObserver>();
    public int date = 0;
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            date++;
            Notify();
        }
    }
    
    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (IObserver observer in _observers)
        {
            observer.UpdateObserver(this);
        }
    }
}
