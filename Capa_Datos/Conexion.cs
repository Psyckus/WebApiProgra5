﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class Conexion
    {
        public static string cn = ConfigurationManager.ConnectionStrings["cadena"].ToString();
    }
}
