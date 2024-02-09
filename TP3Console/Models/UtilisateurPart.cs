using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3Console.Models.EntityFramework
{
    public partial class Utilisateur
    {
        public Utilisateur() { }
        public Utilisateur(string login, string email, string pwd)
        {
            Login = login;
            Email = email;
            Pwd = pwd;
        }

        public override string ToString()
        {
            return $"Id : {this.Id}\nLogin : {this.Login}";
        }
    }
}
