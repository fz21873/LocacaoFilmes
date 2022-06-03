
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Film")]
    public class Film : EntityBase
    {
       
        public string Titulo { get; set; }

        public string Descricao { get; set; }

       
        public string Tipo{ get; set; }

       
        public DateTime DataLancamento { get; set; }

        public virtual long DirectorId { get; set; }
        public virtual Director Director { get; set; }

       


    }
}
