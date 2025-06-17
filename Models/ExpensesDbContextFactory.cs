using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Azure.Core;
using Microsoft.Data.SqlClient;
using System.IO;

namespace MVC_first.Models
{
    public class ExpensesDbContextFactory : IDesignTimeDbContextFactory<ExpensesDbContext>
    {
        public ExpensesDbContext CreateDbContext(string[] args)
        {
            // Manually load config from the base directory
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            var azureCredential = new DefaultAzureCredential();
            var token = azureCredential.GetToken(
                new TokenRequestContext(new[] { "https://database.windows.net/.default" }));

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.AccessToken = token.Token;

            var optionsBuilder = new DbContextOptionsBuilder<ExpensesDbContext>();
            optionsBuilder.UseSqlServer(sqlConnection);

            return new ExpensesDbContext(optionsBuilder.Options);
        }
    }
}