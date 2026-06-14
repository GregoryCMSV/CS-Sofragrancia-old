using Sofragrancia_EmailSender.Interfaces;
using Sofragrancia_EmailSender.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia_EmailSender.Factory
{
    public static class AlertaStrategyFactory
    {
        public static IAlertStrategy? ObterStrategy(int alertaBaseId)
        {
            return alertaBaseId switch
            {
                1 => new EstoqueMinimoStrategy(), 
                2 => new EstoqueMaximoStrategy(),
                3 => new TaxaCancelamentoStrategy(),
                4 => new ClienteInativoStrategy(),
                5 => new MetaVendasStrategy(),
                _ => null 
            };
        }
    }
}
