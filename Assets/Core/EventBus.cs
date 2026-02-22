using System;
using System.Collections.Generic;

public class EventBus
{
  private readonly Dictionary<Type, object> _subscribers = new();

  public void SubScribe<T>(Action<T> action) where T: struct
  {
    var type = typeof(T);
    if (!_subscribers.ContainsKey(type))
      _subscribers[type] = null;

    _subscribers[type] = (Action<T>)_subscribers[type] + action;
  }

  public void Unsubscribe<T>(Action<T> action) where T: struct
  {
    var type = typeof(T);
    if (_subscribers.ContainsKey(type))
      _subscribers[type] = (Action<T>)_subscribers[type] - action;
  }

  public void Raise<T>(T eventData) where T: struct
  {
    var type = typeof(T);
    if (_subscribers.TryGetValue(type, out var action))
      (action as Action<T>)?.Invoke(eventData);
  }
}