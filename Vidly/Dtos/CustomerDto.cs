using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;
using Vidly.Dtos;
namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }

        public MemberShipTypeDto MemberShipType { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}