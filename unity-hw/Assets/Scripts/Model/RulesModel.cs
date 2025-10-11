//There is no actual save / load logit Y E T , but let's pretend that this struct exists for serialization purposes
public struct RulesModel
{
    //All validation is happening in GameState class, so i could just use public fields as well
    //but they are BAD because they are HORRIBLE because they are TERRIBLE
    public int LossCond { get; set; }
    public int WinCond { get; set; }
    public int DiceCount { get; set; }

    public RoundOutcome GetOutcome(int score)
    {
        if (score >= WinCond)
        {
            return RoundOutcome.Win;
        }
        else if (score <= LossCond)
        {
            return RoundOutcome.Loss;
        }
        return RoundOutcome.Draw;
    }
}