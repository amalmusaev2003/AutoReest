﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReest.Model;

namespace AutoReest.Services.Workers
{
    internal static class DataBuilder
    {
        public static RegistryData RegistryDataBuild(PdfWorker worker)
        {
            var data = new RegistryData();

            for (int i = 0; i < worker.GetPageNumbers() - 1; i++)
            {
                var notation = worker.FindNotation(i);
                var table = worker.FindTable(i);

                if (!string.IsNullOrWhiteSpace(notation)
                    && (table?.Length != 0))
                {
                    data.Notation = notation;
                    if (i == 1 || i == 2)
                        data.Data = table.Last().ToTableData(true);
                    else 
                        data.Data = table.Last().ToTableData(false);
                    
                    return data;
                } 
            }

            return null;
        }
    }
}
