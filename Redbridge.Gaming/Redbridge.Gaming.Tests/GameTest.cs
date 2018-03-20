using NUnit.Framework;
using System;
namespace Redbridge.Gaming.Tests
{
	[TestFixture()]
	public class GameTest
	{
		[Test()]
		public void CreateGameExpectSuccess()
		{
			var game = new Game();
			Assert.AreEqual(0, game.Players.Count);
		}

		[Test()]
		public void CreateGameAddPlayerExpectOnePlayerSuccess()
		{
			var game = new Game();
			game.Add(new Player("Ben"));
			Assert.AreEqual(1, game.Players.Count);
		}

		[Test()]
		public void CreateGameAddPlayerExpectOnePlayerStartGameExpectFirstTurn()
		{
			var game = new Game();
			game.Add(new Player("Ben"));
			game.Start();
			var round = game.CurrentRound;
			//Assert.AreEqual(1, round.Number);
		}
	}
}
