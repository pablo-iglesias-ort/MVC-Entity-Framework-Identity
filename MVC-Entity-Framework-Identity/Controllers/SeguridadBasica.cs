using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MVC_Entity_Framework.Controllers
{
	public class SeguridadBasica : ISeguridad
	{
		public byte[] EncriptarPass(string pass)
		{
			return new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(pass));
		}

		public bool ValidarPass(string pass)
		{
			if (string.IsNullOrEmpty(pass))
			{
				return false;
			}
				
			// Regular Expressions (RegEx) para validar minuculas, mayusculas y numeros
			var tieneMinuscula = Regex.Match(pass, $"[a-z]").Success;
			var tieneMayuscula = Regex.Match(pass, $"[A-Z]").Success;
			var tieneNumero = Regex.Match(pass, $"[0-9]").Success;

			// una longitud de 8 caracteres o mas
			// una minuscula, una mayuscula, un numero
			return pass.Length >= 8 && tieneMinuscula && tieneMayuscula && tieneNumero;
		}
	}
}