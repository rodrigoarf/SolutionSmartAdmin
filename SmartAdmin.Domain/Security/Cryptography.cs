using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SmartAdmin.Domain.Security
{
    /// <summary>
    /// Classe de criptografia simétrica ou de tambem chamado de chave única, que criptografa informações 
    /// que requerem segurança em sua visualização na aplicação e no banco de dados. Pode ser usada
    /// por 2 ou mais pessoas compartilhando uma mesma informação ente si, desde que saibam qual é a 
    /// chave.
    ///  <remarks>
    ///    http://msdn.microsoft.com/pt-br/library/5e9ft273%28v=vs.110%29.aspx
    ///  </remarks> 
    /// </summary>
    public class Cryptography : IDisposable
    {
        #region :: Variáveis e Métodos Privados ::

        private bool _disposed;
        private string _key = string.Empty;
        private ECryptographyTypes _CryptographyTypes;
        private SymmetricAlgorithm _algorithm;

        /// <summary>
        /// Inicialização do vetor do algoritmo simétrico.
        /// </summary>
        private void SetIV()
        {
            switch (_CryptographyTypes)
            {
                case ECryptographyTypes.Rijndael:
                    _algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
                    break;
                default:
                    _algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
                    break;
            }
        }

        #endregion

        #region :: Propriedades ::

        /// <summary>
        /// Chave secreta para o algoritmo simétrico de criptografia.
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        #endregion

        #region :: Construtores ::

        /// <summary>
        /// Contrutor padrão da classe, é setado um tipo de criptografia padrão (Rijndael).
        /// </summary>
        public Cryptography()
        {
            _algorithm = new RijndaelManaged();
            _algorithm.Mode = CipherMode.CBC;
            _CryptographyTypes = ECryptographyTypes.Rijndael;
        }

        /// <summary>
        /// Construtor com o tipo de criptografia a ser usada Você pode escolher o tipo pelo Enum chamado CryptographyTypes.
        /// </summary>
        /// <param name="CryptographyTypes">Tipo de criptografia.</param>
        public Cryptography(ECryptographyTypes CryptographyTypes)
        {
            // Seleciona algoritmo simétrico
            switch (CryptographyTypes)
            {
                case ECryptographyTypes.Rijndael:
                    _algorithm = new RijndaelManaged();
                    _CryptographyTypes = ECryptographyTypes.Rijndael;
                    break;
                case ECryptographyTypes.RC2:
                    _algorithm = new RC2CryptoServiceProvider();
                    _CryptographyTypes = ECryptographyTypes.RC2;
                    break;
                case ECryptographyTypes.DES:
                    _algorithm = new DESCryptoServiceProvider();
                    _CryptographyTypes = ECryptographyTypes.DES;
                    break;
                case ECryptographyTypes.TripleDES:
                    _algorithm = new TripleDESCryptoServiceProvider();
                    _CryptographyTypes = ECryptographyTypes.TripleDES;
                    break;
            }
            _algorithm.Mode = CipherMode.CBC;
        }

        #endregion

        #region :: Metodos Públicos ::

        /// <summary>
        /// Gera a chave de criptografia válida dentro do array.
        /// </summary>
        /// <returns>Chave com array de bytes.</returns>
        public virtual byte[] GetKey()
        {
            string salt = string.Empty;
            // Ajusta o tamanho da chave se necessário e retorna uma chave válida
            if (_algorithm.LegalKeySizes.Length > 0)
            {
                // Tamanho das chaves em bits
                int keySize = _key.Length * 8;
                int minSize = _algorithm.LegalKeySizes[0].MinSize;
                int maxSize = _algorithm.LegalKeySizes[0].MaxSize;
                int skipSize = _algorithm.LegalKeySizes[0].SkipSize;
                if (keySize > maxSize)
                {
                    // Busca o valor máximo da chave
                    _key = _key.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {
                    // Seta um tamanho válido
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize)
                    {
                        // Preenche a chave com arterisco para corrigir o tamanho
                        char paddingChar = '*';
                        _key = _key.PadRight(validSize / 8, paddingChar);
                    }
                }
            }
            PasswordDeriveBytes key = new PasswordDeriveBytes(_key, ASCIIEncoding.ASCII.GetBytes(salt));
            return key.GetBytes(_key.Length);
        }

        /// <summary>
        /// Encripta o dado solicitado.
        /// </summary>
        /// <param name="plainText">Texto a ser criptografado.</param>
        /// <returns>Texto criptografado.</returns>
        public virtual string Encrypt(string texto)
        {
            byte[] plainByte = Encoding.UTF8.GetBytes(texto);
            byte[] keyByte = GetKey();
            // Seta a chave privada
            _algorithm.Key = keyByte;
            SetIV();
            // Interface de criptografia / Cria objeto de criptografia
            ICryptoTransform cryptoTransform = _algorithm.CreateEncryptor();
            MemoryStream _memoryStream = new MemoryStream();
            CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Write);
            // Grava os dados criptografados no MemoryStream
            _cryptoStream.Write(plainByte, 0, plainByte.Length);
            _cryptoStream.FlushFinalBlock();
            // Busca o tamanho dos bytes encriptados
            byte[] cryptoByte = _memoryStream.ToArray();
            // Converte para a base 64 string para uso posterior em um xml
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
        }

        /// <summary>
        /// Desencripta o dado solicitado.
        /// </summary>
        /// <param name="cryptoText">Texto a ser descriptografado.</param>
        /// <returns>Texto descriptografado.</returns>
        public virtual string Decrypt(string textoCriptografado)
        {
            // Converte a base 64 string em num array de bytes
            byte[] cryptoByte = Convert.FromBase64String(textoCriptografado);
            byte[] keyByte = GetKey();
            // Seta a chave privada
            _algorithm.Key = keyByte;
            SetIV();
            // Interface de criptografia / Cria objeto de descriptografia
            ICryptoTransform cryptoTransform = _algorithm.CreateDecryptor();
            try
            {
                MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);
                // Busca resultado do CryptoStream
                StreamReader _streamReader = new StreamReader(_cryptoStream);
                return _streamReader.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region :: Implements dispose methods ::

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize (this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _algorithm.Dispose();
            }
            _key = null;
            _disposed = true;
        }

        ~Cryptography()
        {
            Dispose(false);
        }

        #endregion  
    }

    /// <summary>
    /// Enumerator com os tipos de classes para criptografia.
    /// </summary>
    public enum ECryptographyTypes
    {
        /// <summary>
        /// Representa a classe base para implementações criptografia dos algoritmos simétricos Rijndael.
        /// </summary>
        Rijndael,
        /// <summary>
        /// Representa a classe base para implementações do algoritmo RC2.
        /// </summary>
        RC2,
        /// <summary>
        /// Representa a classe base para criptografia de dados padrões (DES - Data Encryption Standard).
        /// </summary>
        DES,
        /// <summary>
        /// Representa a classe base (TripleDES - Triple Data Encryption Standard).
        /// </summary>
        TripleDES
    }
}
