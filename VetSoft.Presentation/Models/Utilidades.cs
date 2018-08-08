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
                case Sexo.Macho:
                    p.Genero = Constantes.Macho; break;
                case Sexo.Hembra:
                    p.Genero = Constantes.Hembra; break;
            }

        }

        public static void PacienteDeSingleModel(this Paciente p, PacienteSingleModel paciente)
        {
            p.Nombre = paciente.Nombre;
            p.Microchip_Licencia = paciente.Microchip_Licencia;

            p.FechaIngreso = DateTime.Now;
            p.FechaNac = paciente.FechaNac;
            p.Color = paciente.Color;
            p.RazaID = paciente.RazaID;

            switch (paciente.Genero.Value)
            {
                case Sexo.Macho:
                    p.Genero = Constantes.Macho; break;
                case Sexo.Hembra:
                    p.Genero = Constantes.Hembra; break;
            }

        }

        public static void PropietarioDeViewModel(this Propietario p, PropietarioViewModel propietario)
        {
            p.Nombre = propietario.Nombre;
            p.Apellido = propietario.Apellido;
            p.Email = propietario.Email;
            p.Telefono = propietario.Telefono;
            p.Mascotas = propietario.Mascotas
                .Select(x => new PropietarioPaciente {
                    ClienteID = x.ClienteID,
                    PacienteID = x.PacienteID,
                    Tipo = x.Tipo,
                    FechaRegistro = x.FechaRegistro
                }).ToList();
        }
    }
}