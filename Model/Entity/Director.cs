
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Director")]
    public class Director: EntityBase
    {
       
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public List<Film> Film { get; set; }
    }
}
