using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventProxy {}
public interface IEventListenerProxy : IEventProxy { }
public interface IEventRegisterProxy : IEventProxy { }