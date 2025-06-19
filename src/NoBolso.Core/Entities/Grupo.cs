using NoBolso.Domain.Entities.Common;
using System.Collections.Generic;

namespace NoBolso.Domain.Entities;

public class Grupo : BaseEntity
{
    public string Nome { get; private set; }

    public ICollection<Usuario> Usuarios { get; private set; } = new List<Usuario>();
    public ICollection<Carteira> Carteiras { get; private set; } = new List<Carteira>();

    protected Grupo() { }

    public Grupo(string nome)
    {
        Nome = nome;
    }
}
