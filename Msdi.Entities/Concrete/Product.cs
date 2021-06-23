using Msdi.Core.Entities;
using System;
using System.Collections.Generic;

#nullable disable

namespace Msdi.Entities.Concrete
{
    public partial class Product  : IEntity
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RegisterDate { get; set; }
    }
}
