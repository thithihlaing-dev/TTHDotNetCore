﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TTHDotNetCore.Database.Models;

public partial class TblBlog
{
    
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string BlogAuthor { get; set; } = null!;

    public string BlogContent { get; set; } = null!;

    public bool DeleteFlag { get; set; }
}
