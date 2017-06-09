﻿using Voat.Common.Configuration;

namespace Voat.Data
{
    public enum DataStoreType
    {
        SqlServer,
        PostgreSql
    }
    public class DataConfigurationSettings : ConfigurationSettings<DataConfigurationSettings>
    {
       
        public DataStoreType StoreType { get; set; }

        public DataConnection[] Connections { get; set; }

    }
    public class DataConnection
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}