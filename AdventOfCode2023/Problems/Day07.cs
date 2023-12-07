namespace AdventOfCode2023.Problems
{
    public class Day07 : IProblem
    {
        public static List<char> AllCards = "AKQJT98765432".ToList();
        public static List<char> AllCardsPart2 = "AKQT98765432J".ToList();
        public void Solve(string[] input)
        {
            var hands = new List<Hand>();
            var hands2 = new List<Hand>();
            
            foreach(var line in input)
            {
                var splitHandAndBid = line.Split(' ');
                var handToAdd = new Hand{Cards = splitHandAndBid[0], Bid = int.Parse(splitHandAndBid[1])};
                var handToAdd2 = new Hand{Cards = splitHandAndBid[0], Bid = int.Parse(splitHandAndBid[1]), IsPart1 = false};

                AllCards.ForEach(c => {
                    if (handToAdd.Cards.Count(h => h == c) == 5) handToAdd.TypeRank = 1;
                    else if (handToAdd.Cards.Count(h => h == c) == 4) handToAdd.TypeRank = 2;
                    else if (handToAdd.Cards.Count(h => h == c) == 3 && handToAdd.TypeRank == 6) handToAdd.TypeRank = 3;
                    else if (handToAdd.Cards.Count(h => h == c) == 3) handToAdd.TypeRank = 4;
                    else if (handToAdd.Cards.Count(h => h == c) == 2 && handToAdd.TypeRank == 4) handToAdd.TypeRank = 3;
                    else if (handToAdd.Cards.Count(h => h == c) == 2 && handToAdd.TypeRank == 6) handToAdd.TypeRank = 5;
                    else if (handToAdd.Cards.Count(h => h == c) == 2) handToAdd.TypeRank = 6;
                });

                AllCardsPart2.ForEach(c => {
                    if(c != 'J'){
                        if (handToAdd2.Cards.Count(h => h == c) == 5) handToAdd2.TypeRank = 1;
                        else if (handToAdd2.Cards.Count(h => h == c) == 4) handToAdd2.TypeRank = 2;
                        else if (handToAdd2.Cards.Count(h => h == c) == 3 && handToAdd2.TypeRank == 6) handToAdd2.TypeRank = 3;
                        else if (handToAdd2.Cards.Count(h => h == c) == 3) handToAdd2.TypeRank = 4;
                        else if (handToAdd2.Cards.Count(h => h == c) == 2 && handToAdd2.TypeRank == 4) handToAdd2.TypeRank = 3;
                        else if (handToAdd2.Cards.Count(h => h == c) == 2 && handToAdd2.TypeRank == 6) handToAdd2.TypeRank = 5;
                        else if (handToAdd2.Cards.Count(h => h == c) == 2) handToAdd2.TypeRank = 6;
                    }
                });
                
                var jCount = handToAdd2.Cards.Count(h => h == 'J');
                if(jCount == 5) handToAdd2.TypeRank = 1;
                else{
                    for(int i = 0; i < jCount; i++){
                        if(handToAdd2.TypeRank == 4 || handToAdd2.TypeRank == 6 || handToAdd2.TypeRank == 5) handToAdd2.TypeRank-=2;
                        else handToAdd2.TypeRank--;
                    }
                }

                hands.Add(handToAdd);
                hands2.Add(handToAdd2);
            }

            hands.Sort();
            for(int i = 1; i <= hands.Count; i++)
                hands[i-1].Rank = i;

            System.Console.WriteLine($"Part 1: {hands.Sum(h => h.Rank * h.Bid)}");

            hands2.Sort();
            for(int i = 1; i <= hands2.Count; i++)
                hands2[i-1].Rank = i;

            System.Console.WriteLine($"Part 2: {hands2.Sum(h => h.Rank * h.Bid)}");

        }
    }

    public class Hand : IComparable
    {
        public required string Cards { get; set; }
        public int Rank { get; set; }
        public int Bid { get; set; }
        public int TypeRank { get; set; } = 7;

        public bool IsPart1 { get; set; } = true;

        public int CompareTo(object? obj)
        {
            Hand hand = (Hand)obj!;
            if(TypeRank < hand.TypeRank) return 1;
            if(TypeRank > hand.TypeRank) return -1;

            for(int i = 0; i < Cards.Length; i++)
            {
                if(Cards[i] != hand.Cards[i])
                {
                    if(IsPart1){
                        if(Day07.AllCards.IndexOf(Cards[i]) < Day07.AllCards.IndexOf(hand.Cards[i]))
                            return 1;
                        else return -1;
                    }
                    else{
                        if(Day07.AllCardsPart2.IndexOf(Cards[i]) < Day07.AllCardsPart2.IndexOf(hand.Cards[i]))
                            return 1;
                        else return -1;
                    }
                }
            }
            return 0;
        }
    }
}