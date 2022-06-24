using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwd
{
    public class Password
    {
        public static void Pwd(string UserName,string mobile,string Password) {
            string path = "C:/password";
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            /*FileStream fs = new FileStream(path+"/password.txt", FileMode.Append);*/
            StreamWriter streamWriter = File.AppendText(path + "/password.txt");
            streamWriter.WriteLine("" + mobile + "\t : \t" + UserName + "\t : \t" + Password);
            /*fs.Close();*/
            streamWriter.Close();
        }
    }
}
