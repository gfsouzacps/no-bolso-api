using System.Threading.Tasks;
using NoBolso.Domain.Events;

namespace NoBolso.Domain.Interfaces.Services
{
    public interface IEventService
    {
        Task PublicarEventoAsync<T>(T evento) where T : class;
        Task PublicarTransacaoCriadaAsync(TransacaoCriadaEvent evento);
    }
}