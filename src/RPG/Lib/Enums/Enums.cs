namespace RPG.Lib.Enums
{
    public enum TriggerMethod
    {
        Instant = -1,
        OnEnterLocation = 1,
        OnTalkToNPC = 2,
        OnQuestComplete = 3,
        OnDialogYes = 4,
        OnDialogNo = 5,
        OnEnterQuestState = 6,
        OnQuestStatusEquals = 7
    }

    public enum TriggerAction
    {
        None = 0,
        MoveLocation = 1,
        BeginQuest = 2,
        AdvanceQuestState = 3,
        OpenDialog = 4,
        ExitDialog = 5
    }
}
