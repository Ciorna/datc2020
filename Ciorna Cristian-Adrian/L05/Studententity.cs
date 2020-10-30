
using System;
	using Microsoft.WindowsAzure.Storage.Table;
	
	namespace Models
	{
	    public class Studententity : TableEntity
	    {
	        public Studententity(string University, string Cnp)
	        {
	            this.PartitionKey = University;
	            this.RowKey = Cnp;
	        }
	        public Studententity() {}
	
	        public string Nume {get; set;}
	        public string Prenume {get; set;}
	        public string Facultate {get; set;}
	        public int An {get; set;}
	        
	
	    }
	}
