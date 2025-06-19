using NoBolso.Domain.Entities;
using NoBolso.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoBolso.Domain.Entities
{
    public class Carteira : BaseEntity
    {
        public string Nome { get; private set; }
        public bool Ativo { get; private set; } = true;
        public Guid GrupoId { get; private set; }
        public Grupo Grupo { get; private set; }

        private readonly List<Transacao> _transacoes;
        public IReadOnlyCollection<Transacao> Transacoes => _transacoes.AsReadOnly();

        protected Carteira()
        {
            _transacoes = new List<Transacao>();
        }

        public Carteira(string nome, Guid grupoId) : this()
        {
            ValidarNome(nome);
            Nome = nome;
            GrupoId = grupoId;
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
            SetAtualizadoEm();
        }

        public void AtualizarNome(string novoNome)
        {
            ValidarNome(novoNome);
            Nome = novoNome;
            SetAtualizadoEm();
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            if (transacao == null)
                throw new ArgumentNullException(nameof(transacao));

            if (transacao.CarteiraId != Id)
                throw new InvalidOperationException("A transação não pertence a esta carteira.");

            _transacoes.Add(transacao);
            SetAtualizadoEm();
        }

        private decimal CalcularSaldo()
        {
            return _transacoes
                .Where(t => t.Ativo)
                .Sum(t => t.TipoTransacao == Enums.TipoTransacao.Entrada ? t.Valor : -t.Valor);
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome da carteira não pode ser vazio.", nameof(nome));

            if (nome.Length > 100)
                throw new ArgumentException("Nome da carteira não pode ter mais de 100 caracteres.", nameof(nome));
        }
    }
}