using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Capstone.Models
{
    public class F1Driver
    {
        public int DriverId { get; set; }
        public int DriverPopularity { get; set; } 
        public string Name { get; set; }
        public string DOB {get; set; }
        public string Team { get; set; }
        public int CarNum { get; set; }
        public string Nationality { get; set; }
        public int WorldChampionships {get; set; }
        public int NumberOfGPStarts { get; set; }
        public int NumberOfGPPodiums { get; set; }
        public int NumberOfWins { get; set; }
        public int NumberOfDNFs { get; set; }

    }

}
