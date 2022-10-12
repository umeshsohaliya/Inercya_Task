using System.Data;

namespace Helper
{
    public static class CsvHelper
    {

        public static DataTable CsvToDatatable(string csvData)
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string[] row = csvData.Split(System.Environment.NewLine);
                for (int i = 0; i < row.Count() - 1; i++)
                {
                    string[] rowData = row[i].Split(';');
                    {
                        if (i == 0)
                        {
                            for (int j = 0; j < rowData.Count(); j++)
                            {
                                dtCsv.Columns.Add(rowData[j].Trim());
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int k = 0; k < rowData.Count(); k++)
                            {
                                dr[k] = rowData[k].ToString();
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return dtCsv;
        }

    }
}