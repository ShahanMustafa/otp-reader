using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OTPReader.EmailClient
{
    class ClientFactory
    {
        /// <summary>
        /// Creates instance of a Class
        /// </summary>
        /// <param name="ClientName">Name of client whose instance is required</param>
        /// <returns> Returns instance of client or null if Client's class doesnt exist</returns>
        public object CreateClient (String ClientName)
        {
            if (!String.IsNullOrEmpty(ClientName))
            {

            Assembly mscorlib = Assembly.GetExecutingAssembly();
            foreach (Type type in mscorlib.GetTypes())
            {
                if (type.Name.Equals(ClientName, StringComparison.OrdinalIgnoreCase))
                {
                     object _object = Activator.CreateInstance(type);
                     if (_object != null)
                    {
                        Logger.LogDebug(ClientName + " Client instance created succesfully");
                        return _object;
                    }
                     
                }
                 
            }
            Logger.LogError("Wrong domain name requested, No such client as '"+ ClientName+"'" );
            return null;   
            }

            else
            {
                Logger.LogError("No domain name requested");
                return null;
            }

        }
    }

}
