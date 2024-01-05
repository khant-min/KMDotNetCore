using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMDotNetCore.AtmWebApp.Models
{
    [Table("Tbl_Atm")]
    public class AtmDataModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CardNumber { get; set; }
        public int Pin { get; set; }
        public int Balance { get; set; }
    }

    public class MessageModel
    {
        public MessageModel() { }

        public MessageModel(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}