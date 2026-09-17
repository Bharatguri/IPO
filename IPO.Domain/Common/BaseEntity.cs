using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Domain.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public Boolean IsActive { get; set; }
    public Boolean IsDeleted { get; set; }
}
