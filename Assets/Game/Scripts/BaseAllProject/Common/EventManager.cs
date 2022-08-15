using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    #region System of all Project
    public static Action<SoundName, bool> TurnOnAudio;
    #endregion



    public static Action<int> ReceiveScore;
    public static Action<float> ReceiveGrowth;
    public static Action<bool> EndGame;
    public static Action<float> AttackPlayer;
}