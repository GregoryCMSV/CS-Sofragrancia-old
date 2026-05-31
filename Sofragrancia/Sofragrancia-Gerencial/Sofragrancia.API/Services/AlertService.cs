using Sofragrancia.Banco.Repositories;
using Supabase;

namespace Sofragrancia.API.Services
{
    public class AlertService 
    {
        AlertaRepository _repository;

        public AlertService(Client client)
        {
            _repository = new(client);
        }

        public async Task SincronizarAlertasDoUsuarioAsync(string idUsuarioAuth, string emailUsuario)
        {
            await _repository.SincronizarAlertasDoUsuarioAsync(idUsuarioAuth, emailUsuario);
        }

    }
}
