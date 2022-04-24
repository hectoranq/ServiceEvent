using System;
namespace ServiceEventEF.Models
{
    public class appUser
    {
        public appUser()
        {
        }

        public int id
        {
            get;
            set;
        }
        public string login
        {
            get;
            set;
        }
        public string crypted_password
        {
            get;
            set;
        }
        public string password_salt
        {
            get;
            set;
        }
        public int login_count
        {
            get;
            set;
        }
        public string role
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }

        public string email
        {
            get;
            set;
        }
        // active
        public string state
        {
            get;
            set;
        }

        public int company_id
        {
            get;
            set;
        }

        public string surname
        {
            get;
            set;
        }
        /*
        public DateTime last_request_at
        {
            get;
            set;
        }

        public DateTime last_login_at
        {
            get;
            set;
        }

        public DateTime current_login_at
        {
            get;
            set;
        }*/

        public string last_login_ip
        {
            get;
            set;
        }

        public string current_login_ip
        {
            get;
            set;
        }

        public int group_id
        {
            get;
            set;
        }

        public int concesionaria_id
        {
            get;
            set;
        }

        public bool in_report_mail
        {
            get;
            set;
        }
    }
}
