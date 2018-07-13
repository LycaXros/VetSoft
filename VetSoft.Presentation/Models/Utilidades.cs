using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{
    public static class Utilidades
    {

        public static void PacienteDeViewModel(this Paciente p, PacienteViewModel paciente)
        {
            p.Nombre = paciente.Nombre;
            p.Microchip_Licencia = paciente.Microchip_Licencia;
            
            p.FechaIngreso = DateTime.Now;
            p.FechaNac = paciente.FechaNac;
            p.Color = paciente.Color;
            p.RazaID = paciente.RazaID;
            
            switch (paciente.Genero.Value)
            {
                case Sexo.Masculino:
                    p.Genero = Constantes.Masculino; break;
                case Sexo.Femenino:
                    p.Genero = Constantes.Femenino; break;
            }

        }

        public static void PropietarioDeViewModel(this Propietario p, PropietarioViewModel propietario)
        {
            p.Nombre = propietario.Nombre;
            p.Apellido = propietario.Apellido;
            p.Email = propietario.Email;
            p.Telefono = propietario.Telefono;
            p.Mascotas = propietario.Mascotas;
        }
    }
}