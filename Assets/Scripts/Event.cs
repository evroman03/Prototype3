using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="EventData", menuName = "EventData")]
public class Event : ScriptableObject
{
    public string EventName;
    public string Description;
    public int HealthEffect, CannonCtEffect, GoldEffect, CrewEffect, ReputationEffect, GoldPerHealthEffect, GoldPerCrewEffect;
    public int MXHealthEffect, MXCannontCtEffect, MXGoldEffect, MXCrewEffect, MXReputationEffect;

    public enum SendToTile
    {
        None, Rand, Exact, Port, Ocean, Royal, Island
    }
    public SendToTile ToGo;
    public Vector3 ShipExactMoveTo = Vector3.zero;
}
