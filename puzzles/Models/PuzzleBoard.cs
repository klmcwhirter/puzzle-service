#pragma warning disable CS1572, CS1573, CS1591
using System.Runtime.Serialization;

namespace puzzles.Models
{
    [DataContract]
    public class PuzzleBoard
    {
        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public string[,] Letters { get; set; }

        [DataMember]
        public WordSolution[] Solutions { get; set; }

        [DataMember]
        public Puzzle Puzzle { get; set; }
    }
}
