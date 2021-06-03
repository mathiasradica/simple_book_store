using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple_Book_Store.ViewModels
{
    [NotMapped]
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
