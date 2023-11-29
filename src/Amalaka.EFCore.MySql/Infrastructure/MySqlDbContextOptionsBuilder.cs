using Amalaka.EntityFrameworkCore.Infrastructure;

namespace Amalaka.EntityFrameworkCore.SqlServer.Infrastructure;

public class MySqlDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) : DbContextOptionsBuilder<MySqlDbContextOptionsBuilder, MySqlDbContextOptionsExtension>(optionsBuilder)
{
}
