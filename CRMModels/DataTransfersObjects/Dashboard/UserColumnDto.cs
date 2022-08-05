using CRMModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMModels.DataTransfersObjects
{
    public class UserColumnDto
    {
        public int TableType { get; set; }
        public TableType TableName { get { return (TableType)TableType; } }
        public string ColumnName { get; set; }
        public int ColumnOrder { get; set; }
    }
}
