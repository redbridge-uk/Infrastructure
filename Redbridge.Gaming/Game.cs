using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using Redbridge.SDK;

namespace Redbridge.Gaming
{
	

	public class Game
	{
		private DateTime? _started;
		private List<Player> _players = new List<Player>();
		private List<GameRound> _round = new List<GameRound>();
		private BehaviorSubject<Player> _currentPlayer = new BehaviorSubject<Player>(null);
		private BehaviorSubject<GameRound> _currentRound = new BehaviorSubject<GameRound>(null);

		public Game()
		{
		}

		public ICollection<Player> Players
		{ 
			get { return _players; }
		}

		public static Game StartNew()
		{
			var game = new Game();
			game.Start();
			return game;
		}

		public void Add (Player player)
		{
			if (player == null) throw new ArgumentNullException(nameof(player));
			_players.Add(player);
		}

		public GameRound NextRound()
		{
			var round = GameRound.FromPlayers(_players);
			_currentRound.OnNext(round);
			return round;
		}

		public IObservable<Player> Player
		{
			get { return _currentPlayer; }
		}

		public Player CurrentPlayer
		{
			get { return _currentPlayer.Value; }
		}

		public IObservable<GameRound> Round
		{
			get { return _currentRound; }
		}

		public GameRound CurrentRound
		{
			get { return _currentRound.Value; }
		}

		public TimeSpan? GameTime
		{
			get 
			{
				if (_started.HasValue)
					return DateTime.UtcNow - _started.Value;

				return null;
			}
		}

		public void Start()
		{
			if ( !_players.Any() )
				throw new NotSupportedException("The game should have at least one player.");

			if (!_started.HasValue)
				_started = DateTime.UtcNow;

			var round = GameRound.FromPlayers(_players);
			_round.Add(round);
			_currentRound.OnNext(round);
		}

		public void Stop()
		{ 
		}
	}
}
