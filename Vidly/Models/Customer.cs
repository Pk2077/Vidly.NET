﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Min18YearsIfAMember]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        [Display(Name = "MemberShip Type")]
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }


    }
}