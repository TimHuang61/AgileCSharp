using System.ComponentModel.DataAnnotations;

namespace Demo_Sprint1.Models
{
    public class CreateRoomViewModel
    {
        [Required]
        public string NewRoomName { get; set; }
    }
}