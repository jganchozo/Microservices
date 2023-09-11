using ServiceShop.Messenger.Email.SendGridLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceShop.Messenger.Email.SendGridLibrary.Interface
{
    public interface IEmail
    {
        Task<ResponseModel> Send(SendGridData data);
    }
}

public record ResponseModel(bool Result, string? ErrorMessage);
