#pragma warning disable CS1572, CS1573, CS1591
namespace puzzles.Models
{
    public interface IPuzzleProvider
    {
        Puzzle Puzzle { get; }
    }
}
