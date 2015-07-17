using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

class EventManager:MonoBehaviour
{
    private static EventManager _instant;
    public static EventManager getInstance()
    {
        return _instant;
    }

    void Awake()
    {
        _eventTable = new Hashtable();
        _instant = this;
    }

    private Hashtable _eventTable;
    public void addEventListener(Event_Name name, Action act)
    {
        AddAction(name, act);
    }
    public void addEventListener(Event_Name name, Action<string> act)
    {
        AddAction(name, act);
    }

    private void AddAction(Event_Name name, System.Object act)
    {
        if (_eventTable.ContainsKey(name))
        {
            List<System.Object> list = (List<System.Object>)_eventTable[name];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == act)
                {
                    Debug.Log(act + "已注册");
                    return;
                }
            }
            list.Add(act);
        }
        else
        {
            List<System.Object> list = new List<System.Object>();
            list.Add(act);
            _eventTable.Add(name, list);
        }
    }

    public void trigger(Event_Name name)
    {
        if (_eventTable.ContainsKey(name))
        {
            List<System.Object> list = (List<System.Object>)_eventTable[name];
            for (int i = 0; i < list.Count; i++)
            {
                Action act = (Action)list[i];
                act.Invoke();
            }
        }
    }

    public void trigger(Event_Name name, string param)
    {
        if (_eventTable.ContainsKey(name))
        {
            List<System.Object> list = (List<System.Object>)_eventTable[name];
            for (int i = 0; i < list.Count; i++)
            {
                Action<string> act = (Action<string>)list[i];
                act.Invoke(param);
            }
        }
    }
}
public enum Event_Name {BLOCK_DROP_END};
