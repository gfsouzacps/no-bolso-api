using System;
using NoBolso.Domain.Entities.Common;
using NoBolso.Domain.Enums;

namespace NoBolso.Domain.Entities
{
    public class Transacao : BaseEntity
    {
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public TipoTransacao TipoTransacao { get; private set; }
        public DateTime DataTransacao { get; private set; }
        public Guid CarteiraId { get; private set; }
        public bool Ativo { get; private set; }

        // Navigation property
        public Carteira Carteira { get; private set; }

        protected Transacao() { }

        public Transacao(
            string descricao,
            decimal valor,
            TipoTransacao tipoTransacao,
            DateTime dataTransacao,
            Guid carteiraId)
        {
            ValidarDescricao(descricao);
            ValidarValor(valor);
            ValidarDataTransacao(dataTransacao);
            ValidarCarteiraId(carteiraId);

            Descricao = descricao;
            Valor = valor;
            TipoTransacao = tipoTransacao;
            DataTransacao = dataTransacao;
            CarteiraId = carteiraId;
            Ativo = true;
        }

        public void AtualizarDescricao(string novaDescricao)
        {
            ValidarDescricao(novaDescricao);
            Descricao = novaDescricao;
            SetAtualizadoEm();
        }

        public void AtualizarValor(decimal novoValor)
        {
            ValidarValor(novoValor);
            Valor = novoValor;
            SetAtualizadoEm();
        }

        public void AtualizarTipoTransacao(TipoTransacao novoTipo)
        {
            TipoTransacao = novoTipo;
            SetAtualizadoEm();
        }

        public void AtualizarDataTransacao(DateTime novaData)
        {
            ValidarDataTransacao(novaData);
            DataTransacao = novaData;
            SetAtualizadoEm();
        }

        public void Desativar()
        {
            Ativo = false;
            SetAtualizadoEm();
        }

        public void Reativar()
        {
            Ativo = true;
            SetAtualizadoEm();
        }

        private static void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição da transação não pode ser vazia.", nameof(descricao));

            if (descricao.Length > 500)
                throw new ArgumentException("Descrição da transação não pode ter mais de 500 caracteres.", nameof(descricao));
        }

        private static void ValidarValor(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor da transação deve ser maior que zero.", nameof(valor));
        }

        private static void ValidarDataTransacao(DateTime dataTransacao)
        {
            if (dataTransacao > DateTime.UtcNow.AddDays(1))
                throw new ArgumentException("Data da transação não pode ser no futuro.", nameof(dataTransacao));
        }

        private static void ValidarCarteiraId(Guid carteiraId)
        {
            if (carteiraId == Guid.Empty)
                throw new ArgumentException("ID da carteira deve ser válido.", nameof(carteiraId));
        }
    }
}