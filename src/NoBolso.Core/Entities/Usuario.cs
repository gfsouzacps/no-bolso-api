using NoBolso.Domain.Entities;
using NoBolso.Domain.Entities.Common;

namespace NoBolso.Domain.Entities;

public class Usuario : BaseEntity
{
    public string Nome { get; private set; }
    public string Email { get; private set; }

    private readonly List<Carteira> _carteiras = new();
    public IReadOnlyCollection<Carteira> Carteiras => _carteiras.AsReadOnly();
    protected Usuario() { }

    public Usuario(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}