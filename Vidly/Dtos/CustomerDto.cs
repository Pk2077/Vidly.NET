﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }

        public MembershipTypeDto MembershipType { get; set; }
        public bool isSubscribedToNewsletter { get; set; }
        public byte MembershipTypeId { get; set; }
    }
}