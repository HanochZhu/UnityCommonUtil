/**
 * regiest event keys
 */

public enum EventKeys
{
    StartSensorsDetect,
    GetSensorDetectResult,
    ControlPanelMove,

    // detector(sensor)
    SetRandomErrorRange,
    BroadcastPosition,
    DetectComplete,


    // UI
    AskForRepeatPosition,
    ChangeDetectedObjectRotX,
    ChangeDetectedObjectRotY,
    ChangeSlidDistance,

    Global,
}

public enum FreeEventKey
{
    RecordPLCSignal,
    Other
}
