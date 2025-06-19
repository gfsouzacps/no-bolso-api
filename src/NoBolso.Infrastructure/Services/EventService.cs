using Microsoft.Extensions.Logging;
using NoBolso.Domain.Interfaces.Services;
using NoBolso.Domain.Events;
using System.Text.Json;

namespace NoBolso.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly ILogger<EventService> _logger;
        // private readonly IHttpClientFactory _httpClientFactory; // Para chamadas HTTP para n8n

        public EventService(ILogger<EventService> logger)
        {
            _logger = logger;
        }

        public async Task PublicarEventoAsync<T>(T evento) where T : class
        {
            _logger.LogInformation("Publicando evento: {EventoTipo}", typeof(T).Name);

            var eventoJson = JsonSerializer.Serialize(evento);
            _logger.LogDebug("Evento serializado: {EventoJson}", eventoJson);

            // Aqui você implementaria a lógica para enviar para n8n
            // Exemplo:
            // var httpClient = _httpClientFactory.CreateClient();
            // var response = await httpClient.PostAsync("webhook-url-n8n", content);

            await Task.CompletedTask;
        }

        public async Task PublicarTransacaoCriadaAsync(TransacaoCriadaEvent evento)
        {
            _logger.LogInformation("Transação criada - ID: {TransacaoId}, Carteira: {CarteiraId}, Valor: {Valor}",
                evento.TransacaoId, evento.CarteiraId, evento.Valor);

            await PublicarEventoAsync(evento);
        }
    }
}