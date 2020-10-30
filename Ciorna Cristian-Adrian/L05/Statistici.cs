using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Models
{
    public class Statistici : TableEntity
    {
        public Statistici(string university,string date)
        {
            this.PartitionKey = university;
            this.RowKey = date.ToString();
        }
        public Statistici() {}
        public int TotalNrOfStudents{get; set;} 
    }
}