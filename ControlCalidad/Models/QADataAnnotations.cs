using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlCalidad.Models
{
    public class ClientQA
    {
        [Display( Name = "Cédula" )]
        public string cedulaPK
        {
            get; set;
        }
        [Display( Name = "Nombre" )]
        public string nombreP
        {
            get; set;
        }
        [Display( Name = "Primer Apellido" )]
        public string apellido1
        {
            get; set;
        }
        [Display( Name = "Segundo Apellido" )]
        public string apellido2
        {
            get; set;
        }
        [Display( Name = "Teléfono" )]
        public string telefono
        {
            get; set;
        }
        [Display( Name = "Correo" )]
        public string correo
        {
            get; set;
        }
        [Display( Name = "Provincia" )]
        public string provincia
        {
            get; set;
        }
        [Display( Name = "Cantón" )]
        public string canton
        {
            get; set;
        }
        [Display( Name = "Distrito" )]
        public string distrito
        {
            get; set;
        }
        [Display( Name = "Dirección Exacta" )]
        public string direccionExacta
        {
            get; set;
        }
    }

    public class ProjectQA
    {
        [Display( Name = "Id" )]
        public int idPK
        {
            get; set;
        }
        [Display( Name = "Nombrevvvv" )]
        public string nombre
        {
            get; set;
        }
        [Display( Name = "Objetivo" )]
        public string objetivo
        {
            get; set;
        }
        [Display( Name = "Fecha de Inicio" )]
        public System.DateTime fechaInicio
        {
            get; set;
        }
        [Display( Name = "Fecha de Finalización" )]
        public Nullable<System.DateTime> fechaFin
        {
            get; set;
        }
        [Display( Name = "Estado" )]
        public string estado
        {
            get; set;
        }
        [Display( Name = "Duración Estimada" )]
        public float duracionEstimada
        {
            get; set;
        }
        [Display( Name = "Duración Real" )]
        public Nullable<float> duracionReal
        {
            get; set;
        }
        [Display( Name = "Cliente" )]
        public string cedulaClienteFK
        {
            get; set;
        }
    }

    public class RequirementQA
    {
        [Display( Name = "Nombre" )]
        public string nombre
        {
            get; set;
        }
        [Display( Name = "Fecha de Inicio" )]
        public System.DateTime fechaInicio
        {
            get; set;
        }
        [Display( Name = "Fecha de Finalización" )]
        public Nullable<System.DateTime> fechaFinalizacion
        {
            get; set;
        }
        [Display( Name = "Fecha de Asignación" )]
        public Nullable<System.DateTime> fechaAsignacion
        {
            get; set;
        }
        [Display( Name = "Estadobbbb" )]
        public string estado
        {
            get; set;
        }
        [Display( Name = "Complejidad" )]
        public string complejidad
        {
            get; set;
        }
        [Display( Name = "Descripción" )]
        public string descripcion
        {
            get; set;
        }
        [Display( Name = "Duración Estimada" )]
        public float duracionEstimada
        {
            get; set;
        }
        [Display( Name = "Duración Real" )]
        public Nullable<float> duracionReal
        {
            get; set;
        }
    }

    public class idEmpleado
    {
        public string cedulaPk
        {
            get; set;
        }
    }

    [MetadataType( typeof( RequirementQA ) )]
    public partial class Requerimiento
    {
    }

    [MetadataType( typeof( ProjectQA ) )]
    public partial class Proyecto
    {
    }

    [MetadataType( typeof( ClientQA ) )]
    public partial class Cliente
    {
    }





}