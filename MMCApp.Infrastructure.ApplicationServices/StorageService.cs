using iTextSharp.text;
using iTextSharp.text.pdf;
using MMCApp.Domain.Models.StorageModel;
using MMCApp.Infrastructure.ApplicationServices.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System;
using System.Web;

namespace MMCApp.Infrastructure.ApplicationServices
{
    public class StorageService : System.Web.UI.Page, IStorageService
    {
        public const string Patients = "Patients";
        public const string MedicalRecords = "MedicalRecords";
        public const string LegalDocs = "LegalDocs";
        public const string Letters = "Letters";
        public const string Invoices = "Invoices";
        public const string Templates = "Templates";
        public const string MergePDF = "MergePDF";
        public string GeneatePatientMedicalStorage(string storagePath, int _patientID)
        {
            string path = Path.Combine(storagePath, Patients, _patientID.ToString(), Templates);
            CreateDirectory(path);
            return path;
        }

        public string GeneateStorage(StorageModel _storageModel)
        {
            string path = "";
            if (_storageModel.ReferralID != 0)
            {
                //path create inside the ReferralID
                path = Path.Combine(_storageModel.path, _storageModel.ClientID.ToString(), _storageModel.PatientID.ToString(), _storageModel.ClaimID.ToString(), _storageModel.ReferralID.ToString(), _storageModel.FolderName);
            }
            else
            {
                //path create outside the ReferralID
                path = Path.Combine(_storageModel.path, _storageModel.ClientID.ToString(), _storageModel.PatientID.ToString(), _storageModel.ClaimID.ToString(), _storageModel.FolderName);
            }
            CreateDirectory(path);
            return path;
        }


        public string getReferralLetterStorage(string storagePath, int referralID)
        {
            string path = Path.Combine(storagePath, Patients, referralID.ToString());
            CreateDirectory(path);
            //referralID => letters folder
            path = Path.Combine(path, Letters);
            CreateDirectory(path);

            return path;
        }

        public string getInvoiceFolder(StorageModel _storageModel)
        {
            string path = "";
            if (_storageModel.ReferralID != 0)
            {
                //path create inside the ClaimID in Invoice Folder
                path = Path.Combine(_storageModel.path, _storageModel.ClientID.ToString(), _storageModel.PatientID.ToString(), _storageModel.ClaimID.ToString(), _storageModel.FolderName);
            }
            CreateDirectory(path);
            return path;
        }

        public string getUserSignatureStorage(string storagePath, int userID)
        {
            string path = Path.Combine(storagePath, Global.GlobalConst.VirtualPathFolders.User, userID.ToString());
            CreateDirectory(path);
            return path;
        }

        public string GeneateStorageByStoragePath(string storagePath)
        {
            string path = Path.Combine(storagePath);
            CreateDirectory(path);
            return path;
        }


        public string renameMedicalRecordsFiles(string storagePath, int patientID, int claimID, string newFileName, string oldFileName)
        {
            //patient ID
            string path = Path.Combine(storagePath, Patients, patientID.ToString(), claimID.ToString(), MedicalRecords);

            if (File.Exists(Path.Combine(path, oldFileName)))
            {
                File.Move(Path.Combine(path, oldFileName), Path.Combine(path, newFileName));
            }

            return path;
        }

        public void movePatientMedicalRecordIntakeByClaimID(string sourcePath, string destinationPath)
        {
            if (File.Exists(sourcePath))
                File.Move(sourcePath, destinationPath);
        }

        private static bool CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }
            return false;
        }

        public string getCurrentWokloadReportStorage(string storagePath, string folderName)
        {
            string path = Path.Combine(storagePath, folderName);
            CreateDirectory(path);
            path = Path.Combine(path);
            CreateDirectory(path);
            return path;
        }

        public void MergePdf(string outPutFilePath, params string[] filesPath)
        {
            List<PdfReader> readerList = new List<PdfReader>();
            PdfReader pdfReader = null;
            foreach (string filePath in filesPath)
            {
                pdfReader = new PdfReader(filePath);
                readerList.Add(pdfReader);               
            }

            //Define a new output document and its size, type
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 0, 0, 0, 0);
            //Create blank output pdf file and get the stream to write on it.
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outPutFilePath, FileMode.OpenOrCreate,FileAccess.ReadWrite));
            document.Open();
        
            foreach (PdfReader reader in readerList)
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    document.Add(iTextSharp.text.Image.GetInstance(page));
                }
            }
            document.Close();
            document.Dispose();
            foreach (PdfReader pdfRead in readerList)
            {
                pdfRead.Close();
                pdfRead.Dispose();
            }
            pdfReader.Dispose();
            writer.Dispose();
        }

        public void MergeDoc(string outPutFilePath, params string[] filePath) 
        {
            object oMissing = System.Reflection.Missing.Value;
            object oObject = System.Reflection.Missing.Value;

                Word.ApplicationClass oWord = new Word.ApplicationClass();
                Word._Document oDoc = null;
                oWord.Visible = false;
                Word.Documents oDocs = oWord.Documents;
                try
                {   
                    oDoc = oDocs.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                    foreach (string fs in filePath)
                    {
                        if (File.Exists(fs))
                        {
                            oWord.Selection.InsertFile(fs, ref oObject, ref oObject, ref oObject, ref oObject);
                            object obj = Word.WdBreakType.wdPageBreak;
                            //oWord.Selection.InsertBreak(ref obj);
                        }
                    }
                    object objFinalFile = (object)outPutFilePath;
                    oDoc.SaveAs(ref objFinalFile, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                }
                catch (Exception e)
                {

                }
                finally
                {
                    oDoc.Close(ref oMissing, ref oMissing, ref oMissing);
                }            
        }
    }
}
