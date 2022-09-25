using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    public void Subcsribe(IObserver observer);
    public void Uncubscribe(IObserver observer);
    public void Notify();
}
