using Trophy.Data;
using Trophy.Test.Helper;
using Trophy.Service;

namespace Trophy.Test.Service
{
    public class RankingServiceTest
    {
        [Fact]
        public async Task GetByWinCountAsync_NoData()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);

            //Act
            var ranking = await service.GetByWinCountAsync();

            //Assert
            Assert.Empty(ranking);
        }

        [Fact]
        public async Task GetByWinCountAsync_NoGames()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);
            await GameHelper.AddPlayer(context, "Player");

            //Act
            var ranking = await service.GetByWinCountAsync();

            //Assert
            Assert.Single(ranking);
        }

        [Fact]
        public async Task GetByWinCountAsync()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);
            var player1 = "P1";
            var player2 = "P2";
            var player3 = "P3";
            var player4 = "P4";
            await GameHelper.AddGame(context, DateTime.UtcNow, player1, player2, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow, player1, player3, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow, player3, player2, 1, 0);
            await GameHelper.AddPlayer(context, player4);

            //Act
            var ranking = await service.GetByWinCountAsync();

            //Assert
            Assert.Equal(4, ranking.Count());

            Assert.Equal(player1, ranking[0].Player);
            Assert.Equal(2, ranking[0].Value);

            Assert.Equal(player3, ranking[1].Player);
            Assert.Equal(1, ranking[1].Value);

            Assert.Equal(player2, ranking[2].Player);
            Assert.Equal(0, ranking[2].Value);

            Assert.Equal(player4, ranking[3].Player);
            Assert.Equal(0, ranking[3].Value);
        }

        [Fact]
        public async Task GetByWinRateAsync_NoData()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);

            //Act
            var ranking = await service.GetByWinRateAsync();

            //Assert
            Assert.Empty(ranking);
        }

        [Fact]
        public async Task GetByWinRateAsync_NoGames()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);
            await GameHelper.AddPlayer(context, "Player");

            //Act
            var ranking = await service.GetByWinRateAsync();

            //Assert
            Assert.Single(ranking);
        }

        [Fact]
        public async Task GetByWinRateAsync()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);
            var player1 = "P1";
            var player2 = "P2";
            var player3 = "P3";
            var player4 = "P4";
            await GameHelper.AddGame(context, DateTime.UtcNow, player1, player2, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow, player1, player2, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow, player1, player3, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow, player3, player1, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow, player3, player2, 1, 0);
            await GameHelper.AddPlayer(context, player4);

            //Act
            var ranking = await service.GetByWinRateAsync();

            //Assert
            Assert.Equal(4, ranking.Count());

            Assert.Equal(player1, ranking[0].Player);
            Assert.Equal(75, ranking[0].Value);

            Assert.Equal(player3, ranking[1].Player);
            Assert.Equal((decimal)66.67, ranking[1].Value);

            Assert.Equal(player2, ranking[2].Player);
            Assert.Equal(0, ranking[2].Value);

            Assert.Equal(player4, ranking[3].Player);
            Assert.Equal(0, ranking[3].Value);
        }

        [Fact]
        public async Task GetByWinSizeAsync_NoData()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);

            //Act
            var ranking = await service.GetByWinSizeAsync();

            //Assert
            Assert.Empty(ranking);
        }

        [Fact]
        public async Task GetByWinSizeAsync_NoGames()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);
            await GameHelper.AddPlayer(context, "Player");

            //Act
            var ranking = await service.GetByWinSizeAsync();

            //Assert
            Assert.Single(ranking);
        }

        [Fact]
        public async Task GetByWinSizeAsync()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);
            var player1 = "P1";
            var player2 = "P2";
            var player3 = "P3";
            var player4 = "P4";
            await GameHelper.AddGame(context, DateTime.UtcNow, player1, player2, 15, 11);
            await GameHelper.AddGame(context, DateTime.UtcNow, player1, player2, 14, 9);
            await GameHelper.AddGame(context, DateTime.UtcNow, player2, player3, 13, 7);
            await GameHelper.AddGame(context, DateTime.UtcNow, player2, player3, 12, 5);
            await GameHelper.AddGame(context, DateTime.UtcNow, player3, player1, 11, 3);
            await GameHelper.AddGame(context, DateTime.UtcNow, player3, player1, 10, 1);
            await GameHelper.AddPlayer(context, player4);

            //Act
            var ranking = await service.GetByWinSizeAsync();

            //Assert
            Assert.Equal(4, ranking.Count());

            Assert.Equal(player3, ranking[0].Player);
            Assert.Equal(9, ranking[0].Value);

            Assert.Equal(player2, ranking[1].Player);
            Assert.Equal(7, ranking[1].Value);

            Assert.Equal(player1, ranking[2].Player);
            Assert.Equal(5, ranking[2].Value);

            Assert.Equal(player4, ranking[3].Player);
            Assert.Equal(0, ranking[3].Value);
        }

        [Fact]
        public async Task GetByPointCountAsync_NoData()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);

            //Act
            var ranking = await service.GetByPointCountAsync();

            //Assert
            Assert.Empty(ranking);
        }

        [Fact]
        public async Task GetByPointCountAsync_NoGames()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);
            await GameHelper.AddPlayer(context, "Player");

            //Act
            var ranking = await service.GetByPointCountAsync();

            //Assert
            Assert.Single(ranking);
        }

        [Fact]
        public async Task GetByPointCountAsync()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);
            var player1 = "P1";
            var player2 = "P2";
            var player3 = "P3";
            var player4 = "P4";
            await GameHelper.AddGame(context, DateTime.UtcNow, player1, player2, 3, 1);
            await GameHelper.AddGame(context, DateTime.UtcNow, player2, player3, 4, 1);
            await GameHelper.AddGame(context, DateTime.UtcNow, player3, player1, 5, 4);
            await GameHelper.AddPlayer(context, player4);

            //Act
            var ranking = await service.GetByPointCountAsync();

            //Assert
            Assert.Equal(4, ranking.Count());

            Assert.Equal(player1, ranking[0].Player);
            Assert.Equal(7, ranking[0].Value);

            Assert.Equal(player3, ranking[1].Player);
            Assert.Equal(6, ranking[1].Value);

            Assert.Equal(player2, ranking[2].Player);
            Assert.Equal(5, ranking[2].Value);

            Assert.Equal(player4, ranking[3].Player);
            Assert.Equal(0, ranking[3].Value);
        }

        [Fact]
        public async Task GetByWinStreakAsync_NoData()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);

            //Act
            var ranking = await service.GetByWinStreakAsync();

            //Assert
            Assert.Empty(ranking);
        }

        [Fact]
        public async Task GetByWinStreakAsync_NoGames()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);
            await GameHelper.AddPlayer(context, "Player");

            //Act
            var ranking = await service.GetByWinStreakAsync();

            //Assert
            Assert.Single(ranking);
        }

        [Fact]
        public async Task GetByWinStreakAsync()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);
            var player1 = "P1";
            var player2 = "P2";
            var player3 = "P3";
            var player4 = "P4";
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(10), player1, player2, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(11), player1, player3, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(12), player2, player3, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(13), player3, player1, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(14), player3, player1, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(15), player2, player1, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(16), player1, player3, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(17), player2, player3, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(18), player3, player1, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(19), player3, player2, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(20), player1, player3, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(21), player3, player2, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddMinutes(22), player2, player3, 1, 0);
            await GameHelper.AddPlayer(context, player4);

            //Act
            var ranking = await service.GetByWinStreakAsync();

            //Assert
            Assert.Equal(4, ranking.Count());

            Assert.Equal(player2, ranking[0].Player);
            Assert.Equal(3, ranking[0].Value);

            Assert.Equal(player1, ranking[1].Player);
            Assert.Equal(2, ranking[1].Value);

            Assert.Equal(player3, ranking[2].Player);
            Assert.Equal(2, ranking[2].Value);

            Assert.Equal(player4, ranking[3].Player);
            Assert.Equal(0, ranking[3].Value);
        }

        [Fact]
        public async Task GetByTrophyTimeAsync_NoData()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);

            //Act
            var ranking = await service.GetByTrophyTimeAsync(DateTime.UtcNow);

            //Assert
            Assert.Empty(ranking);
        }

        [Fact]
        public async Task GetByTrophyTimeAsync_NoGames()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);
            await GameHelper.AddPlayer(context, "Player");

            //Act
            var ranking = await service.GetByTrophyTimeAsync(DateTime.UtcNow);

            //Assert
            Assert.Single(ranking);
        }

        [Fact]
        public async Task GetByTrophyTimeAsync()
        {
            //Arrange
            var context = DatabaseHelper.GetContext();
            var service = new RankingService(context);
            var player1 = "P1";
            var player2 = "P2";
            var player3 = "P3";
            var player4 = "P4";
            await GameHelper.AddGame(context, DateTime.UtcNow.AddDays(1), player1, player2, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddDays(4), player2, player3, 1, 0);
            await GameHelper.AddGame(context, DateTime.UtcNow.AddDays(5), player3, player1, 1, 0);
            await GameHelper.AddPlayer(context, player4);

            //Act
            var ranking = await service.GetByTrophyTimeAsync(DateTime.UtcNow.AddDays(9));

            //Assert
            Assert.Equal(4, ranking.Count());

            Assert.Equal(player3, ranking[0].Player);
            Assert.Equal(4, ranking[0].Value);

            Assert.Equal(player1, ranking[1].Player);
            Assert.Equal(3, ranking[1].Value);

            Assert.Equal(player2, ranking[2].Player);
            Assert.Equal(1, ranking[2].Value);

            Assert.Equal(player4, ranking[3].Player);
            Assert.Equal(0, ranking[3].Value);
        }
    }
}
