﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share.Models.File
{
    public class FileDto
    {
        public IFormFile File { get; set; }
       
    }
}
