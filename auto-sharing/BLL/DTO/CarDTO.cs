using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string LicencePlate { get; set; }
        public int Model { get; set; }
        public string Year { get; set; }
        public CarStatus Status { get; set; }
    }
}
