namespace XoDotNet.Features.Abstractions;

public interface IGameFieldCheckerService
{
    /// <summary>
    ///     Checks field for winners.
    /// </summary>
    /// <returns>1 if X wins, 2 if O wins, 0 if no one wins, -1 if the game is not ended yet.</returns>
    int CheckFieldForWinners(char[] field);
}