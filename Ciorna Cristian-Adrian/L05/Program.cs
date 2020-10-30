using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;


namespace L05
{
    class Program
	    {
	        private static CloudTable studentsTable;
	        private static CloudTableClient tableClient;
	        private static TableOperation tableOperation;
	        private static TableResult tableResult;
	        private static List<Studententity> students  = new List<Studententity>();
	        static void Main(string[] args)
	        {
	            Task.Run(async () => { await Initialize(); })
	                .GetAwaiter()
	                .GetResult();
	        }
	        static async Task Initialize()
	        {
	            string storageConnectionString = "DefaultEndpointsProtocol=https;"
	            + "AccountName=datc2020ciorna;"
	            + "AccountKey=/4DpNkt8ai+rb15b5Acq2SbR6qTKUDBeHwGCRS+K33fjo/qewUQqAJaFLYQjiXK2raRuToELs2S49I25zWpcXg==;"
	            + "EndpointSuffix=core.windows.net";
	
	            var account = CloudStorageAccount.Parse(storageConnectionString);
	            tableClient = account.CreateCloudTableClient();
	
	            studentsTable = tableClient.GetTableReference("Studenti");
	
	            await studentsTable.CreateIfNotExistsAsync();
	            
	            int option = -1;
	            do
	            {
	                System.Console.WriteLine("1.Adauga student.");
	                System.Console.WriteLine("2.Modifica student.");
	                System.Console.WriteLine("3.Sterge student.");
	                System.Console.WriteLine("4.Afiseaza studentii.");
	                System.Console.WriteLine("5.Iesire");
	                System.Console.WriteLine("Alege optiunea:");
	                string opt = System.Console.ReadLine();
	                option =Int32.Parse(opt);
	                switch(option)
	                {
	                    case 1:
	                        await AddNewStudent();
	                        break;
	                    case 2:
	                        await EditStudent();
	                        break;
	                    case 3:
	                        await DeleteStudent();
	                        break;
	                    case 4:
	                        await DisplayStudents();
	                        break;
	                    case 5:
	                        System.Console.WriteLine("Iesire!");
	                        break;
	                }
	            }while(option != 5);
	            
	        }
	        private static async Task<Studententity> RetrieveRecordAsync(CloudTable table,string partitionKey,string rowKey)
	        {
	            tableOperation = TableOperation.Retrieve<Studententity>(partitionKey, rowKey);
	            tableResult = await table.ExecuteAsync(tableOperation);
	            return tableResult.Result as Studententity;
	        }
	        private static async Task AddNewStudent()
	        {
	            System.Console.WriteLine("Adauga universitatea:");
	            string university = Console.ReadLine();
	            System.Console.WriteLine("Adauga CNP:");
	            string cnp = Console.ReadLine();
	            System.Console.WriteLine("Adauga nume:");
	            string nume = Console.ReadLine();
	            System.Console.WriteLine("Adauga prenume:");
	            string prenume = Console.ReadLine();
	            System.Console.WriteLine("Adauga facultate:");
	            string facultate= Console.ReadLine();
	            System.Console.WriteLine("Adauga an studiu:");
	            string an = Console.ReadLine();
	
	            Studententity stud = await RetrieveRecordAsync(studentsTable, university, cnp);
	            if(stud == null)
	            {
	                var student = new Studententity(university,cnp);
	                student.Nume = nume;
	                student.Prenume = prenume;
	                student.Facultate = facultate;
	                student.An = Convert.ToInt32(an);
	                var insertOperation = TableOperation.Insert(student);
	                await studentsTable.ExecuteAsync(insertOperation);
	                System.Console.WriteLine("S-a adaugat!");
	            }
	            else
	            {
	                System.Console.WriteLine("S-a gasit!");
	            }
	        }
	        private static async Task EditStudent()
	        {
	            System.Console.WriteLine("Adauga universitatea:");
	            string university = Console.ReadLine();
	            System.Console.WriteLine("Adauga CNP:");
	            string cnp = Console.ReadLine();
	            Studententity stud = await RetrieveRecordAsync(studentsTable, university, cnp);
	            if(stud != null)
	            {
	                System.Console.WriteLine("S-a gasit!");
	                var student = new Studententity(university,cnp);
	                System.Console.WriteLine("Adauga nume:");
	                string nume = Console.ReadLine();
	                System.Console.WriteLine("Adauga prenume:");
	                string prenume = Console.ReadLine();
	                System.Console.WriteLine("Adauga facultate:");
	                string facultate = Console.ReadLine();
	                System.Console.WriteLine("Adauga an studiu:");
	                string an = Console.ReadLine();
	                student.Nume= nume;
	                student.Prenume = prenume;
	                student.Facultate = facultate;
	                student.An = Convert.ToInt32(an);
	                student.ETag = "*";
	                var updateOperation = TableOperation.Replace(student);
	                await studentsTable.ExecuteAsync(updateOperation);
	                System.Console.WriteLine("S-a modificat!");
	            }
	            else
	            {
	                System.Console.WriteLine("Nu s-a gasit studentul!");
	            }
	        }
	        private static async Task DeleteStudent()
	        {
	            System.Console.WriteLine("Adauga universitatea:");
	            string university = Console.ReadLine();
	            System.Console.WriteLine("Adauga CNP:");
	            string cnp = Console.ReadLine();
	
	            Studententity stud = await RetrieveRecordAsync(studentsTable, university, cnp);
	            if(stud != null)
	            {
	                var student = new Studententity(university,cnp);
	                student.ETag = "*";
	                var deleteOperation = TableOperation.Delete(student);
	                await studentsTable.ExecuteAsync(deleteOperation);
	                System.Console.WriteLine("S-a sters!");
	            }
	            else
	            {
	                System.Console.WriteLine("Studentul nu exista!");
	            }
	        }
	        private static async Task<List<Studententity>> GetAllStudents()
	        {
	            TableQuery<Studententity> tableQuery = new TableQuery<Studententity>();
	            TableContinuationToken token = null;
	            do
	            {
	                TableQuerySegment<Studententity> result = await studentsTable.ExecuteQuerySegmentedAsync(tableQuery,token);
	                token = result.ContinuationToken;
	                students.AddRange(result.Results);
	            }while(token != null);
	            return students;
	        }
	        private static async Task DisplayStudents()
	        {
	            await GetAllStudents();
	
	            foreach(Studententity std in students)
	            {
	                Console.WriteLine("Student facultate : {0}", std.Facultate);
	                Console.WriteLine("Student nume : {0}", std.Nume);
	                Console.WriteLine("Student prenume : {0}", std.Prenume);
	                Console.WriteLine("Student an : {0}", std.An);
	                Console.WriteLine("******************************");
	            }
	            students.Clear();
	            
	        }
	    }
	}

