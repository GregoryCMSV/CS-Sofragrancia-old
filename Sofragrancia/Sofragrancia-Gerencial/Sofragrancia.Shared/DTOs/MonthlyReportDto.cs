using System;
using System.Collections.Generic;

namespace Sofragrancia.Shared.Dtos;

public class MonthlyReportDto
{
    // E-mail isolado e dedicado apenas para este relatório sob demanda
    public string EmailDestinatario { get; set; } = string.Empty;

    // Período de coleta dos dados selecionado pelo usuário
    public DateTime DataInicialRelatorio { get; set; } = DateTime.Today.AddMonths(-1);
    public DateTime DataFinalRelatorio { get; set; } = DateTime.Today;

    // Lista contendo quais seções o usuário marcou como ativo ou não
    public List<MonthlyReportSectionDto> Sections { get; set; } = new();
}