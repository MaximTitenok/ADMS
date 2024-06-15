using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMS.Models
{
    internal class Order
    {
        [Key]
        public ushort Id { get; set; }
        public string? Number { get; set; }
        public string? Name { get; set; }
        public int? Type{ get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        [Column("Groups", TypeName = "integer[]")]
        public int[] Groups { get; set; }
        [Column("Students", TypeName = "integer[]")]
        public int[] Students { get; set; }
        [Column("File", TypeName = "integer[]")]
        public ushort[]? File { get; set; }
        public Employee? AddedEmplyoee { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? AddedTime { get; set; }
        public Order() { }
        public Order(Order order)
        {
            this.Id = order.Id;
            this.Number = order.Number;
            this.Name = order.Name;
            this.Type = order.Type;
            this.Description = order.Description;
            this.Status = order.Status;
            this.Groups = order.Groups;
            this.Students = order.Students;
            this.File = order.File;
            this.AddedEmplyoee = order.AddedEmplyoee;
            this.StartDate = order.StartDate;
            this.EndDate = order.EndDate;
            this.AddedTime = order.AddedTime;
        }
    }
}
