using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceWebApi.Models
{
    public class HumidityReading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Uid { get; set; }

        public string Name { get; set; }

        public int HumidReading { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ReadingTime { get; set; }
    }
}
