﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorUsuariosBol
    {
        private ProveedorUsuariosDal proveedorUsuariosDal = new ProveedorUsuariosDal();
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();

        public string iniciarSesion(string usuario, string contra)
        {
            mensajeRespuestaSP.Clear();   
            return proveedorUsuariosDal.GetNombreByUsuarioByContra(usuario, contra).Nombre; ;
        }
    }
}