using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AdStudio.Data.Security
{
    /// <summary>
    /// Classe que criptografa uma senha no banco de dados, na tabela usuários, e trambem pode ser gerada uma nova senha,
    /// para ser enviada por email, quando o usuario solicitar uma nova senha por algum motivo.
    /// </summary>
    public class Senha
    {
        private StringBuilder _mensagem;
        private int _minTamanho = 0;
        private int _minQtdLetras = 0;
        private int _minQtdNumero = 0;
        
        #region :: Métodos de manipulação ::

        /// <summary>
        /// Armazena a mensagem de validação da senha
        /// </summary>
        public string Mensagem
        {
            get
            {
                return (_mensagem.ToString());
            }
        }

        /// <summary>
        /// Cria uma instancia deste classe.
        /// </summary>
        public Senha()
        {
            _mensagem = new StringBuilder();
        }

        /// <summary>
        /// Cria uma instancia deste classe.
        /// </summary>
        /// <param name="p_minTamanho">Informa o tamanho minímo da senha</param>
        /// <param name="p_minQtdLetras">Quantidade de letras que deve conter a senha</param>
        /// <param name="p_minQtdNumero">Quantidade de números que deve conter a senha</param>
        public Senha(int p_minTamanho, int p_minQtdLetras, int p_minQtdNumero)
        {
            _mensagem = new StringBuilder();
            _minTamanho = p_minTamanho;
            _minQtdLetras = p_minQtdLetras;
            _minQtdLetras = p_minQtdNumero;
        }

        /// <summary>
        /// Gera hash de uma senha.
        /// </summary>
        public string Criptografa(string senha)
        {
            senha = senha.Trim().ToLower();
            return (GeraHashMD5(senha));
        }

        /// <summary>
        /// Gera hash de uma senha passada padrão MD5 de criptografia.
        /// </summary>
        private static string GeraHashMD5(string senha)
        {
            MD5 algorithm = MD5.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(senha));
            string sh1 = string.Empty;

            for (int i = 0; i < data.Length; i++)
            {
                sh1 += data[i].ToString("x2").ToUpperInvariant();
            }

            return sh1;
        }

        /// <summary>
        /// Gera uma nova senha aleatoriamente 
        /// </summary>
        /// <param name="Tamanho">Número de caracteres da nova senha</param>
        /// <returns>Retorna a senha gerada no tamanho especificado</returns>
        public string GeraSenha(int tamanho)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[tamanho];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return (finalString);
        }

        /// <summary>
        /// Verifica se a senha atende aos pré-requisitos definidos na inicialização do objeto
        /// </summary>
        /// <param name="Senha">Senha a ser verificada</param>
        /// <returns>True quando válida</returns>
        public bool SenhaValida(string senha)
        {
            bool retorno = false;
            int QtdLetra, QtdNumero, Verify;

            if (senha.Length >= _minTamanho)
            {
                QtdLetra = 0;
                QtdNumero = 0;

                for (int i = 0; i < senha.Length; i++)
                {
                    if (int.TryParse(senha[i].ToString(), out Verify))
                        QtdNumero += 1;
                    else
                        QtdLetra += 1;
                }

                if ((QtdNumero < _minQtdNumero) || (QtdLetra < _minQtdLetras))
                    _mensagem.Append("A Senha deve conter pelo menos " + _minQtdLetras.ToString() + " letras e " + _minQtdNumero.ToString() + " números");
                else
                    retorno = true;
            }
            else
            {
                _mensagem.Append("A Senha deve conter pelo menos " + _minTamanho.ToString() + " caracteres");
            }

            return (retorno);
        }

        #endregion
    }
}
