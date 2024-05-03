using JornadaMilhas.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace JM.Test.Integracao
{
    [Collection(nameof(ContextoCollection))]
    public class OfertaViagemDalRecuperarPorId
    {
        private readonly JornadaMilhasContext context;
       
        public OfertaViagemDalRecuperarPorId(ITestOutputHelper output, ContextoFixture fixture)
        {
            context = fixture.Context;
            output.WriteLine(context.GetHashCode().ToString());
        }

        [Fact]
        public void RetornaNuloQuandoIdInexistente()
        {
            //Arrange 
            var dal = new OfertaViagemDAL(context);

            //Act 
            var ofertaRecuperada = dal.RecuperarPorId(-2);

            //Assert    
            Assert.Null(ofertaRecuperada);
        }
    }
}
