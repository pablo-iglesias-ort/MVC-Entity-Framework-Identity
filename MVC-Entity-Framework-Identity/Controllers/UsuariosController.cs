using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Entity_Framework.Data;
using MVC_Entity_Framework.Models;

namespace MVC_Entity_Framework.Controllers
{
    [AllowAnonymous]
    public class UsuariosController : Controller
    {
        private readonly MVC_Entity_FrameworkContext _context;

        public UsuariosController(MVC_Entity_FrameworkContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Ingresar(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ingresar(string usuario, string pass)
        {
            // Verificamos que ambos esten informados
            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(pass)){

                // Verificamos que exista el usuario
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.User == usuario);
                if (user != null)
                {

                    // Verificamos que coincida la contraseña
                    var contraseña = Encoding.UTF8.GetBytes(pass);
                    if (contraseña.SequenceEqual(user.Contraseña))
                    {

                        // Creamos los Claims (credencial de acceso con informacion del usuario)
                        ClaimsIdentity identidad = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        // Agregamos a la credencial el nombre de usuario
                        identidad.AddClaim(new Claim(ClaimTypes.Name, usuario));
                        // Agregamos a la credencial el nombre del estudiante/administrador
                        identidad.AddClaim(new Claim(ClaimTypes.GivenName, user.Nombre));
                        // Agregamos a la credencial el Rol
                        identidad.AddClaim(new Claim(ClaimTypes.Role, user.Rol.ToString()));

                        ClaimsPrincipal principal = new ClaimsPrincipal(identidad);

                        // Ejecutamos el Login
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        // Redirigimos a la pagina principal
                        return RedirectToAction("Index", "Home");

                    }                    
                }
            }

            ViewBag.ErrorEnLogin = "Verifique el usuario y contraseña";
            return View();

            
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }


        public IActionResult Registrarse()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrarse(Usuario usuario, string pass)
        {
            if (ModelState.IsValid)
            {
                usuario.Id = Guid.NewGuid();
                usuario.Contraseña = Encoding.UTF8.GetBytes(pass);
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Ingresar));
            }
            return View(usuario);
        }
    }
}
