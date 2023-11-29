using Amalaka.EntityFrameworkCore.Infrastructure;

namespace Amalaka.EntityFrameworkCore.SqlServer.Infrastructure;

public class SqlServerDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) : DbContextOptionsBuilder<SqlServerDbContextOptionsBuilder, SqlServerDbContextOptionsExtension>(optionsBuilder)
{
}
