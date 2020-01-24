using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _2 {
  class Program {
    public static async Task Main (string[] args) {

      HttpClient client = new HttpClient ();
      HttpRequestMessage req = new HttpRequestMessage (HttpMethod.Get, "https://mul14.github.io/data/employees.json");
      HttpResponseMessage res = await client.SendAsync (req);
      var getJson = await res.Content.ReadAsStringAsync ();

      var ObjekList = JsonConvert.DeserializeObject<List<objek>> (getJson);
  

      Console.WriteLine ("\nEmployees which have salary more than Rp15.000.000 : ");
      var salary = ObjekList.Where (a => a.Salary > 15000000).Select (a => a.First_Name);
      foreach (var item in salary) {
        Console.WriteLine (item);
      }

      Console.WriteLine ("\nEmployees who life in Jakarta : ");
      var address = from item in ObjekList
      from a in item.Adresses
      where a.City == "DKI Jakarta"
      select (item.Username);
      var get = address.Distinct ();
      foreach (var item in get) {
        Console.WriteLine (item);
      }

      Console.WriteLine ("\nEmployees which birthday on March :");
      var birthday = ObjekList.Where (a => a.Birthday.Month == 3).Select (a => a.Username);
      foreach (var item in birthday) {
        Console.WriteLine (item);
      }

      Console.WriteLine ("\nEmployees in Research and Development departement :");
      var dept = from item in ObjekList
      where ( item.Department.Name == "Research and development")
      select (item.Username);
      var getName = address.Distinct ();
      foreach (var item in getName) {
        Console.WriteLine (item);
      }

      Console.WriteLine ("\nHow many each employee absences in October 2019 :");
      var absences = from item in ObjekList
      from a in item.Pres_List
      where a.Month == 10 && a.Year == 2019
      select (item.Username);
      int countSloane = 0;
      int countHayness = 0;
      int countJuan = 0;
      var getList = absences.Distinct ();
      foreach (var item in absences) {
        if (item == "sloane") {
          countSloane++;
        } else if (item == "haynes") {
          countHayness++;
        } else if (item == "juan") {
          countJuan++;
        }
      }

      foreach (var item in getList) {
        if (item == "sloane") {
          Console.WriteLine (item + " : " + countSloane);
        } else if (item == "haynes") {
          Console.WriteLine (item + " : " + countHayness);
        } else if (item == "juan") {
          Console.WriteLine (item + " : " + countJuan);
        }
      }
    }
  }

  class objek {
    [JsonProperty ("id")]
    public int Id { get; set; }

    [JsonProperty ("avatar_url")]
    public string Ava_Url { get; set; }

    [JsonProperty ("employee_id")]
    public string Employee_Id { get; set; }

    [JsonProperty ("first_name")]
    public string First_Name { get; set; }

    [JsonProperty ("last_name")]
    public string Last_Name { get; set; }

    [JsonProperty ("email")]
    public string Email { get; set; }

    [JsonProperty ("username")]
    public string Username { get; set; }

    [JsonProperty ("birthday")]
    public DateTime Birthday { get; set; }

    [JsonProperty ("addresses")]
    public List<Adress> Adresses { get; set; } = new List<Adress> ();

    [JsonProperty ("phones")]
    public List<phone> Phone { get; set; } = new List<phone> ();

    [JsonProperty ("presence_list")]
    public List<DateTime> Pres_List { get; set; } = new List<DateTime> ();
    [JsonProperty ("salary")]
    public int Salary { get; set; }

    [JsonProperty ("department")]
    public department Department { get; set; } 

    [JsonProperty ("position")]
    public position Position { get; set; } = new position ();

  }

  class Adress {
    [JsonProperty ("label")]
    public string Label { get; set; }

    [JsonProperty ("address")]
    public string Var_Adress { get; set; }

    [JsonProperty ("city")]
    public string City { get; set; }
  }

  class phone {
    [JsonProperty ("label")]
    public string Label { get; set; }

    [JsonProperty ("phone")]
    public string Phone { get; set; }
  }

  class department {
    [JsonProperty ("name")]
    public string Name { get; set; }
  }
  class position {
    [JsonProperty ("name")]
    public string Name { get; set; }
  }
}