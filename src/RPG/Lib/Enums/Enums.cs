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
        OnExitQuestState = 6,
        OnQuestStatusEquals = 7,
        OnBeginQuest = 8,
        OnQuestFailed = 9,
        OnEnterQuestState = 10
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

    public enum TriggererType
    {
        None = 0,
        Quest = 1,
        QuestStatus = 2,
        Location = 3
    }

    public enum QuestResult
    {
        InProgress = 0,
        Success = 1,
        Fail = 2
    }
}
