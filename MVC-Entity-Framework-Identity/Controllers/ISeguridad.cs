namespace MVC_Entity_Framework.Controllers
{
	public interface ISeguridad
	{
		public byte[] EncriptarPass(string pass);
		public bool ValidarPass(string pass);
	}
}
