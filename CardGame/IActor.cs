namespace CardGame
{
    internal interface IActor
    {
        void PlayTurn(Player me, Player enemy);
    }
}