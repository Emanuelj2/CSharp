using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace MediaManagementSystem.Code
{
    internal class DvD : LibraryItem
    {
        //Extra field: Duration (minutes)
        //Extra field: AgeRating
        //Late fee: $1.00 per day

        public int Durration {  get; private set; }
        public int AgeRating { get; private set; }

        //double lateFee = 1.00;
        public DvD(int id, string title, int durration, int ageRating) : base(id, title)
        {
            Durration = durration;
            AgeRating = ageRating;
        }

        //method to get the info
        public override string GetInfo()
        {
            return $"Movie\n" +
                $"ID: {UniqueId}" +
                $"Title {Title}" +
                $"Durration: {Durration}" +
                $"Age Rating: {AgeRating}" +
                $"Borrowed {Borrowed}";
        }
    }
}
