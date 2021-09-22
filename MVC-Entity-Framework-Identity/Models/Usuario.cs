using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_Entity_Framework.Models
{
	public class Usuario
	{
		[Key]
		public Guid Id { get; set; }

		[MaxLength(20, ErrorMessage = "La longitud máxima es {1}")]
		[MinLength(8, ErrorMessage = "La longitud mínima es {1}")]
		[Display(Name = "Usuario")]
		public string User { get; set; }

		[ScaffoldColumn(false)]
		public byte[] Contraseña { get; set; }

		[MaxLength(50, ErrorMessage = "La longitud máxima es {1}")]
		[MinLength(3, ErrorMessage = "La longitud mínima es {1}")]
		public string Nombre { get; set; }

		[Required]
		public Rol Rol { get; set; }
	}
}
