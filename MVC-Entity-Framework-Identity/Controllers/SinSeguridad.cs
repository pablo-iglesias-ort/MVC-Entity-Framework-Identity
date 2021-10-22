using System.Text;

namespace MVC_Entity_Framework.Controllers
{
	public class SinSeguridad : ISeguridad
	{
		public byte[] EncriptarPass(string pass)
		{
			return Encoding.UTF8.GetBytes(pass);
		}

		public bool ValidarPass(string pass)
		{
			return true;
		}
	}
}