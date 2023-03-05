
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace ApiPasswordSeguras.Controllers
{

        [ApiController]
        [Route("[controller]")]
        public class PasswordController : ControllerBase
        {
            [HttpGet]
            public IActionResult Generate(int length = 12, bool includeUppercase = true, bool includeLowercase = true, bool includeNumbers = true, bool includeSymbols = true)
            {
                if (length < 4 || length > 128)
                {
                    return BadRequest("La longitud de la contraseña debe estar entre 4 y 128 caracteres.");
                }

                if (!includeUppercase && !includeLowercase && !includeNumbers && !includeSymbols)
                {
                    return BadRequest("Debe seleccionar al menos un tipo de carácter para incluir en la contraseña.");
                }

                var validChars = new StringBuilder();
                if (includeUppercase)
                {
                    validChars.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
                }

                if (includeLowercase)
                {
                    validChars.Append("abcdefghijklmnopqrstuvwxyz");
                }

                if (includeNumbers)
                {
                    validChars.Append("0123456789");
                }

                if (includeSymbols)
                {
                    validChars.Append("!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~");
                }

                var password = new char[length];
                using (var rng = RandomNumberGenerator.Create())
                {
                    var bytes = new byte[length];
                    rng.GetBytes(bytes);
                    for (var i = 0; i < length; i++)
                    {
                        var index = bytes[i] % validChars.Length;
                        password[i] = validChars[index];
                    }
                }

                return Ok(new string(password));
            }
        }
    }

    
