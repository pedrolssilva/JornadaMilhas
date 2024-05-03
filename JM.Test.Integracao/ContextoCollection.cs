using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.Test.Integracao
{
    [CollectionDefinition(nameof(ContextoCollection))]
    public class ContextoCollection : ICollectionFixture<ContextoFixture>
    {
    }
}
