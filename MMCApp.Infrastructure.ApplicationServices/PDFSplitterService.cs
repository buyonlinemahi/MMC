using iTextSharp.text;
using iTextSharp.text.pdf;
using MMCApp.Infrastructure.Global;
using System.IO;

namespace MMCApp.Infrastructure.ApplicationServices
{
    public class PDFSplitterService
    {
        private StorageService _storageService;
        public PDFSplitterService() {
            _storageService = new StorageService();
        }
        public string splitPDFIntake(int startpage, int endpage, string VirtualDirPath, string saveToPath, string fileName, string saveToFileName)
        {
            return SplitPDFFile(startpage, endpage, VirtualDirPath + GlobalConst.VirtualPathFolders.IntakeUploadFiles + GlobalConst.ConstantChar.DoubleBackSlash + fileName, saveToPath + GlobalConst.ConstantChar.DoubleBackSlash, saveToFileName);
        }

        public int getTotalPageofFileIntake(string VirtualDirPath, string fileName)
        {
            return getTotalPageofFile(VirtualDirPath + GlobalConst.VirtualPathFolders.IntakeUploadFiles + "\\" + fileName);
        }

        public void deleteIntakeUploadedFile(string VirtualDirPath, string fileName)
        {
            deleteFile(VirtualDirPath + "\\" + fileName);
        }

        public void deleteFileRFARecSplt(string fileFullPath)
        {
            deleteFile(fileFullPath);
        }

        private void deleteFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }

        private int getTotalPageofFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                PdfReader pdfReader = new PdfReader(filepath);
                int s = pdfReader.NumberOfPages;
                pdfReader.Dispose();
                return s;
            }
            else
                return 0;
        }

        private string SplitPDFFile(int startpage, int endpage, string filepath,string saveToPath,string saveToFileName)
        {
            //source file where pages can be read to create new pdf file.....hp
            //read the file using pdf reader......hp
            PdfReader reader = new PdfReader(filepath);
            //get the number of total page of the source file.....hp
            int pageCount = reader.NumberOfPages;
            if (endpage < startpage || endpage > pageCount)
            {
                endpage = pageCount;
            }
            if (startpage > pageCount)
                startpage = pageCount;
            //create object of new document to create the pdf file.....hp
            Document doc = new Document(reader.GetPageSizeWithRotation(startpage));

            //write the name for the new file that will created from source file......hp
            string filename = saveToFileName + Path.GetExtension(filepath);

            bool exists = System.IO.Directory.Exists(saveToPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(saveToPath);

            string path1 = saveToPath + filename;
            //assign the document and file stream to create the blank pdf and then write according to selected pages from the source file......hp
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path1, FileMode.OpenOrCreate, FileAccess.ReadWrite));
            doc.Open();
            //get the bytes of pdf using pdf writter.....hp
            PdfContentByte cb = writer.DirectContent;

            int i = startpage;
            //loop for get the number of pages from the source file and write new file....hp
            while (i <= endpage)
            {
                //set the page size for the new page in pdf as per the page size in the source pdf....hp
                doc.SetPageSize(reader.GetPageSizeWithRotation(i));
                //create new page in the pdf....hp
                doc.NewPage();
                //get the content of page from the source pdf using the pdf reader........hp
                PdfImportedPage page = writer.GetImportedPage(reader, i);
                //get rotation of pdf page...landscape or potrait.....according to it set the new pdf page rotation.....hp
                int rotation = reader.GetPageRotation(i);
                if (rotation == 90 || rotation == 270)
                {
                    cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                }
                else
                {
                    cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                }
                i++;
            }

            //after written all the page close the pdf......hp
            doc.Close();
            doc.Dispose();
            writer.Dispose();
            reader.Dispose();
            
            return filename;
        }

        #region Patient(s) Medical Record

        public string splitPDFPatientMedicalRecord(int startpage, int endpage, string VirtualDirPath,string saveToPath, string fileName, string saveToFileName)
        {
            return SplitPDFFilePatientMedicalRecord(startpage, endpage, VirtualDirPath + GlobalConst.VirtualPathFolders.Patients + GlobalConst.ConstantChar.DoubleBackSlash + fileName, saveToPath + GlobalConst.ConstantChar.DoubleBackSlash, saveToFileName);
        }

        public int getTotalPageofFilePatientMedicalRecord(string VirtualDirPath, string fileName)
        {
            return getTotalPageofFile(VirtualDirPath + GlobalConst.VirtualPathFolders.Patients + "\\" + fileName);
        }

        public void deleteFilePatientMedicalRecordRecSplt(string VirtualDirPath, string fileName)
        {
            deleteFile(VirtualDirPath + "\\" + fileName);
        }

        public string SplitPDFFilePatientMedicalRecord(int startpage, int endpage, string filepath, string saveToPath, string saveToFileName)
        {
            //source file where pages can be read to create new pdf file.....hp
            //read the file using pdf reader......hp
            PdfReader reader = new PdfReader(filepath);
            //get the number of total page of the source file.....hp
            int pageCount = reader.NumberOfPages;
            if (endpage < startpage || endpage > pageCount)
            {
                endpage = pageCount;
            }
            if (startpage > pageCount)
                startpage = pageCount;
            //create object of new document to create the pdf file.....hp
            Document doc = new Document(reader.GetPageSizeWithRotation(startpage));

            //write the name for the new file that will created from source file......hp
            string filename = saveToFileName + Path.GetExtension(filepath);

            bool exists = System.IO.Directory.Exists(saveToPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(saveToPath);

            string path1 = saveToPath + filename;
            //assign the document and file stream to create the blank pdf and then write according to selected pages from the source file......hp
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path1, FileMode.OpenOrCreate, FileAccess.ReadWrite));
            doc.Open();
            //get the bytes of pdf using pdf writter.....hp
            PdfContentByte cb = writer.DirectContent;

            int i = startpage;
            //loop for get the number of pages from the source file and write new file....hp
            while (i <= endpage)
            {
                //set the page size for the new page in pdf as per the page size in the source pdf....hp
                doc.SetPageSize(reader.GetPageSizeWithRotation(i));
                //create new page in the pdf....hp
                doc.NewPage();
                //get the content of page from the source pdf using the pdf reader........hp
                PdfImportedPage page = writer.GetImportedPage(reader, i);
                //get rotation of pdf page...landscape or potrait.....according to it set the new pdf page rotation.....hp
                int rotation = reader.GetPageRotation(i);
                if (rotation == 90 || rotation == 270)
                {
                    cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                }
                else
                {
                    cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                }
                i++;
            }

            //after written all the page close the pdf......hp
            doc.Close();
            doc.Dispose();
            writer.Dispose();
            reader.Dispose();

            return filename;
        }
        #endregion
    }
}

