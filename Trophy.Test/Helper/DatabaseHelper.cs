using Microsoft.EntityFrameworkCore;
using Trophy.Data;

namespace Trophy.Test.Helper
{
    public static class DatabaseHelper
    {
        public static TrophyDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<TrophyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TrophyDbContext(options);
            //AddDefaults(context);

            return context;
        }
    }
}
