using XoDotNet.Features.Abstractions;

namespace XoDotNet.Features.Services;

public class GameFieldCheckerService : IGameFieldCheckerService
{
    public int CheckFieldForWinners(char[] field)
    {
        const int n = 3;
        for (var i = 0; i < n; i++)
        {
            var row = field.Skip(i * n).Take(n).ToArray();
            var x = i;
            var col = Enumerable.Range(0, n).Select(j => field[j * n + x]).ToArray();
            var diag1 = Enumerable.Range(0, n).Select(j => field[j * n + j]).ToArray();
            var diag2 = Enumerable.Range(0, n).Select(j => field[j * n + n - 1 - j]).ToArray();
            if (row.All(cell => cell == 'x') ||
                col.All(cell => cell == 'x') ||
                diag1.All(cell => cell == 'x') ||
                diag2.All(cell => cell == 'x'))
                return 1;
            if (row.All(cell => cell == 'o') ||
                col.All(cell => cell == 'o') ||
                diag1.All(cell => cell == 'o') ||
                diag2.All(cell => cell == 'o'))
                return 2;
        }

        if (field.Contains('-'))
            return -1;
        return 0;
    }
}