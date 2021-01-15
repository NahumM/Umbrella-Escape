public interface IGameManagerObserver
{
    public enum ChooseEvent { coinCollection, death, gamePause, gameRestart, changeScene, gameContinue, };
    void Notify(ChooseEvent option);
}
