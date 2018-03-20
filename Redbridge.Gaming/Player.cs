using System;
using System.Collections.Generic;
using System.Linq;

namespace Redbridge.Gaming
{
	

	public class Player
	{
		private List<Score> _score = new List<Score>();

		public Player()
		{
			Name = "Anonymous";
		}

		public Player(string name)
		{
			Name = name;
		}

		public void AddScore(decimal score, PlayerTurn turn)
		{
			_score.Add(new Score(score, turn));
		}

		public override string ToString()
		{
			return string.Format("[Player: Name={0}, TotalScore={1}]", Name, TotalScore);
		}

		public string Name
		{
			get;
			set;
		}

		public void Clear()
		{
			_score.Clear();
		}

		public decimal TotalScore
		{
			get { return _score.Sum(s => s.Value); }
		}
	}
}
