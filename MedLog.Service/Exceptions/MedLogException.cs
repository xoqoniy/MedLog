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
	public MedLogException(int code = 500, string message = "Nimadir xato ketti") : base(message)
	{
		this.Code = code;	
	}
}
