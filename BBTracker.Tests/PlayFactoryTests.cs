using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using BBTracker.Models;

namespace BBTracker.Tests
{
    [TestFixture]
    class PlayFactoryTests
    {
        private PlayFactory _playFactory;
        private Game _game;
        private Player _playerA;
        private Player _playerB;
        private Player _playerC;

        [SetUp]
        public void Setup()
        {
            _game = new Game();
            _playFactory = new PlayFactory(_game);
            _playerA = new Player { FirstName = "PlayerA" };
            _playerB = new Player { FirstName = "PlayerB" };
            _playerC = new Player { FirstName = "PlayerC" };
        }
        [Test]
        public void CreateNewPlay_Creates_Proper_Play()
        {
            _playFactory.CreateNewPlay(_playerA,true);
            Assert.Multiple(() =>
            {
                Assert.That(_playFactory)
            });
        }
    }
}
