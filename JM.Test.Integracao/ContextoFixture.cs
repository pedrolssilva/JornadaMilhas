using JornadaMilhas.Dados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace JM.Test.Integracao
{
    public class ContextoFixture : IAsyncLifetime
    {
        public JornadaMilhasContext Context { get; private set; }
        private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();

        public async Task InitializeAsync()
        {
            await _msSqlContainer.StartAsync();
            var options = new DbContextOptionsBuilder<JornadaMilhasContext>()
                .UseSqlServer(_msSqlContainer.GetConnectionString())
                //.UseSqlServer("Server = localhost, 1433; Database = JornadaMilhas; User Id = sa; Password = UoyN^bW61K; Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
                .Options;

            Context = new JornadaMilhasContext(options);
            Context.Database.Migrate();
        }

        public async Task DisposeAsync()
        {
            await _msSqlContainer.StopAsync();
        }
    }
}
