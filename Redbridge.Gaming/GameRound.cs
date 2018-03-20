using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using Redbridge.Linq;
using Redbridge.SDK;

namespace Redbridge.Gaming
{
	public class PlayerTurn
	{
		public PlayerTurn(Player player)
		{
			if (player == null)
				throw new ArgumentNullException(nameof(player));
			Player = player;
		}

		public Player Player { get; private set; }
		public Score TurnScore { get; set; }
	}

	public class GameRound
	{
		private readonly SortedDictionary<Player, Score> _playerTurns = new SortedDictionary<Player, Score>();
		private readonly BehaviorSubject<PlayerTurn> _turns;

		public GameRound(IEnumerable<Player> players)
		{
			if (players == null)
				throw new ArgumentNullException(nameof(players));

			if (!players.Any())
				throw new NotSupportedException("A round must contain at least one player.");

			players.ForEach(p => _playerTurns.Add(p, null));
			_turns = new BehaviorSubject<PlayerTurn>(new PlayerTurn(players.First()));
		}

		public IObservable<PlayerTurn> Turn
		{
			get { return _turns; }
		}

		public static GameRound FromPlayers(IEnumerable<Player> players)
		{
			if (players == null) throw new ArgumentNullException(nameof(players));
			var round = new GameRound(players);
			return round;
		}

		public decimal TurnTotalScore
		{
			get { return _playerTurns.Values.Sum(v => v.Value); }
		}
		
		public IEnumerable<Score> Scores 
		{
			get { return _playerTurns.Select(pt => pt.Value); }
		}
	}
	
}
