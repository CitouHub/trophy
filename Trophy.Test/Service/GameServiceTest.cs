using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentAssertions;
using Trophy.Data;
using Trophy.Domain;
using Trophy.Service;
using Trophy.Test.Helper;

namespace Trophy.Test.Service
{
    public class GameServiceTest
    {
        private readonly IMapper _mapper;

        public GameServiceTest() 
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task AddGameAsync()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new GameService(context, _mapper);
            var gameDto = new GameDTO()
            {
                Location = "AddGameAsync",
                MatchDate = DateTime.Now,
                PlayerResults = new List<PlayerResultDTO>
                {
                    new PlayerResultDTO
                    {
                        Player = new PlayerDTO()
                        {
                            Id = 1,
                            Name = "P1",
                        },
                        Score = 0,
                        Win = false
                    },
                    new PlayerResultDTO
                    {
                        Player = new PlayerDTO()
                        {
                            Id = 2,
                            Name = "P2",
                        },
                        Score = 1,
                        Win = false
                    },
                }
            };

            //Act
            await service.AddGameAsync(gameDto);

            //Assert
            var game = await context.Games.ToListAsync();
            var playerResults = await context.PlayerResults.ToListAsync();
            Assert.Single(game);
            Assert.Equal(2, playerResults.Count());
            Assert.NotEqual(0, context.Games.First(_ => _.Location == gameDto.Location).Id);
            Assert.True(playerResults.All(_ => _.GameId == game[0].Id));
            Assert.True(playerResults.First(_ => _.PlayerId == gameDto.PlayerResults.OrderByDescending(_ => _.Score).ToList()[0].Player!.Id).Win);
            Assert.False(playerResults.First(_ => _.PlayerId == gameDto.PlayerResults.OrderByDescending(_ => _.Score).ToList()[1].Player!.Id).Win);
        }

        [Fact]
        public async Task GetGamesAsync()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new GameService(context, _mapper);
            var game1 = new Game()
            {
                Location = "GetGamesAsync1",
                MatchDate = DateTime.Now.AddMinutes(-10),
                PlayerResults = new List<PlayerResult>()
                {
                    new PlayerResult()
                    {
                        Player = new Player()
                        {
                            Name = "Test 1",
                        },
                        Score = 1,
                        Win = true
                    },
                    new PlayerResult()
                    {
                        Player = new Player()
                        {
                            Name = "Test 2",
                        },
                        Score = 0,
                        Win = false
                    }
                }
            };
            var game2 = new Game()
            {
                Location = "GetGamesAsync2",
                MatchDate = DateTime.Now.AddMinutes(-30),
                PlayerResults = new List<PlayerResult>()
                {
                    new PlayerResult()
                    {
                        Player = new Player()
                        {
                            Name = "Test 1",
                        },
                        Score = 0,
                        Win = false
                    },
                    new PlayerResult()
                    {
                        Player = new Player()
                        {
                            Name = "Test 2",
                        },
                        Score = 1,
                        Win = true
                    }
                }
            };
            var game3 = new Game()
            {
                Location = "GetGamesAsync3",
                MatchDate = DateTime.Now.AddMinutes(-20),
                PlayerResults = new List<PlayerResult>()
                {
                    new PlayerResult()
                    {
                        Player = new Player()
                        {
                            Name = "Test 2",
                        },
                        Score = 0,
                        Win = false
                    },
                    new PlayerResult()
                    {
                        Player = new Player()
                        {
                            Name = "Test 1",
                        },
                        Score = 1,
                        Win = true
                    }
                }
            };
            await context.AddRangeAsync(game1, game2, game3);
            await context.SaveChangesAsync();

            //Act
            var games = await service.GetGamesAsync();

            //Assert
            Assert.Equal(3, games.Count());
            Assert.Equal(game1.Location, games[0].Location);
            Assert.Equal(game2.Location, games[2].Location);
            Assert.Equal(game3.Location, games[1].Location);

            games[0].PlayerResults[0].Should().BeEquivalentTo(_mapper.Map<PlayerResultDTO>(game1.PlayerResults.ToList()[0]));
            games[0].PlayerResults[1].Should().BeEquivalentTo(_mapper.Map<PlayerResultDTO>(game1.PlayerResults.ToList()[1]));
            games[2].PlayerResults[0].Should().BeEquivalentTo(_mapper.Map<PlayerResultDTO>(game2.PlayerResults.ToList()[1]));
            games[2].PlayerResults[1].Should().BeEquivalentTo(_mapper.Map<PlayerResultDTO>(game2.PlayerResults.ToList()[0]));
            games[1].PlayerResults[0].Should().BeEquivalentTo(_mapper.Map<PlayerResultDTO>(game3.PlayerResults.ToList()[1]));
            games[1].PlayerResults[1].Should().BeEquivalentTo(_mapper.Map<PlayerResultDTO>(game3.PlayerResults.ToList()[0]));
        }

        [Fact]
        public async Task GetTrophyHolderAsync()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new GameService(context, _mapper);
            var player1 = "P1";
            var player2 = "P2";
            var player3 = "P3";
            var player4 = "P4";
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(-30), player1, player3, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(-20), player2, player1, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(-10), player3, player2, 1, 0);
            await GameHelper.AddPlayer(context, player4);

            //Act
            var trophyHolder = await service.GetTrophyHolderAsync();

            //Assert
            Assert.Equal(player3, trophyHolder.Name);
            Assert.StartsWith("#", trophyHolder.AvatarColor);
            Assert.Equal(7, trophyHolder.AvatarColor.Length);
        }
    }
}
