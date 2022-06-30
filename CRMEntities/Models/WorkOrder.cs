using CRMEntities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMEntities.Models
{
    public class WorkOrder : BaseEntity
    {
        [Required]
        public int Priority { get; set; }
        [Required]
        public int Status { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public string Notes { get; set; }
        //Tracking Fields
        public DateTime? LastStatusChangeDate { get; set; }
        //----------------Relational Fields-------------------------
        [ForeignKey(nameof(Contact))]
        public long? ContactId { get; set; }
        // ---------------To be configured--------------------------
        //public ICollection<LineItem> LineItems { get; set; }
        //public ICollection<AssignedTeamMember> AssignedTeamMembers { get; set; }
        //public ICollection<Subcontractor> Subcontractors { get; set; }
        //---------------Navigation properties-----------------------
        public Contact Contact { get; set; }
    }
}
