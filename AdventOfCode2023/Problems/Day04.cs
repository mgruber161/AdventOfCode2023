namespace AdventOfCode2023.Problems
{
    internal class Day04 : IProblem
    {
        public void Solve(string[] input)
        {
            //Part 1
            var cards = new List<Card>();
            foreach(var line in input)
            {
                var splitCardsAndValues = line.Split(':');
                var cardToAdd = new Card{ Id = int.Parse(new string(splitCardsAndValues[0].Where(Char.IsDigit).ToArray()))};
                var splitWinningAndPicked = splitCardsAndValues[1].Split('|');
                foreach(var winningNumber in splitWinningAndPicked[0].Split(' ').Where(n => !string.IsNullOrEmpty(n)))
                    cardToAdd.WinningNumbers.Add(int.Parse(new string(winningNumber.Where(Char.IsDigit).ToArray())));
                foreach(var pickedNumber in splitWinningAndPicked[1].Split(' ').Where(n => !string.IsNullOrEmpty(n)))
                    cardToAdd.PickedNumbers.Add(int.Parse(new string(pickedNumber.Where(Char.IsDigit).ToArray())));
                foreach(var match in cardToAdd.WinningNumbers.Intersect(cardToAdd.PickedNumbers))
                    cardToAdd.Points = cardToAdd.Points == 0 ? 1 : cardToAdd.Points * 2;
                    
                cards.Add(cardToAdd);
            }

            System.Console.WriteLine($"Part 1: {cards.Sum(c => c.Points)}");

            //Part 2
            var cardsClone = new List<Card>(cards);
            for(int i = 0; i < cards.Count; i++)
                AddCardsRecursive(cardsClone, i);
            
            System.Console.WriteLine($"Part 2: {cardsClone.Count}");
        }

        private void AddCardsRecursive(List<Card> cards, int index)
        {
            var matches = cards[index].WinningNumbers.Intersect(cards[index].PickedNumbers).ToList();
            for(int j = 1; j <= matches.Count; j++)
            {
                cards.Add(cards[index+j]);
                AddCardsRecursive(cards, index+j);
            }
        }

        public class Card
        {
            public int Id { get; set; }
            public List<int> WinningNumbers { get; set; } = new();
            public List<int> PickedNumbers { get; set; } = new();
            public int Points { get; set; }
        }
    }
}