﻿using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IReservationService
    {
        IEnumerable<ReservationDTO> GetReservations(int page);
    }
}