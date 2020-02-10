using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using MMCApp.Domain.Models.StorageModel;

namespace MMCApp.Infrastructure.ApplicationServices.Contracts
{
    public interface IStorageService  
    {
        string GeneatePatientMedicalStorage(string storagePath, int _patientID);
        string GeneateStorage(StorageModel _storageModel);
        string GeneateStorageByStoragePath(string storagePath);
    }
}
