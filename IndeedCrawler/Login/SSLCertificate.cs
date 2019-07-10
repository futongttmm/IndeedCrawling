using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace IndeedCrawler.Login
{

    public class MyPolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint srvPoint,
          X509Certificate certificate, WebRequest request,
          int certificateProblem)
        {
            //Return True to force the certificate to be accepted.
            return true;
        }
    }
}
