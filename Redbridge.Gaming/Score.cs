using System;
using System.Collections.Generic;
using System.Linq;

namespace Redbridge.Gaming
{
	public class Score
	{
		public Score(decimal score, PlayerTurn turn)
		{
			Value = score;
			Turn = turn;
		}
		
		public PlayerTurn Turn { get; private set; }
		public decimal Value { get; private set; }
	}
	
}
