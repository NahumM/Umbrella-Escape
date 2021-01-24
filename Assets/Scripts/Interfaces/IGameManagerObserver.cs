public interface IGameManagerObserver
{
    public enum ChooseEvent { coinCollection, death, gamePause, gameRestart, changeScene, gameContinue, menuLoaded };
    void Notify(ChooseEvent option);
}
