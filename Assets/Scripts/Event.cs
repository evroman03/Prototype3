using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="EventData", menuName = "EventData")]
public class Event : ScriptableObject
{
    public string EventName;
    public string Description;
    public bool AffectShipPosRand = false;
    public bool AffectShipPosExact = false;
    public Vector3 ShipExactMoveTo = Vector3.zero;
    public int HealthEffect, CannonCtEffect, GoldEffect, CrewEffect, ReputationEffect, GoldPerHealthEffect, GoldPerCrewEffect;
    public int MXHealthEffect, MXCannontCtEffect, MXGoldEffect, MXCrewEffect, MXReputationEffect;
}
