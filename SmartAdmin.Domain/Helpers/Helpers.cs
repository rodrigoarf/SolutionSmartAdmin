using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmartAdmin.Domain.Helpers
{
    public class Untils
    {
        #region OPERAÇÕES COM IP
                 
        /// <summary>
        /// Obtem o endereço de IP do cliente, atraves de um serviço externo de obtenção de ip, método usado quando
        /// o servidor ao qual a aplicação esta hospedada, esta em mode Trust Level Mediun, pois essa configuração na
        /// machine.config do servidor não permite a execução de certas dll(s), neste caso nao é possivel usar: Envirorment (mscorlib),
        /// System.Net e outras. Obs: Requer conhecimento avançado sobre Trust level.
        /// </summary>
        public static string GetClientIpAddress()
        {
            try
            {
                var ExternalURL = "http://checkip.dyndns.org";
                var Request = System.Net.WebRequest.Create(ExternalURL);
                var Response = Request.GetResponse();
                var StreamReader = new System.IO.StreamReader(Response.GetResponseStream());
                var ReturnReader = StreamReader.ReadToEnd().Trim();

                var a0 = ReturnReader.Split(':');
                var a2 = a0[1].Substring(1);
                var a3 = a2.Split('<');
                var Retorno = a3[0];

                return (Retorno);
            }
            catch (Exception)
            {
                return ("0.0.0.0");
            }       
        }

        /// <summary>
        /// Gera uma senha randômica até 14 caracteres.
        /// </summary>
        public static string GeneratePassword(int Size)
        {
            //var Identifier = Guid.NewGuid().ToString().Replace("-", string.Empty);     
            //var ObjectRandom = new Random();
            //var Pass = String.Empty;
            //var SizePass = ObjectRandom.Next(1, 14);  
           
            //for (Int32 i = 0; i <= SizePass; i++)
            //    Pass += Identifier.Substring(ObjectRandom.Next(1, Identifier.Length), 1);

            //return Pass;
            var CaracteresValidos = "abcdefghijklmnopqrstuvwxyz1234567890@#!?";
            var ValorMaximo = CaracteresValidos.Length;
            var ObjectRandom = new Random(DateTime.Now.Millisecond);
            var Pass = new StringBuilder(Size);

            for (int i = 0; i < Size; i++)
            {
                Pass.Append(CaracteresValidos[ObjectRandom.Next(0, (ValorMaximo - 1))]);
            }                 

            return (Pass.ToString());
        }

        #endregion   
    }
}
