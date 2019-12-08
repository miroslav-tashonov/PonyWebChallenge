using static PonyWebChallenge.Models.CellStateEnumModel;

namespace PonyWebChallenge.Extensions
{
    public static class CellStateExtension
    {
        public static bool HasFlag(this CellState cs, CellState flag)
        {
            return ((int)cs & (int)flag) != 0;
        }
    }
}