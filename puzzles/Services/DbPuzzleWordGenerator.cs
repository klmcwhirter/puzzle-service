#pragma warning disable CS1572, CS1573, CS1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using puzzles.Models;
using puzzles.Repositories;
using puzzles.Utils;

namespace puzzles.Services
{
    [DataContract]
    public class DbPuzzleWordGenerator : IPuzzleKind
    {
        [DataMember]
        public string Key => StaticKey;

        [DataMember]
        public string Name => Puzzle?.Name ?? "Saved Puzzle";

        [DataMember]
        public PuzzleKindFeatures Features => DbPuzzleFeatures;

        [DataMember]
        public string Description => Puzzle?.Description ?? "Pick from a set of saved puzzles";
        
        private ILogger<DbPuzzleWordGenerator> Logger { get; }

        static readonly PuzzleKindFeatures DbPuzzleFeatures = new PuzzleKindFeatures
        {
            HasTopics = true,
            HasTags = true,
            HasSavedPuzzles = true
        };

        public static readonly string StaticKey = "puzzle";

        public Puzzle Puzzle { get; set; }

        public IPuzzlesRepository Repository { get; }

        public DbPuzzleWordGenerator(IPuzzlesRepository repository, ILogger<DbPuzzleWordGenerator> logger)
        {
            Repository = repository;
            Logger = logger;
        }

        protected int CurrentIdx { get; set; } = 0;
        protected ISet<string> Seen { get; set; }

        public PuzzleWord Generate(params object[] options)
        {
            Puzzle = (Puzzle)options[0];

            PuzzleWord rc;
            string word;
            do
            {
                // If the word list is exhausted, start over
                if (Seen == null || Seen.Count >= Puzzle.Words.Length)
                {
                    ResetSeen();
                }

                var idx = Puzzle.Words.Length.Random();
                rc = Puzzle.PuzzleWords.Skip(idx).FirstOrDefault();
                word = rc.Word;
            } while (Seen.Contains(word));

            Seen.Add(word);

            return rc;
        }

        protected void ResetSeen()
        {
            Seen = new SortedSet<string>(Comparer<string>.Create(
                        (e1, e2) => e1.ToUpperInvariant().CompareTo(e2.ToUpperInvariant())
                    ));
        }
    }
}
