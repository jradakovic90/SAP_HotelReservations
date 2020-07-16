using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelReservations.Core.Entity
{
    public class Reservation : BaseEntity
    {
        [Display(Name = "Room")]
        public virtual Guid RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Rooms { get; set; }

        public int StartDate { get; set; }
        public int EndDate { get; set; }
    }
}
