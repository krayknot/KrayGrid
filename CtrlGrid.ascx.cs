using Krayknot.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_CtrlGrid : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void BindGridData<T>(List<T> dataSet, string colstoDisplay, string properColNamesJson, string editColumnName = "")
    {
        string column = string.Empty;
        DataTable dt = new DataTable();

        dt = dataSet.ConvertToDataTable(); //Convert to datatable to get the colsand rows details

        //Get the column names---------------------------------
        string[] columnNames = dt.Columns.Cast<DataColumn>()
                                   .Select(x => x.ColumnName)
                                   .ToArray();

        //Columns to display-----------------------------------
        string[] arrColstoRetain = colstoDisplay.Split(',');

        //Remove other columns and keep only those to display--
        for (int i = 0; i <= columnNames.GetUpperBound(0); i++)
        {
            if (!arrColstoRetain.Contains(columnNames[i]))
            {
                if (dt.Columns.Contains(columnNames[i]))
                {
                    dt.Columns.Remove(columnNames[i]);
                    i = 0;
                }
            }
        }

        //Set the Headers and the rows---------------------------------------------------------------------
        string headers = String.Empty;
        string rowsDetails = String.Empty;
        var jsonHeadersString = properColNamesJson;
        JObject jsonHeaders = JObject.Parse(jsonHeadersString);

        dt.SetColumnsOrder(colstoDisplay.Split(','));

        //Prepare the columns and map the readable column names taking from the json file------------------
        for (int cols = 0; cols <= dt.Columns.Count - 1; cols++)
        {
            headers += "<th>" + jsonHeaders.SelectToken(dt.Columns[cols].ToString()).ToString() + "</th>";
        }
        headers = "<thead><tr>" + headers + "</tr></thead>";
        literalHeaders.Text = headers;

        //Prepare the rows---------------------------------------------------------------------------------
        for (int r = 0; r <= dt.Rows.Count - 1; r++)
        {
            for (int rCol = 0; rCol <= dt.Columns.Count - 1; rCol++)
            {
                if(dt.Columns[rCol].ColumnName == editColumnName)
                {
                    column += "<td><a href='"+ dt.Rows[r][rCol].ToString() + "' target='_blank'><span class='fa fa-pencil'></span></a></td>";
                }
                else
                {
                    column += "<td>" + dt.Rows[r][rCol].ToString() + "</td>";
                }

            }
            rowsDetails += "<tr>" + column + "</tr>";
            column = String.Empty;
        }

        rowsDetails = "<tbody>" + rowsDetails + "</tbody>";
        literalRows.Text = rowsDetails;
    }
}