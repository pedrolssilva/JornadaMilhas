using JornadaMilhas.Dados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.Test.Integracao
{
    public class ContextoFixture
    {
        public JornadaMilhasContext Context { get; }

        public ContextoFixture()
        {
            var options = new DbContextOptionsBuilder<JornadaMilhasContext>()
                .UseSqlServer("Server = localhost, 1433; Database = JornadaMilhas; User Id = sa; Password = UoyN^bW61K; Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
                .Options;

            Context = new JornadaMilhasContext(options);
        }
    }
}
