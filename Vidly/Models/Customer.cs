using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MemberShipType MemberShipType { get; set; }

        [Display(Name ="MemberShip Type")]
        public byte MemberShipTypeId { get; set; }

        [MIn18YrsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}