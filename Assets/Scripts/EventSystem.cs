using System;
using System.Collections.Generic;

public static class EventSystem {
  private static Dictionary<string, EventHandler> eventHandlers
    = new Dictionary<string, EventHandler>();

  public static void Subscribe(string name, EventHandler callback) {
    lock (eventHandlers) {
      if (eventHandlers.ContainsKey(name))
        eventHandlers[name] += callback;
      else
        eventHandlers.Add(name, callback);
    }
  }

  public static void Unsubscribe(string name, EventHandler callback) {
    lock (eventHandlers) {
      if (eventHandlers.ContainsKey(name)) {
        eventHandlers[name] -= callback;
        var eventHandler = eventHandlers[name];
        if (eventHandler == null
          || eventHandler.GetInvocationList().Length == 0) {
          eventHandlers.Remove(name);
        }
      }
    }
  }

  public static void Publish(object sender, string name) {
    lock (eventHandlers) {
      if (eventHandlers.ContainsKey(name))
        eventHandlers[name].Invoke(sender, null);
    }
  }
}
