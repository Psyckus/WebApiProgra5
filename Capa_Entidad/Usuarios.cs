using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        private string _clave;
        public string Clave
        {
            get { return _clave; }
            set
            {
                if (value.Length < 8)
                {
                    throw new Exception("La contraseña debe tener al menos 8 caracteres.");
                }
                if (!value.Any(char.IsUpper))
                {
                    throw new Exception("La contraseña debe contener al menos una letra mayúscula.");
                }
                if (!value.Any(char.IsLower))
                {
                    throw new Exception("La contraseña debe contener al menos una letra minúscula.");
                }
                if (!value.Any(char.IsDigit))
                {
                    throw new Exception("La contraseña debe contener al menos un número.");
                }
                if (!value.Any(c => !char.IsLetterOrDigit(c)))
                {
                    throw new Exception("La contraseña debe contener al menos un carácter especial.");
                }
                _clave = value;
            }
        }
        public string Estado { get; set; }
        public int? Intentos { get; set; }
    }
}
