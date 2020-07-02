using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GDA.Models
{
    public class RankModel
    {
        [Key]
        public int RankId { get; set; }
        public string RankName { get; set; }
    }
}
