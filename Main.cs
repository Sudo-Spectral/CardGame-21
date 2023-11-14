namespace CardGame
{
    public class Card
    {
        public string Suit { get; }
        public string Rank { get; }
        public int Value { get; }

        public Card(string suit, string rank, int value)
        {
            Suit = suit;
            Rank = rank;
            Value = value;
        }
    }

    public class Deck
    {
        private List<Card> cards = new List<Card>();
        private Random random = new Random();

        public Deck()
        {
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    int value = int.TryParse(rank, out int numericValue) ? numericValue : (rank == "Ace" ? 11 : 10);
                    cards.Add(new Card(suit, rank, value));
                }
            }
        }

        public Card DealCard()
        {
            int index = random.Next(cards.Count);
            Card card = cards[index];
            cards.RemoveAt(index);
            return card;
        }
    }

    public class Hand
    {
        private List<Card> cards = new List<Card>();

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public int CalculateHandValue()
        {
            int value = cards.Sum(card => card.Value);
            int numAces = cards.Count(card => card.Rank == "Ace");

            while (value > 21 && numAces > 0)
            {
                value -= 10;
                numAces--;
            }

            return value;
        }

        public void DisplayHand()
        {
            foreach (var card in cards)
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            Hand playerHand = new Hand();
            Hand dealerHand = new Hand();

            playerHand.AddCard(deck.DealCard());
            playerHand.AddCard(deck.DealCard());

            dealerHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());

            Console.WriteLine("Your hand:");
            playerHand.DisplayHand();
            Console.WriteLine($"Total value: {playerHand.CalculateHandValue()}");
            if (playerHand.CalculateHandValue() < 21)
            {
                Console.WriteLine("Do you want to draw another card? Y/N");
                string answer = Console.ReadLine();
                while (answer.ToLower() != "n")
                {
                    if (answer.ToLower() == "y")
                    {
                        playerHand.AddCard(deck.DealCard());
                        Console.WriteLine("Your hand:");
                        playerHand.DisplayHand();
                        Console.WriteLine($"Total value: {playerHand.CalculateHandValue()}");
                    }
                    Console.WriteLine("\nDo you want to draw another card? Y/N");
                    answer = Console.ReadLine();
                }
            }
           
            while(dealerHand.CalculateHandValue() <= 18)
            {
                dealerHand.AddCard(deck.DealCard());
            }

            Console.WriteLine("\nDealer's hand:");
            dealerHand.DisplayHand();
            Console.WriteLine($"Total value: {dealerHand.CalculateHandValue()}");

            if(dealerHand.CalculateHandValue() > playerHand.CalculateHandValue() && dealerHand.CalculateHandValue() <= 21)
            {
                Console.WriteLine("THE DEALER WINS!");
            }else if(playerHand.CalculateHandValue() > dealerHand.CalculateHandValue() && playerHand.CalculateHandValue() <= 21)
            {
                Console.WriteLine("THE PLAYER WON");
            }
            else
            {
                Console.WriteLine("YOU ARE BOTH LOSERS XD");
            }
        }
    }
}
