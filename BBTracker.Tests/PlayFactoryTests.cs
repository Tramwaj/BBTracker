﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using BBTracker.Models;
using System.Linq;
using System.Security.Policy;
using System.Security.Cryptography.X509Certificates;

namespace BBTracker.Tests
{
    [TestFixture]
    class PlayFactoryTests
    {
        private PlayCreator _playFactory;
        private Game _game;
        private Player _playerA;
        private Player _playerB;
        private Player _playerC;

        [SetUp]
        public void Setup()
        {
            _game = new Game();
            _playFactory = new PlayCreator(_game);
            _playerA = new Player { FirstName = "PlayerA" };
            _playerB = new Player { FirstName = "PlayerB" };
            _playerC = new Player { FirstName = "PlayerC" };
        }
        [Test]
        public void CreateNewPlay_Does_Not_Give_Play_When_Not_Completed()
        {
            _playFactory.NewPlay(_playerA, true);
            Assert.Multiple(() =>
            {
                var _plays = _playFactory.GetPlays();
                Assert.AreEqual(0, _plays.Count);
                //Assert.AreEqual(_playerA, _plays.First().Player);
            });
        }
        [Test]
        public void Properly_constructed_FieldGoal_gives_a_proper_FG()
        {
            _playFactory.NewPlay(_playerA, true);
            _playFactory.ChoosePlayType(PlayType.FieldGoal);
            _playFactory.SelectPoints("3");
            _playFactory.FGResult("Make");
            Assert.Multiple(() =>
            {
                var _plays = _playFactory.GetPlays();
                Assert.AreEqual(1, _plays.Count);
                var _play = _plays.First();
                Assert.AreEqual(_playerA, _play.Player);
                Assert.AreEqual(true, _play.TeamB);
                Assert.AreEqual(3, _play.Points);
            });
        }
        [Test]
        public void FieldGoal_with_Assister()
        {
            _playFactory.NewPlay(_playerA, true);
            _playFactory.ChoosePlayType(PlayType.FieldGoal);
            _playFactory.SelectPoints("2");
            _playFactory.FGResult("Make");
            _playFactory.ConsecutivePlay(_playerB, true);
            Assert.Multiple(() =>
            {
                var _plays = _playFactory.GetPlays();
                Assert.AreEqual(2, _plays.Count);
                Assert.AreEqual(PlayType.FieldGoal, _plays.First().PlayType);
                Assert.AreEqual(_playerA, _plays.First().Player);
                Assert.AreEqual(PlayType.Assist, _plays.Last().PlayType);
                Assert.AreEqual(_playerB, _plays.Last().Player);
                Assert.AreEqual(2, _plays.Last().Points);
            });
        }
        [Test]
        public void FieldGoal_with_Rebounder()
        {
            _playFactory.NewPlay(_playerA, true);
            _playFactory.ChoosePlayType(PlayType.FieldGoal);
            _playFactory.SelectPoints("2");
            _playFactory.FGResult("Miss");
            _playFactory.ConsecutivePlay(_playerB, true);
            Assert.Multiple(() =>
            {
                var _plays = _playFactory.GetPlays();
                Assert.AreEqual(2, _plays.Count);
                Assert.AreEqual(PlayType.FieldGoal, _plays.First().PlayType);
                Assert.AreEqual(_playerA, _plays.First().Player);
                Assert.AreEqual(PlayType.Rebound, _plays.Last().PlayType);
                Assert.AreEqual(_playerB, _plays.Last().Player);
                Assert.AreEqual(2, _plays.First().Points);
                Assert.AreEqual(true, _plays.Last().OffensiveRebound);
            });
        }
        [Test]
        public void FieldGoal_with_Block()
        {
            _playFactory.NewPlay(_playerA, true);
            _playFactory.ChoosePlayType(PlayType.FieldGoal);
            _playFactory.SelectPoints("3");
            _playFactory.FGResult("Block");
            _playFactory.ConsecutivePlay(_playerB, true);
            Assert.Multiple(() =>
            {
                var _plays = _playFactory.GetPlays();
                Assert.AreEqual(2, _plays.Count);
                Assert.AreEqual(PlayType.FieldGoal, _plays.First().PlayType);
                Assert.AreEqual(_playerA, _plays.First().Player);
                Assert.AreEqual(PlayType.Block, _plays.Last().PlayType);
                Assert.AreEqual(_playerB, _plays.Last().Player);
                Assert.AreEqual(3, _plays.First().Points);
            });
        }
        [Test]
        public void SelectPlayer_SelectPlaytype_CheckOut_SelectPlayer_gives_full_substitution()
        {
            _playFactory.NewPlay(_playerA, true);
            _playFactory.ChoosePlayType(PlayType.CheckOut);
            _playFactory.ConsecutivePlay(_playerB, true);            
            Assert.Multiple(() =>
            {
                var _plays = _playFactory.GetPlays();
                Assert.AreEqual(2, _plays.Count);
                Assert.AreEqual(PlayType.CheckOut,_plays.First().PlayType);
                Assert.AreEqual(_playerA, _plays.First().Player);
                Assert.AreEqual(PlayType.CheckIn, _plays.Last().PlayType);
                Assert.AreEqual(_playerB, _plays.Last().Player);    
            });
        }
        [Test]
        public void SelectPlayer_SelectType_Turnover_Without_Causer()
        {
            _playFactory.NewPlay(_playerA, true);
            _playFactory.ChoosePlayType(PlayType.TurnOver);            
            Assert.Multiple(() =>
            {
                var _plays = _playFactory.GetPlays();
                Assert.AreEqual(1, _plays.Count);
                Assert.AreEqual(PlayType.TurnOver, _plays.First().PlayType);
                Assert.AreEqual(_playerA, _plays.First().Player);
            }
            );
        }
        [Test]
        public void SelectPlayer_SelectType_Turnover_SelectPlayer()
        {
            _playFactory.NewPlay(_playerA, true);
            _playFactory.ChoosePlayType(PlayType.TurnOver);
            _playFactory.ConsecutivePlay(_playerB, true);
            Assert.Multiple(() =>
            {
                var _plays = _playFactory.GetPlays();
                Assert.AreEqual(2, _plays.Count);
                Assert.AreEqual(PlayType.TurnOver, _plays.First().PlayType);
                Assert.AreEqual(_playerA, _plays.First().Player);
                Assert.AreEqual(PlayType.Steal, _plays.Last().PlayType);
                Assert.AreEqual(_playerB, _plays.Last().Player);
            });
        }
        [Test]
        public void Substitution()
        {
            _playFactory.NewPlay(_playerA, true);
            _playFactory.ChoosePlayType(PlayType.CheckOut);
            _playFactory.ConsecutivePlay(_playerB, true);            
            
            Assert.Multiple(() =>
            {
                var _plays = _playFactory.GetPlays();
                Assert.AreEqual(2, _plays.Count);
                Assert.AreEqual(PlayType.CheckOut, _plays.First().PlayType);
                Assert.AreEqual(_playerA, _plays.First().Player);
                Assert.AreEqual(PlayType.CheckIn, _plays.Last().PlayType);
                Assert.AreEqual(_playerB, _plays.Last().Player);
            });
        }
        [Test]
        public void CheckIn_Two_Players()
        {
            _playFactory.NewPlay(_playerA, true);
            _playFactory.ChoosePlayType(PlayType.CheckIn);
            var _player1 = _playFactory.GetPlays().First().Player;
            _playFactory.Clear();
            _playFactory.NewPlay(_playerB, true);
            _playFactory.ChoosePlayType(PlayType.CheckIn);

            Assert.Multiple(() =>
            {
                var _plays = _playFactory.GetPlays();
                Assert.AreEqual(1, _plays.Count);
                Assert.AreEqual(PlayType.CheckIn, _plays.First().PlayType);
                Assert.AreEqual(_playerA, _player1);
                Assert.AreEqual(PlayType.CheckIn, _plays.Last().PlayType);
                Assert.AreEqual(_playerB, _plays.Last().Player);
            });
        }
        //[Test]
        //public void FGResult_givesExceptionIf
    }
}
