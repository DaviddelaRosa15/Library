﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Application.Dtos.Account
{
	public class JwtResponse
	{
		public bool HasError { get; set; }
		public string Error { get; set; }
	}
}