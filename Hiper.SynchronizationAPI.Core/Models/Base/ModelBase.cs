using System;
using System.ComponentModel.DataAnnotations;

namespace Hiper.SynchronizationAPI.Core.Models
{
    public class ModelBase
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset LastUpdateOn { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;
    }
}
