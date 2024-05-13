using MongoDB.Driver.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedLog.Service.Exceptions;

public class MedLogException : Exception
{
    public int Code { get; set; }
	public MedLogException(int code = 500, string message = "Something went wrong") : base(message)
	{
		this.Code = code;	
	}
}
