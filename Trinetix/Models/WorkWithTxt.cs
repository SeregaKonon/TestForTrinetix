using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinetix.Models
{
    public class WorkWithTxt
    {
        static DbFileContext db = new DbFileContext();
        public static void BulkInserting(List<Words> wordList)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbFileContext"].ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                {
                    bulkCopy.BatchSize = 1000;
                    bulkCopy.DestinationTableName = "Words";
                    try
                    {
                        DataTable wordsDataTable = ConvertToDataTable.ToDataTable(wordList);
                        wordsDataTable.Columns.Remove(wordsDataTable.Columns[5]);
                        bulkCopy.WriteToServer(wordsDataTable);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        connection.Close();
                    }
                }

                transaction.Commit();
            }
        }

        public static List<Words> AddWords(List<string[]> result, Files f)
        {
            List<Words> wordList = new List<Words>();
            int i = 0;
            int j = 0;
            foreach (var r in result)
            {
                ++j;
                foreach (var word in r)
                {
                    ++i;
                    Words w = new Words();
                    w.WordID = Guid.NewGuid();
                    w.WordName = word;
                    w.WordPositionCol = i;
                    w.WordPositionRow = j;
                    w.FileId = f.FileId;
                    wordList.Add(w);
                }
            }

            return wordList;
        }

        public static void AddFilesData(string filepath, Files f)
        {
            FileData fd = new FileData();
            byte[] filedata;
            fd.Files = f;
            using (var stream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                using (var reader = new System.IO.BinaryReader(stream))
                {
                    filedata = reader.ReadBytes((int)stream.Length);
                }
                fd.FileData1 = filedata;
                db.FileData.Add(fd);
            }
            db.SaveChanges();
        }

        public static Files AddFiles(string filepath, FileTypes ft, Dirrectories dir)
        {
            Files f = new Files();
            f.FileTypes = ft;
            f.FileName = System.IO.Path.GetFileNameWithoutExtension(filepath);
            f.DateCreated = DateTime.Now;
            f.Dirrectories = dir;
            db.Files.Add(f);
            db.SaveChanges();
            return f;
        }

        public static Dirrectories AddDirrectories(string filepath)
        {
            Dirrectories dir = new Dirrectories();
            dir.DirrectoryName = System.IO.Path.GetDirectoryName(filepath);
            if (!db.Dirrectories.Any(x => x.DirrectoryName == dir.DirrectoryName))
            {
                db.Dirrectories.Add(dir);
            }
            else
            {
                dir = (Dirrectories)db.Dirrectories.FirstOrDefault(x => x.DirrectoryName == dir.DirrectoryName);
            }
            db.SaveChanges();
            return dir;
        }

        public static FileTypes AddFileTypes(string filepath)
        {
            FileTypes ft = new FileTypes();
            ft.FileTypeName = System.IO.Path.GetExtension(filepath);
            if (!db.FileTypes.Any(x => x.FileTypeName == ft.FileTypeName))
            {
                db.FileTypes.Add(ft);
            }
            else
            {
                ft = (FileTypes)db.FileTypes.FirstOrDefault(x => x.FileTypeName == ft.FileTypeName);
            }
            db.SaveChanges();
            return ft;
        }

        public static string[] DevideByWords(string s)
        {
            return s.Split(' ');
        }
    }
}
