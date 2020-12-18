using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KitchenService.Api.Model
{
    public class FridgeItem
    {
        [BindNever]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.PositiveInfinity)]
        public double WeightOz { get; set; }

        public DateTimeOffset? Expiration { get; set; }

        public bool IsExpired => Expiration <= DateTime.UtcNow;
    }
}
