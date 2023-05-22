using AutoMapper;
using Trophy.Data;
using Trophy.Domain;
using Trophy.Service;
using Trophy.Test.Helper;

namespace Trophy.Test.Service
{
    public class PlayerServiceTest
    {
        private readonly IMapper _mapper;

        public PlayerServiceTest()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();
        }


        [Fact]
        public async Task AddPlayerAsync()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new PlayerService(context, _mapper);
            var player = "AddPlayerAsync";

            //Act
            var addedPlayer = await service.AddPlayerAsync(player);

            //Assert
            Assert.Single(context.Players);
            Assert.True(context.Players.Any(_ => _.Name == player));
            Assert.Equal(player, addedPlayer.Name);
        }

        [Fact]
        public async Task GetPlayersAsync()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new PlayerService(context, _mapper);
            var player1 = new Player() { Name = "AddPlayerAsyncA" };
            var player2 = new Player() { Name = "AddPlayerAsyncC" };
            var player3 = new Player() { Name = "AddPlayerAsyncB" };
            await context.AddRangeAsync(player1, player2, player3);
            await context.SaveChangesAsync();

            //Act
            var players = await service.GetPlayersAsync();

            //Assert
            Assert.Equal(3, players.Count());
            Assert.Equal(player1.Name, players[0].Name);
            Assert.Equal(player2.Name, players[2].Name);
            Assert.Equal(player3.Name, players[1].Name);
        }
    }
}
